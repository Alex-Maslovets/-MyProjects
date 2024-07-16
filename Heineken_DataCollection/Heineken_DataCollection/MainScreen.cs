using Lextm.SharpSnmpLib;
using Modbus.Device;
using Newtonsoft.Json.Linq;
using Npgsql;
using Sharp7;
using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Heineken_DataCollection
{
    public partial class MainScreen : Form
    {
        ///////////////////////////////// TEST /////////////////////////////////
        public int testCounter = new int();

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
        public uint counterPackaging_mb = new uint();
        public uint counterBLO_mb = new uint();
        public uint counterVAO_mb = new uint();
        public uint counterEnergoBlock_mb = new uint();
        public uint counterFiltration_mb = new uint();
        public uint counterElectroCounters_mb = new uint();
        public uint counterDB_mb = new uint();
        public uint countermb = new uint();


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

            bgWReadModBus.WorkerReportsProgress = true;
            bgWReadModBus.WorkerSupportsCancellation = true;
            bgWReadModBus.DoWork += new DoWorkEventHandler(BgWReadModBus_DoWork);
            bgWReadModBus.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgWReadModBus_RunWorkerCompleted);

            bgWMessages.WorkerReportsProgress = true;
            bgWMessages.WorkerSupportsCancellation = true;
            bgWMessages.DoWork += new DoWorkEventHandler(BgWMessages_DoWork);
            bgWMessages.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgWMessages_RunWorkerCompleted);

            // Alarm - 🟥; Warning - 🟧; Info - 🟦
            messageText[0] = "🟥 Alarm Reserve 0";
            messageText[1] = "🟥 Alarm Reserve 1";
            messageText[2] = "🟥 Alarm Reserve 2";
            messageText[3] = "🟥 Alarm Reserve 3";
            messageText[4] = "🟥 Alarm Reserve 4";
            messageText[5] = "🟥 Показания СО2 прибора ZIROX \\>\\= 10\\ ppm";
            messageText[6] = "🟥 Нажата аварийная кнопка в аммиачном/СО2 отделении";
            messageText[7] = "🟥 Уровень в отделители NH3 \\>\\= 45\\%";
            messageText[8] = "🟥 Уровень в отделители NH3 \\<\\= 10\\%";
            messageText[9] = "🟥 Уровень наполнения газгольдера \\>\\= 95\\%";
            messageText[10] = "🟥 Давление в танке хранения СО2 \\>\\= 17 Бар";
            messageText[11] = "🟥 Давление на подаче NH3 \\>\\= 6\\,5 Бар";
            messageText[12] = "🟥 Давление конденсации \\>\\= 13\\,5 Бар";
            messageText[13] = "🟥 Просадка давления пара";
            messageText[14] = "🟥 Неисправность аммиачного компрессора №1";
            messageText[15] = "🟥 Неисправность аммиачного компрессора №2";
            messageText[16] = "🟥 Неисправность аммиачного компрессора №3";
            messageText[17] = "🟥 Отключение электроэнергии";
            messageText[18] = "🟥 Уровень воды в баке оборотного водоснабжения \\>\\= 95\\%";
            messageText[19] = "🟥 Уровень воды в баке оборотного водоснабжения \\<\\= 25\\%";
            messageText[20] = "🟥 Температура ледяной воды \\>\\= 7\\,0 град\\. С";
            messageText[21] = "🟥 Температура ледяной воды \\<\\= 0\\,0 град\\. С";
            messageText[22] = "🟥 Все насосы на водоподготовке отключены";
            messageText[23] = "🟥 Давление воды после водоподготовки ниже уставки";

            for (int i = 0; i < messageText.Length; i++)
            {
                if (!string.IsNullOrEmpty(messageText[i]))
                {
                    messageText_SMS[i] = messageText[i].Replace("\\", "");
                    //messageText_SMS[i] = messageText_SMS[i].Replace("🟥", "Aвария:");
                    //messageText_SMS[i] = messageText_SMS[i].Replace("🟧", "Предупреждение:");
                    //messageText_SMS[i] = messageText_SMS[i].Replace("🟦", "Инфо:");
                }
            }
        }

        // Read S7
        private void Button_Read_s7_Click(object sender, EventArgs e)
        {
            counterTime = 0;
            counterPLC3679 = 0;
            counterPLC2 = 0;
            counterMessages = 0;
            counterDB = 0;
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
            counterTime = 0;
            counterPLC3679 = 0;
            counterPLC2 = 0;
            counterMessages = 0;
            counterDB = 0;
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
        public async void ReadwriteS7()
        {

            DateTime s1 = DateTime.Now;

            // Установка соединения с PostgreSQL
            NpgsqlConnection PGCon = new NpgsqlConnection(//"Host=10.129.20.253;" +
                "Host=localhost;" +
                "Username=postgres;" +
                "Password=123456;" +
                "Database=postgres;" +
                "Timeout = 300;" +
                "CommandTimeout = 300");

            try
            {
                PGCon.Open();
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);

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
                            sw.Write("Siemens; " + DateTime.Now + "; " + sb + ";\n");
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }
                PGCon.Close();
            }

            if (PGCon.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    DateTime s2 = DateTime.Now;
                    if (!firstStart)
                    {
                        var cmd_select_time = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "SELECT date_time FROM _minutes_table ORDER BY date_time DESC LIMIT 1"
                        };
                        NpgsqlDataReader reader = cmd_select_time.ExecuteReader();
                        reader.Read();

                        seconds_last = reader.GetDateTime(0).Second;
                        minutes_last = reader.GetDateTime(0).Minute;
                        hours_last = reader.GetDateTime(0).Hour;
                        days_last = reader.GetDateTime(0).Day;

                        counterTime++;
                    }
                    firstStart = true;

                    TimeSpan s3 = DateTime.Now.Subtract(s2);

                    timeLabel_s7_1.Invoke(new Action(() => timeLabel_s7_1.Text = "Время обнов. даты: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterTime));

                    s2 = DateTime.Now;

                    List<string> myList = new List<string>();

                    byte[] db1Buffer = new byte[128];

                    // Установка соединения с PLC в щитовой ТП-3679
                    S7Client plcClient = new S7Client();
                    int result = plcClient.ConnectTo("10.129.31.147", 0, 2);

                    result = plcClient.DBRead(20, 0, 128, db1Buffer);
                    if (result != 0)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                                sw.Write("S7; " + DateTime.Now + "; " + plcClient.ErrorText(result) + ";\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= 31; i++)
                        {
                            double db1ddd4 = S7.GetRealAt(db1Buffer, 4 * i);
                            myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                        counterPLC3679++;
                    }

                    plcClient.Disconnect();

                    s3 = DateTime.Now.Subtract(s2);

                    timeLabel_s7_2.Invoke(new Action(() => timeLabel_s7_2.Text = "Время PLC_3679: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterPLC3679));

                    s2 = DateTime.Now;

                    // Соединение и считывание данных с контроллера в энергоблоке
                    result = plcClient.ConnectTo("10.129.31.134", 0, 3);
                    byte[] db2Buffer = new byte[160];

                    result = plcClient.DBRead(4000, 1838, 160, db2Buffer);
                    if (result != 0)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                                sw.Write("S7; " + DateTime.Now + "; " + plcClient.ErrorText(result) + ";\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        for (int i = 32; i <= 71; i++)
                        {
                            double db2ddd4 = S7.GetRealAt(db2Buffer, 4 * (i - 32));
                            myList.Add("(" + i + "," + db2ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                        counterPLC2++;
                    }

                    s3 = DateTime.Now.Subtract(s2);

                    timeLabel_s7_3.Invoke(new Action(() => timeLabel_s7_3.Text = "Время PLC_2: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterPLC2));

                    s2 = DateTime.Now;

                    ////////// Работа с сообщениями //////////

                    db2Buffer = new byte[3];

                    result = plcClient.DBRead(4000, 8, 3, db2Buffer);
                    if (result != 0)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                                sw.Write("Messages; " + DateTime.Now + "; " + plcClient.ErrorText(result) + ";\n");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        bool[] createMessage = new bool[numberOfMessage];

                        for (int i = 0; i < db2Buffer.Length; i++)
                        {
                            for (int j = 0; j <= 7; j++)
                            {
                                bool bit = S7.GetBitAt(db2Buffer, i, j);
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
                        counterMessages++;
                    }

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    plcClient.Disconnect();

                    s3 = DateTime.Now.Subtract(s2);

                    timeLabel_s7_4.Invoke(new Action(() => timeLabel_s7_4.Text = "Время сообщений: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterMessages));

                    s2 = DateTime.Now;

                    var sqlValues = String.Join(", ", myList.ToArray());

                    int seconds_now = DateTime.Now.Second;
                    int minutes_now = DateTime.Now.Minute;
                    int hours_now = DateTime.Now.Hour;
                    int days_now = DateTime.Now.Day;

                    if (seconds_now != seconds_last)
                    {
                        seconds_last = seconds_now;
                        // Запись данных в PostgreSQL 1 раз в 1 секунду
                        var cmd_sec_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _seconds_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_sec_insert.ExecuteNonQuery();
                    }
                    if (minutes_now != minutes_last && seconds_now >= 4)
                    {
                        minutes_last = minutes_now;
                        // Запись данных в PostgreSQL 1 раз в 1 минуту
                        var cmd_min_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _minutes_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_min_insert.ExecuteNonQuery();
                    }
                    if (hours_now != hours_last && seconds_now >= 4)
                    {
                        hours_last = hours_now;
                        // Запись данных в PostgreSQL 1 раз в 1 час
                        var cmd_hour_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _hours_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_hour_insert.ExecuteNonQuery();
                    }
                    if (days_now != days_last && seconds_now >= 4)
                    {
                        days_last = days_now;
                        // Запись данных в PostgreSQL 1 раз в 1 день
                        var cmd_day_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _days_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_day_insert.ExecuteNonQuery();
                    }

                    counterDB++;

                    s3 = DateTime.Now.Subtract(s2);

                    timeLabel_s7_5.Invoke(new Action(() => timeLabel_s7_5.Text = "Время записи: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterDB));

                    progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Marquee));

                    counterS7++;

                    timeLabel_s7.Invoke(new Action(() => timeLabel_s7.Text = "Время последнего цикла: " + Math.Round(DateTime.Now.Subtract(s1).TotalMilliseconds, 0) + " мс Счётчик: " + counterS7));

                }
                catch (Exception ex)
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
                                sw.Write("Siemens; " + DateTime.Now + "; " + sb + ";\n");
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show(exe.Message);
                        }
                    }

                }
            }
            // Закрытие соединения с PostgreSQL
            PGCon.Close();
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
                    ReadwriteS7();
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
            ///// Messages SMS /////
            string[] telephoneNumbers = new string[50];

            telephoneNumbers[0] = "+79659906230";
            telephoneNumbers[1] = "+79835143748";

            foreach (string phoneNumber in telephoneNumbers)
            {
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    // Prepare target
                    UdpTarget target = new UdpTarget((IPAddress)new IpAddress("10.129.31.118"));
                    // Create a SET PDU

                    Pdu pdu = new Pdu(PduType.Set);
                    pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.2.0"), new SnmpSharpNet.OctetString(phoneNumber));

                    if (messageType[i] == "⬆️")
                    {
                        pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.1.0"), new SnmpSharpNet.OctetString(messageType[i] + messageText_SMS[i]));
                    }
                    else
                    {
                        pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.1.0"), new SnmpSharpNet.OctetString(messageType[i] + messageText_SMS[i] + " (Длительность: " + Math.Round(messageDuration[i].TotalSeconds, 2) + " с)"));
                    }
                    pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.3.0"), new SnmpSharpNet.Integer32(1));

                    // Set Agent security parameters
                    AgentParameters aparam = new AgentParameters(SnmpVersion.Ver2, new SnmpSharpNet.OctetString("public"));
                    // Response packet
                    SnmpV2Packet response;
                    
                    try
                    {
                        // Send request and wait for response
                        response = target.Request(pdu, aparam) as SnmpV2Packet;
                    }
                    catch (Exception ex)
                    {
                        // If exception happens, it will be returned here
                        using (StreamWriter sw = new StreamWriter(alarmMessagesArchivePath, true, System.Text.Encoding.Default))
                            sw.Write("Messages SMS; " + DateTime.Now + "; " + "Request failed with exception: {0}", ex.Message + ";\n");

                        target.Close();
                        //return;
                    }
                    finally
                    {

                    }
                }
            }
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
                            sw.Write("Messages Telegram; " + DateTime.Now + "; " + sb + ";\n");
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }
            }
        }
        private void BgWMessages_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /*
             Something interesting comes next 
             */
        }

        // Read Modbus
        private void Button_Read_mb_Click(object sender, EventArgs e)
        {
            counterTime_mb = 0;
            counterPackaging_mb = 0;
            counterBLO_mb = 0;
            counterVAO_mb = 0;
            counterEnergoBlock_mb = 0;
            counterFiltration_mb = 0;
            counterElectroCounters_mb = 0;
            counterDB_mb = 0;
            countermb = 0;

            try
            {
                if (bgWReadModBus.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    bgWReadModBus.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_notRead_mb_Click(object sender, EventArgs e)
        {
            counterTime_mb = 0;
            counterPackaging_mb = 0;
            counterBLO_mb = 0;
            counterVAO_mb = 0;
            counterEnergoBlock_mb = 0;
            counterFiltration_mb = 0;
            counterElectroCounters_mb = 0;
            counterDB_mb = 0;
            countermb = 0;

            try
            {
                if (bgWReadModBus.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    bgWReadModBus.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ReadwriteModbus()
        {
            // Установка соединения с PostgreSQL
            NpgsqlConnection PGCon = new NpgsqlConnection(
                "Host=localhost;" +
                "Username=postgres;" +
                "Password=123456;" +
                "Database=postgres;" +
                "Timeout = 300;" +
                "CommandTimeout = 300");

            try
            {
                PGCon.Open();
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);

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
                            sw.Write("Modbus; " + DateTime.Now + "; " + sb + ";\n");
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }
                PGCon.Close();
            }

            if (PGCon.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    DateTime s1 = DateTime.Now;
                    DateTime s2 = DateTime.Now;
                    if (!firstStartMB)
                    {
                        var cmd_select_time = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "SELECT date_time FROM _minutes_table_mb ORDER BY date_time DESC LIMIT 1"
                        };

                        NpgsqlDataReader reader = cmd_select_time.ExecuteReader();
                        reader.Read();

                        seconds_last_mb = reader.GetDateTime(0).Second;
                        minutes_last_mb = reader.GetDateTime(0).Minute;
                        hours_last_mb = reader.GetDateTime(0).Hour;
                        days_last_mb = reader.GetDateTime(0).Day;
                        counterTime_mb++;
                    }
                    firstStartMB = true;

                    TimeSpan s3 = DateTime.Now.Subtract(s2);

                    timeLabel_mb_1.Invoke(new Action(() => timeLabel_mb_1.Text = "Время обнов. даты: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterTime_mb));

                    s2 = DateTime.Now;

                    // Connect to Packaging
                    TcpClient client = new TcpClient("10.129.31.165", 502);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                    List<ushort> modbusList = new List<ushort>();

                    for (int i = 0; i <= 29; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(1, startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    List<float> values = new List<float>();

                    for (int j = 0; j <= 29; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterPackaging_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_2.Invoke(new Action(() => timeLabel_mb_2.Text = "Время TH_Pack: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterPackaging_mb));
                    s2 = DateTime.Now;

                    // Connect to BLO --- Propogators
                    client = new TcpClient("10.129.31.162", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 9; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(1, startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    for (int j = 0; j <= 9; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterBLO_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_3.Invoke(new Action(() => timeLabel_mb_3.Text = "Время TH_BLO: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterBLO_mb));
                    s2 = DateTime.Now;

                    // Connect to VAO
                    client = new TcpClient("10.129.31.163", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 49; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(1, startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    for (int j = 0; j <= 49; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterVAO_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_4.Invoke(new Action(() => timeLabel_mb_4.Text = "Время TH_VAO: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterVAO_mb));
                    s2 = DateTime.Now;

                    // Connect to EnergyBlock --- WaterReady
                    client = new TcpClient("10.129.31.164", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 9; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(1, startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    for (int j = 0; j <= 9; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterEnergoBlock_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_5.Invoke(new Action(() => timeLabel_mb_5.Text = "Время TH_EnBlock: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterEnergoBlock_mb));
                    s2 = DateTime.Now;

                    // Connect to Filtration
                    client = new TcpClient("10.129.31.161", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 29; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(1, startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    for (int j = 0; j <= 29; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterFiltration_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_6.Invoke(new Action(() => timeLabel_mb_6.Text = "Время TH_Filtr: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterFiltration_mb));
                    s2 = DateTime.Now;

                    // Connect to New Electro Counters
                    /*
                    string[] electroCounters = new string[50];

                    electroCounters[0] = "10.129.31.179";
                    electroCounters[1] = "10.129.31.178";
                    electroCounters[2] = "10.129.31.177";
                    electroCounters[3] = "10.129.31.176";
                    electroCounters[4] = "10.129.31.175";

                    foreach (string electroCounter in electroCounters)
                    {
                        if (!string.IsNullOrEmpty(electroCounter))
                        {
                            try {
                                client = new TcpClient(electroCounter, 502);
                                master = ModbusIpMaster.CreateIp(client);

                                modbusList.Clear();

                                for (int i = 0; i <= 3; i++)
                                {
                                    ushort startAddress = (ushort)(287 + i);
                                    ushort[] inputs = master.ReadHoldingRegisters(1, startAddress, 1);
                                    modbusList.Add(inputs[0]);
                                }

                                for (int j = 0; j <= 3; j += 2)
                                {
                                    ushort temp = (ushort)(modbusList[j] + modbusList[j + 1] * 10000);
                                    values.Add(temp);
                                }
                            }
                            catch (Exception ex)
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
                                            sw.Write("Modbus_Electro; " + DateTime.Now + "; " + sb + ";\n");
                                    }
                                    catch (Exception exe)
                                    {
                                        MessageBox.Show(exe.Message);
                                    }
                                }
                            }
                        }
                    }

                    counterElectroCounters_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_8.Invoke(new Action(() => timeLabel_mb_8.Text = "Время Electro: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterElectroCounters_mb));
                    s2 = DateTime.Now;
                    */
                    List<string> myList = new List<string>();

                    int x = values.Count / 5;

                    for (int i = 4, j = 0; j < x; i += 5, j++)
                    {
                        values.RemoveAt(i - j);
                    }

                    for (int i = 0; i < values.Count; i++)
                    {
                        myList.Add("(" + i + "," + values[i].ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                    }

                    var sqlValues = String.Join(", ", myList.ToArray());

                    int seconds_now = DateTime.Now.Second;
                    int minutes_now = DateTime.Now.Minute;
                    int hours_now = DateTime.Now.Hour;
                    int days_now = DateTime.Now.Day;

                    if (seconds_now != seconds_last_mb)
                    {
                        seconds_last_mb = seconds_now;
                        // Запись данных в PostgreSQL 1 раз в 1 секунду
                        var cmd_sec_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _seconds_table_mb (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_sec_insert.ExecuteNonQuery();
                    }
                    if (minutes_now != minutes_last_mb && seconds_now >= 4)
                    {
                        minutes_last_mb = minutes_now;
                        // Запись данных в PostgreSQL 1 раз в 1 минуту
                        var cmd_min_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _minutes_table_mb (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_min_insert.ExecuteNonQuery();
                    }
                    if (hours_now != hours_last_mb && seconds_now >= 4)
                    {
                        hours_last_mb = hours_now;
                        // Запись данных в PostgreSQL 1 раз в 1 час
                        var cmd_hour_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _hours_table_mb (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_hour_insert.ExecuteNonQuery();
                    }
                    if (days_now != days_last_mb && seconds_now >= 4)
                    {
                        days_last_mb = days_now;
                        // Запись данных в PostgreSQL 1 раз в 1 день
                        var cmd_day_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _days_table_mb (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_day_insert.ExecuteNonQuery();
                    }

                    counterDB_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_7.Invoke(new Action(() => timeLabel_mb_7.Text = "Время записи: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterDB_mb));
                    s2 = DateTime.Now;

                    progressBarRead_mb.Invoke(new Action(() => progressBarRead_mb.Style = ProgressBarStyle.Marquee));

                    countermb++;

                    timeLabel_mb.Invoke(new Action(() => timeLabel_mb.Text = "Время последнего цикла: " + Math.Round(DateTime.Now.Subtract(s1).TotalMilliseconds, 0) + " мс Счётчик: " + countermb));

                }
                catch (Exception ex)
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
                                sw.Write("Modbus; " + DateTime.Now + "; " + sb + ";\n");
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show(exe.Message);
                        }
                    }
                }
            }
            // Закрытие соединений с PostgreSQL
            PGCon.Close();
        }
        private void BgWReadModBus_DoWork(object sender, DoWorkEventArgs e)
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
                    ReadwriteModbus();
                }
            }
        }
        private void BgWReadModBus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarRead_mb.Invoke(new Action(() => progressBarRead_mb.Value = 0));
            progressBarRead_mb.Invoke(new Action(() => progressBarRead_mb.Style = ProgressBarStyle.Blocks));
        }
    }
}