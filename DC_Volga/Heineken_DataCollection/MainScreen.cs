using Modbus.Device;
using Npgsql;
using Sharp7;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;

namespace Heineken_DataCollection
{
    public partial class MainScreen : Form
    {
        #region initialisation
        const int numberOfMessages = 200;

        class stdMessage {
            public string text;
            public string type;
            public bool previousState;
            public bool currentState;
            public DateTime time;
            public TimeSpan duration;
        }

        bool firstScan = false;
        bool firstStart = false;
        bool firstStartMB = false;

        public uint counterTime = new uint();
        public uint counterPLC3679 = new uint();
        public uint counterPLC2 = new uint();
        public uint counterMessages = new uint();
        public uint counterDB = new uint();
        public uint counterS7 = new uint();

        public uint counterTime_mb = new uint();
        public uint counterHSS_0 = new uint();
        public uint counterHSS_1 = new uint();
        public uint counterHSS_1_add = new uint();
        public uint counterHSS_2 = new uint();
        public uint counterHSS_3 = new uint();
        public uint counterHSS_4 = new uint();
        public uint counterDB_mb = new uint();
        public uint counter_mb = new uint();

        public float[] values_sum = new float[200];
        public float[] values_last = new float[200];
        public DateTime date_time_last = new DateTime();

        public float[] values_sum_S7 = new float[200];
        public float[] values_last_S7 = new float[200];
        public DateTime date_time_last_S7 = new DateTime();

        public int seconds_last = new int();
        public int minutes_last = new int();
        public int hours_last = new int();
        public int days_last = new int();

        public int seconds_last_mb = new int();
        public int minutes_last_mb = new int();
        public int hours_last_mb = new int();
        public int days_last_mb = new int();

        string alarmMessagesArchivePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\messageArchive.txt";
        #endregion

        T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        public MainScreen()
        {
            InitializeComponent();
            backgroundWorkerRead.WorkerReportsProgress = true;
            backgroundWorkerRead.WorkerSupportsCancellation = true;
            backgroundWorkerRead.DoWork += new DoWorkEventHandler(BackgroundWorkerRead_DoWork);
            backgroundWorkerRead.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorkerRead_RunWorkerCompleted);

            bgWMessages.WorkerReportsProgress = true;
            bgWMessages.WorkerSupportsCancellation = true;
            bgWMessages.DoWork += new DoWorkEventHandler(BgWMessages_DoWork);
            bgWMessages.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgWMessages_RunWorkerCompleted);

            #region messages Text

            //GameObject[] houses = InitializeArray<GameObject>(200);

            stdMessage[] stdMessages = InitializeArray<stdMessage>(numberOfMessages);

            //Alarm - 🟥; Warning - 🟧; Info - 🟦
            for (int i = 0; i < numberOfMessages; i++) {
                stdMessages[i].text = "🟥 AlarmReserve_" + i;
            }

            stdMessages[0].text = "🟥 ВКУ. Низкое давление на входе";

            stdMessages[25].text = "🟥 Котёл 1. Неисправность котла";
            stdMessages[26].text = "🟥 Котёл 2. Неисправность котла";
            stdMessages[27].text = "🟥 Котёл 3. Неисправность котла";
            stdMessages[34].text = "🟥 Котельная. Неисправность питательного насоса 1";
            stdMessages[36].text = "🟥 Котельная. Неисправность питательного насоса 2";
            stdMessages[38].text = "🟥 Котельная. Неисправность питательного насоса 3";
            stdMessages[39].text = "🟥 Котельная. Неисправность питательного насоса 4";
            stdMessages[40].text = "🟥 Минимальное давление в деаэраторе";
            stdMessages[8].text = "🟥 Котельная. Ввод 1. Отключение электропитания";
            stdMessages[9].text = "🟥 Котельная. Ввод 2. Отключение электропитания";
            stdMessages[62].text = "🟥 Котельная. Превышен порог загазованности СО2";
            stdMessages[51].text = "🟥 Котельная. Превышен порог загазовaнности СН. Порог 1";
            stdMessages[49].text = "🟥 Котельная. Превышен порог загазовaнности СО. Порог 1";
            stdMessages[48].text = "🟥 Котельная. Пожар";
            stdMessages[63].text = "🟥 Просадка давления пара";

            stdMessages[67].text = "🟥 Низкое давление технической воды";

            stdMessages[73].text = "🟥 Низкий уровень в танке FWT";

            stdMessages[60].text = "🟥 АХУ. Несправность одного из насосов оборотной воды";
            stdMessages[61].text = "🟥 АХУ. Несправность одного из КД";

            stdMessages[58].text = "🟥 АХУ. Аварийная остановка";

            stdMessages[57].text = "🟥 АХУ. Неисправность гликолевого насоса внутреннего контура";
            stdMessages[56].text = "🟥 АХУ. Неисправность гликолевого насоса внешнего контура";

            #endregion

            for (int i = 0; i < stdMessages.Length; i++)
            {
                if (!string.IsNullOrEmpty(stdMessages[i].text))
                {
                    stdMessages[i].text = stdMessages[i].text.Replace("(", "\\(");
                    stdMessages[i].text = stdMessages[i].text.Replace(")", "\\)");
                    stdMessages[i].text = stdMessages[i].text.Replace(":", "\\:");
                    stdMessages[i].text = stdMessages[i].text.Replace(".", "\\.");
                    stdMessages[i].text = stdMessages[i].text.Replace(",", "\\,");

                }
            }
        }

        public class stdS7Adress
        {
            public String ipAdress;
            public int rack;
            public int slot;
            public int dBNumber;
            public int startPosition;
            public int size;
        }

        public void CustomException(Exception ex, String module)
        {
            var trace = new StackTrace(ex, true);

            foreach (var frame in trace.GetFrames())
            {
                var sb = new StringBuilder();

                sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                sb.Append($"Метод: {frame.GetMethod()}");

                try
                {
                    using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                        sw.Write(module + DateTime.Now + "; " + sb + ";\n");
                }
                catch (Exception exe)
                {
                    using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                        sw.Write(module + DateTime.Now + "; " + exe + ";\n");
                }
            }
        }

        public class messageArrayState
        {
            public int reusultConnection;
            public bool[] messageStates;
        };

        public messageArrayState Read(stdS7Adress stdS7)
        {
            try
            {
                messageArrayState[] mesArrState = InitializeArray<messageArrayState>(0);

                mesArrState[0].messageStates = new bool[stdS7.size * 8];

                // Установка соединения с PLC S7
                S7Client plcClient = new S7Client();
                int result = plcClient.ConnectTo(stdS7.ipAdress, stdS7.rack, stdS7.slot);

                byte[] DBBuffer = new byte[stdS7.size];

                result = plcClient.DBRead(stdS7.dBNumber, stdS7.startPosition, stdS7.size, DBBuffer);
                if (result != 0)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                            sw.Write("Messages; " + DateTime.Now + "; " + plcClient.ErrorText(result) + " # of error message -> " + result.ToString() + " --- " + ";\n");
                        plcClient.Disconnect();
                        for (int i = 0; i < stdS7.size * 8; i++)
                        {
                            mesArrState[0].messageStates[i] = false;
                        }
                        mesArrState[0].reusultConnection = result;
                        return mesArrState[0];
                    }
                    catch (Exception ex)
                    {
                        plcClient.Disconnect();
                        MessageBox.Show(ex.Message);
                        for (int i = 0; i < stdS7.size * 8; i++)
                        {
                            mesArrState[0].messageStates[i] = false;
                        }
                        mesArrState[0].reusultConnection = result;
                        return mesArrState[0];
                    }
                }
                else
                {
                    for (int i = 0; i < stdS7.size; i++)
                    {
                        for (int j = 0; j <= 7; j++)
                        {
                            bool bit = S7.GetBitAt(DBBuffer, i, j);
                            mesArrState[0].messageStates[i * 8 + j] = bit;
                        }
                    }
                    mesArrState[0].reusultConnection = result;
                }

                plcClient.Disconnect();
                return mesArrState[0];

            }
            catch (Exception ex)
            {
                CustomException(ex, "Siemens");

                messageArrayState mesArrState = new messageArrayState();
                for (int i = 0; i < stdS7.size * 8; i++)
                {
                    mesArrState.messageStates[i] = false;
                }
                mesArrState.reusultConnection = 100;
                return mesArrState;
            }
        }


        /*

        bool[] createMessage = new bool[numberOfMessages];

                    for (int i = 0; i<currentMessageState.Length; i++)
                    {
                        if (previousMessageState[i] != currentMessageState[i] && currentMessageState[i] == true)
                        {
                            previousMessageState[i] = currentMessageState[i];
                            createMessage[i] = true;
                            messageType[i] = "⬆️";
                            messageTime[i] = DateTime.Now;
                        }
                        else if (previousMessageState[i] != currentMessageState[i] && currentMessageState[i] == false)
                        {
                            previousMessageState[i] = currentMessageState[i];
                            createMessage[i] = true;
                            messageType[i] = "⬇️";
                            messageDuration[i] = DateTime.Now.Subtract(messageTime[i]);
                        }
                    }

                    if (firstScan)
{
    for (int i = 0; i < createMessage.Length; i++)
    {
        if (createMessage[i] == true)
        {
            try
            {
                if (bgWMessages.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    bgWMessages.RunWorkerAsync(argument: i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    createMessage = null;
}
        */

        // Read S7
        private void Button_Read_s7_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorkerRead.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    backgroundWorkerRead.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_notRead_s7_Click(object sender, EventArgs e)
        {
            try
            {
                if (backgroundWorkerRead.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerRead.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void ReadWriteS7()
        {
            stdS7Adress newStdS7Adress = new stdS7Adress();

            newStdS7Adress.ipAdress = "192.168.0.1";
            newStdS7Adress.rack = 0;
            newStdS7Adress.slot = 1;
            newStdS7Adress.dBNumber = 5;
            newStdS7Adress.startPosition = 1;
            newStdS7Adress.size = 2;

            messageArrayState nowRead = Read(newStdS7Adress);

            List<bool> listBools = new List<bool>();

            foreach (bool b in nowRead.messageStates) {
                listBools.Add(b);
            }
        }
        private void BackgroundWorkerRead_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    ReadWriteS7();
                }
            }
        }
        private void BackgroundWorkerRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Value = 0));
            progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Blocks));
        }
        // Work With Messages
        private void BgWMessages_DoWork(object sender, DoWorkEventArgs e)
        {
            WriteMessages((int)e.Argument);
        }
        public async void WriteMessages(int i)
        {
            ///// Messages Telegramm /////
            try
            {
                var webProxy = new WebProxy(Host: "10.129.24.100", Port: 8080)
                {
                    // Credentials if needed:
                    // Credentials = new NetworkCredential("USERNAME", "PASSWORD")
                };
                var httpClient = new HttpClient(new HttpClientHandler { Proxy = webProxy, UseProxy = true });
                var botClient = new TelegramBotClient("5211488879:AAEy5YGotJ1bK-vyegu1DaUVI-XDh98vCT4", httpClient);
                /*
                if (messageType[i] == "⬆️")
                {
                    Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
                    chatId: "-1001749496684",//chatId,
                    text: messageType[i] + messageText[i],
                    parseMode: ParseMode.MarkdownV2,
                    disableNotification: true);
                }
                else
                {
                    string duration = " (Длительность: " + Math.Round(messageDuration[i].TotalSeconds, 2) + " с)";
                    duration = duration.Replace("(", "\\(");
                    duration = duration.Replace(")", "\\)");
                    duration = duration.Replace(":", "\\:");
                    duration = duration.Replace(".", "\\.");
                    duration = duration.Replace(",", "\\,");
                    Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
                    chatId: "-1001749496684",//chatId,
                    text: messageType[i] + messageText[i] + duration,
                    parseMode: ParseMode.MarkdownV2,
                    disableNotification: true);
                }
                */
            }
            catch (Exception ex)
            {
                CustomException(ex, "Telegramm");
            }
        }
        private void BgWMessages_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /*
             Something interesting comes next 
            */
        }
    }
}