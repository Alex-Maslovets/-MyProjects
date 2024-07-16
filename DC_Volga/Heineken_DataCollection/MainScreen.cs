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
using System.Linq;

namespace Heineken_DataCollection
{
    public partial class MainScreen : Form
    {
        #region initialisation
        static int numberOfMessages = 200;

        public class stdMessage {
            public string text;
            public string type;
            public bool previousState;
            public bool currentState;
            public DateTime time;
            public TimeSpan duration;
        }

        bool firstStart = false;
        bool firstStartMB = false;

        public uint counterMessages = new uint();
        public uint counterS7 = new uint();

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

        string alarmMessagesArchivePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\messageArchive_Volga.txt";
        
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

        public stdMessage[] stdMessages = new stdMessage[numberOfMessages];

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

            stdMessages = InitializeArray<stdMessage>(numberOfMessages);

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

        public class messageArrayState_Test
        {
            public bool[] messageStates;
        };

        public messageArrayState Read(stdS7Adress stdS7)
        {
            try
            {
                
                messageArrayState[] mesArrState = InitializeArray<messageArrayState>(1);

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
                
                messageArrayState[] mesArrState = InitializeArray<messageArrayState>(0);

                mesArrState[0].messageStates = new bool[stdS7.size * 8];

                for (int i = 0; i < stdS7.size * 8; i++)
                {
                    mesArrState[0].messageStates[i] = false;
                }
                mesArrState[0].reusultConnection = 100;
                return mesArrState[0];
                
            }
        }

        public messageArrayState_Test Read_Test(stdS7Adress[] stdS7)
        {
            stdS7Adress[] internalList = stdS7;
            
            string[] ipAdresses = new string[internalList.Length];
            int[] racks = new int[internalList.Length];
            int[] slots = new int[internalList.Length];
            int[] dbNunbers = new int[internalList.Length];
            int[] strartPositions = new int[internalList.Length];
            int[] sizes = new int[internalList.Length];
            
            for (int i = 0; i < internalList.Length; i++) {
                ipAdresses[i] = internalList[i].ipAdress;
                racks[i] = internalList[i].rack;
                slots[i] = internalList[i].slot;
                dbNunbers[i] = internalList[i].dBNumber;
                strartPositions[i] = internalList[i].startPosition;
                sizes[i] = internalList[i].size;
            }

            string[] uniqIPAdresses = ipAdresses.Distinct().ToArray();
            uniqIPAdresses = uniqIPAdresses.Where(c => c != null).ToArray();
            
            int[] uniqRacks = racks.Distinct().ToArray();
            
            int[] uniqSlots = slots.Distinct().ToArray();
            uniqSlots = uniqSlots.Where(c => c != 0).ToArray();

            int[] uniqDbNumbers = dbNunbers.Distinct().ToArray();
            uniqDbNumbers = uniqDbNumbers.Where(c => c != 0).ToArray();
            
            int[] uniqStartPos = strartPositions.Distinct().ToArray();
            uniqStartPos = uniqStartPos.Where(c => c != 0).ToArray();

            int[] uniqSizes = sizes.Distinct().ToArray();
            uniqSizes = uniqSizes.Where(c => c != 0).ToArray();

            messageArrayState[] mesArrState = InitializeArray<messageArrayState>(1);

            // Установка соединения с PLC S7
            S7Client plcClient = new S7Client();
            int result = plcClient.ConnectTo(uniqIPAdresses[0], uniqRacks[0], uniqSlots[0]);


            mesArrState[0].messageStates = new bool[uniqSizes * 8];


            byte[] DBBuffer = new byte[stdS7.size];

            result = plcClient.DBRead(stdS7.dBNumber, stdS7.startPosition, stdS7.size, DBBuffer);

            messageArrayState_Test mesArrState = new messageArrayState_Test();
            return mesArrState;
        }

        // Read S7
        private void Button_Read_s7_Click(object sender, EventArgs e)
        {
            counterMessages = 0;
            counterS7 = 0;

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
            counterMessages = 0;
            counterS7 = 0;

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
        public void ReadWriteS7()
        {
            DateTime s1 = DateTime.Now;
            DateTime s2 = DateTime.Now;

            stdS7Adress[] newStdS7Adress = InitializeArray<stdS7Adress>(100);

            newStdS7Adress[0].ipAdress = "10.129.34.140";
            newStdS7Adress[0].rack = 0;
            newStdS7Adress[0].slot = 3;
            newStdS7Adress[0].dBNumber = 305;
            newStdS7Adress[0].startPosition = 180;
            newStdS7Adress[0].size = 1;

            newStdS7Adress[1].ipAdress = "10.129.34.140";
            newStdS7Adress[1].rack = 0;
            newStdS7Adress[1].slot = 3;
            newStdS7Adress[1].dBNumber = 303;
            newStdS7Adress[1].startPosition = 208;
            newStdS7Adress[1].size = 4;



            messageArrayState_Test nowRead_Test = Read_Test(newStdS7Adress);

            messageArrayState nowRead = Read(newStdS7Adress[0]);

            List<bool> currentMessagesState = new List<bool>();

            foreach (bool mState in nowRead.messageStates) {
                currentMessagesState.Add(mState);
            }
            
            nowRead = null;

            newStdS7Adress[1].ipAdress = "10.129.34.140";
            newStdS7Adress[1].rack = 0;
            newStdS7Adress[1].slot = 3;
            newStdS7Adress[1].dBNumber = 303;
            newStdS7Adress[1].startPosition = 208;
            newStdS7Adress[1].size = 4;

            nowRead = Read(newStdS7Adress[1]);

            foreach (bool mState in nowRead.messageStates)
            {
                currentMessagesState.Add(mState);
            }

            currentMessagesState.ToArray();

            if (currentMessagesState.Count > 0)
            {
                if (currentMessagesState.Count >= stdMessages.Length)
                {
                    MessageBox.Show("Считано больше статусов чем инициализировано сообщений");
                }
                else {
                    for (int i = 0; i < currentMessagesState.Count; i++)
                    {
                        stdMessages[i].currentState = currentMessagesState[i];
                    }
                }
            }
            else {
                MessageBox.Show("В массиве статусов сообщений нет данных!");
            }

            bool[] createMessage = new bool[numberOfMessages];

            label2.Invoke(new Action(() => label2.Text = "stdMessages.Length: " + stdMessages.Length.ToString()));

            for (int i = 0; i < stdMessages.Length; i++)
            {
                if (stdMessages[i].previousState != stdMessages[i].currentState && stdMessages[i].currentState == true)
                {
                    MessageBox.Show("Создали входящее сообщение");
                    stdMessages[i].previousState = stdMessages[i].currentState;
                    createMessage[i] = true;
                    stdMessages[i].type = "⬆️";
                    stdMessages[i].time = DateTime.Now;
                }
                else if (stdMessages[i].previousState != stdMessages[i].currentState && stdMessages[i].currentState == false)
                {
                    MessageBox.Show("Создали исходящее сообщение");
                    stdMessages[i].previousState = stdMessages[i].currentState;
                    createMessage[i] = true;
                    stdMessages[i].type = "⬇️";
                    stdMessages[i].duration = DateTime.Now.Subtract(stdMessages[i].time);
                }
            }

            string strCurState = null;

            for (int i = 0; i < stdMessages.Length; i++)
            {
                strCurState = strCurState + " --- " + stdMessages[i].currentState.ToString();
            }

            label1.Invoke(new Action(() => label1.Text = "CurStates: " + strCurState));


            string strPrevState = null;

            for (int i = 0; i < stdMessages.Length; i++)
            {
                strPrevState = strPrevState + " --- " + stdMessages[i].previousState.ToString();
            }

            label3.Invoke(new Action(() => label3.Text = "PrevStates: " + strPrevState));

            for (int i = 0; i < createMessage.Length; i++)
            {
                if (createMessage[i] == true)
                {
                    try
                    {
                        if (bgWMessages.IsBusy != true)
                        {
                            MessageBox.Show("Пробуем запутить сообщение в bgwMessages");
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

            TimeSpan s3 = DateTime.Now.Subtract(s2);
            counterMessages++;
            timeLabel_s7_4.Invoke(new Action(() => timeLabel_s7_4.Text = "Время записи: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterMessages));

            progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Marquee));

            counterS7++;
            timeLabel_s7.Invoke(new Action(() => timeLabel_s7.Text = "Время последнего цикла: " + Math.Round(DateTime.Now.Subtract(s1).TotalMilliseconds, 0) + " мс Счётчик: " + counterS7));

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
                MessageBox.Show("Зашли в метод WriteMessages");
                var webProxy = new WebProxy(Host: "10.129.24.100", Port: 8080)
                {
                    // Credentials if needed:
                    // Credentials = new NetworkCredential("USERNAME", "PASSWORD")
                };

                var httpClient = new HttpClient(new HttpClientHandler { Proxy = webProxy, UseProxy = true });
                var botClient = new TelegramBotClient("6464066208:AAFGlk3TY7R9qTjvLpkcTLEuNBUEHY2PELI", httpClient);
                
                if (stdMessages[i].type == "⬆️")
                {
                    Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
                    chatId: "-1001999334838",//chatId,
                    text: stdMessages[i].type + stdMessages[i].text,
                    parseMode: ParseMode.MarkdownV2,
                    disableNotification: true);
                }
                else
                {
                    string duration = " (Длительность: " + Math.Round(stdMessages[i].duration.TotalSeconds, 2) + " с)";
                    duration = duration.Replace("(", "\\(");
                    duration = duration.Replace(")", "\\)");
                    duration = duration.Replace(":", "\\:");
                    duration = duration.Replace(".", "\\.");
                    duration = duration.Replace(",", "\\,");

                    Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
                    chatId: "-1001999334838",//chatId,
                    text: stdMessages[i].type + stdMessages[i].text + duration,
                    parseMode: ParseMode.MarkdownV2,
                    disableNotification: true);
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (stdMessages[0].type == "⬆️")
            {
                stdMessages[0].type = "⬇️";
            }
            else {
                stdMessages[0].type = "⬆️";
            }
            WriteMessages(0);

        }
    }
}