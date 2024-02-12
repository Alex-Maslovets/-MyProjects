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
        const int numberOfMessage = 200;
        bool[] previousMessageState = new bool[numberOfMessage];
        DateTime[] messageTime = new DateTime[numberOfMessage];
        TimeSpan[] messageDuration = new TimeSpan[numberOfMessage];
        string[] messageText = new string[numberOfMessage];
        string[] messageText_SMS = new string[numberOfMessage];

        bool[] currentMessageState = new bool[numberOfMessage];
        string[] messageType = new string[numberOfMessage];

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
            // Alarm - 🟥; Warning - 🟧; Info - 🟦
            messageText[0] = "🟥 AlarmReserve_0";
            messageText[1] = "🟥 AlarmReserve_1";
            messageText[2] = "🟥 AlarmReserve_2";
            messageText[3] = "🟥 AlarmReserve_3";
            messageText[4] = "🟥 AlarmReserve_4";
            messageText[5] = "🟥 AlarmReserve_5";
            messageText[6] = "🟥 AlarmReserve_6";
            messageText[7] = "🟥 AlarmReserve_7";
            messageText[8] = "🟥 AlarmReserve_8";
            messageText[9] = "🟥 AlarmReserve_9";
            messageText[10] = "🟥 AlarmReserve_10";
            messageText[11] = "🟥 AlarmReserve_11";
            messageText[12] = "🟥 AlarmReserve_12";
            messageText[13] = "🟥 AlarmReserve_13";
            messageText[14] = "🟥 AlarmReserve_14";
            messageText[15] = "🟥 AlarmReserve_15";
            messageText[16] = "🟥 AlarmReserve_16";
            messageText[17] = "🟥 AlarmReserve_17";
            messageText[18] = "🟥 AlarmReserve_18";
            messageText[19] = "🟥 AlarmReserve_19";
            messageText[20] = "🟥 AlarmReserve_20";
            messageText[21] = "🟥 AlarmReserve_21";
            messageText[22] = "🟥 AlarmReserve_22";
            messageText[23] = "🟥 AlarmReserve_23";
            messageText[24] = "🟥 AlarmReserve_24";
            messageText[25] = "🟥 AlarmReserve_25";
            messageText[26] = "🟥 AlarmReserve_26";
            messageText[27] = "🟥 AlarmReserve_27";
            messageText[28] = "🟥 AlarmReserve_28";
            messageText[29] = "🟥 AlarmReserve_29";
            messageText[30] = "🟥 AlarmReserve_30";
            messageText[31] = "🟥 AlarmReserve_31";
            messageText[32] = "🟥 AlarmReserve_32";
            messageText[33] = "🟥 AlarmReserve_33";
            messageText[34] = "🟥 AlarmReserve_34";
            messageText[35] = "🟥 AlarmReserve_35";
            messageText[36] = "🟥 AlarmReserve_36";
            messageText[37] = "🟥 AlarmReserve_37";
            messageText[38] = "🟥 AlarmReserve_38";
            messageText[39] = "🟥 AlarmReserve_39";
            messageText[40] = "🟥 AlarmReserve_40";
            messageText[41] = "🟥 AlarmReserve_41";
            messageText[42] = "🟥 AlarmReserve_42";
            messageText[43] = "🟥 AlarmReserve_43";
            messageText[44] = "🟥 AlarmReserve_44";
            messageText[45] = "🟥 AlarmReserve_45";
            messageText[46] = "🟥 AlarmReserve_46";
            messageText[47] = "🟥 AlarmReserve_47";
            messageText[48] = "🟥 AlarmReserve_48";
            messageText[49] = "🟥 AlarmReserve_49";
            messageText[50] = "🟥 AlarmReserve_50";
            messageText[51] = "🟥 AlarmReserve_51";
            messageText[52] = "🟥 AlarmReserve_52";
            messageText[53] = "🟥 AlarmReserve_53";
            messageText[54] = "🟥 AlarmReserve_54";
            messageText[55] = "🟥 AlarmReserve_55";
            messageText[56] = "🟥 AlarmReserve_56";
            messageText[57] = "🟥 AlarmReserve_57";
            messageText[58] = "🟥 AlarmReserve_58";
            messageText[59] = "🟥 AlarmReserve_59";
            messageText[60] = "🟥 AlarmReserve_60";
            messageText[61] = "🟥 AlarmReserve_61";
            messageText[62] = "🟥 AlarmReserve_62";
            messageText[63] = "🟥 AlarmReserve_63";
            messageText[64] = "🟥 AlarmReserve_64";
            messageText[65] = "🟥 AlarmReserve_65";
            messageText[66] = "🟥 AlarmReserve_66";
            messageText[67] = "🟥 AlarmReserve_67";
            messageText[68] = "🟥 AlarmReserve_68";
            messageText[69] = "🟥 AlarmReserve_69";
            messageText[70] = "🟥 AlarmReserve_70";
            messageText[71] = "🟥 AlarmReserve_71";
            messageText[72] = "🟥 AlarmReserve_72";
            messageText[73] = "🟥 AlarmReserve_73";
            messageText[74] = "🟥 AlarmReserve_74";
            messageText[75] = "🟥 AlarmReserve_75";
            messageText[76] = "🟥 AlarmReserve_76";
            messageText[77] = "🟥 AlarmReserve_77";
            messageText[78] = "🟥 AlarmReserve_78";
            messageText[79] = "🟥 AlarmReserve_79";
            messageText[80] = "🟥 AlarmReserve_80";
            messageText[81] = "🟥 AlarmReserve_81";
            messageText[82] = "🟥 AlarmReserve_82";
            messageText[83] = "🟥 AlarmReserve_83";
            messageText[84] = "🟥 AlarmReserve_84";
            messageText[85] = "🟥 AlarmReserve_85";
            messageText[86] = "🟥 AlarmReserve_86";
            messageText[87] = "🟥 AlarmReserve_87";
            messageText[88] = "🟥 AlarmReserve_88";
            messageText[89] = "🟥 AlarmReserve_89";
            messageText[90] = "🟥 AlarmReserve_90";
            messageText[91] = "🟥 AlarmReserve_91";
            messageText[92] = "🟥 AlarmReserve_92";
            messageText[93] = "🟥 AlarmReserve_93";
            messageText[94] = "🟥 AlarmReserve_94";
            messageText[95] = "🟥 AlarmReserve_95";
            messageText[96] = "🟥 AlarmReserve_96";
            messageText[97] = "🟥 AlarmReserve_97";
            messageText[98] = "🟥 AlarmReserve_98";
            messageText[99] = "🟥 AlarmReserve_99";
            messageText[100] = "🟥 AlarmReserve_100";


            messageText[0] = "🟥 ВКУ. Низкое давление на входе";

            messageText[25] = "🟥 Котёл 1. Неисправность котла";
            messageText[26] = "🟥 Котёл 2. Неисправность котла";
            messageText[27] = "🟥 Котёл 3. Неисправность котла";
            messageText[34] = "🟥 Котельная. Неисправность питательного насоса 1";
            messageText[36] = "🟥 Котельная. Неисправность питательного насоса 2";
            messageText[38] = "🟥 Котельная. Неисправность питательного насоса 3";
            messageText[39] = "🟥 Котельная. Неисправность питательного насоса 4";
            messageText[40] = "🟥 Минимальное давление в деаэраторе";
            messageText[8] = "🟥 Котельная. Ввод 1. Отключение электропитания";
            messageText[9] = "🟥 Котельная. Ввод 2. Отключение электропитания";
            messageText[62] = "🟥 Котельная. Превышен порог загазованности СО2";
            messageText[51] = "🟥 Котельная. Превышен порог загазовaнности СН. Порог 1";
            messageText[49] = "🟥 Котельная. Превышен порог загазовaнности СО. Порог 1";
            messageText[48] = "🟥 Котельная. Пожар";
            messageText[63] = "🟥 Просадка давления пара";

            messageText[67] = "🟥 Низкое давление технической воды";

            messageText[73] = "🟥 Низкий уровень в танке FWT";

            messageText[60] = "🟥 АХУ. Несправность одного из насосов оборотной воды";
            messageText[61] = "🟥 АХУ. Несправность одного из КД";

            messageText[58] = "🟥 АХУ. Аварийная остановка";

            messageText[57] = "🟥 АХУ. Неисправность гликолевого насоса внутреннего контура";
            messageText[56] = "🟥 АХУ. Неисправность гликолевого насоса внешнего контура";
            #endregion

            for (int i = 0; i < messageText.Length; i++)
            {
                if (!string.IsNullOrEmpty(messageText[i]))
                {
                    messageText[i] = messageText[i].Replace("(", "\\(");
                    messageText[i] = messageText[i].Replace(")", "\\)");
                    messageText[i] = messageText[i].Replace(":", "\\:");
                    messageText[i] = messageText[i].Replace(".", "\\.");
                    messageText[i] = messageText[i].Replace(",", "\\,");

                }
            }
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

            try
            {
                // Установка соединения с PLC S7-416 10.129.32.72
                S7Client plcClient = new S7Client();
                int result = plcClient.ConnectTo("10.129.32.72", 0, 3);

                byte[] DB1Buffer = new byte[2];

                result = plcClient.DBRead(21, 6, 2, DB1Buffer);
                if (result != 0)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                            sw.Write("Messages; " + DateTime.Now + "; " + plcClient.ErrorText(result) + " # of error message -> " + result.ToString() + " --- " + ";\n");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    bool[] createMessage = new bool[numberOfMessage];

                    for (int i = 0; i < DB1Buffer.Length; i++)
                    {
                        for (int j = 0; j <= 7; j++)
                        {
                            bool bit = S7.GetBitAt(DB1Buffer, i, j);
                            currentMessageState[i * 8 + j] = bit;
                        }
                    }

                    for (int i = 0; i < currentMessageState.Length; i++)
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
                    firstScan = true;
                }

                plcClient.Disconnect();

                progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Marquee));

            }
            catch (Exception ex)
            {
                CustomException(ex, "Siemens");
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