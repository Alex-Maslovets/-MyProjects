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

        string alarmMessagesArchivePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\messageArchive_Patra.txt";

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
            messageText[0] = "🟥 Тестовый Аlarm";
            messageText[1] = "🟥 Alarm Reserve 1";
            messageText[2] = "🟥 Alarm Reserve 2";
            messageText[3] = "🟥 Пустая канистра на водоподготовке";
            messageText[4] = "🟥 Неисправность станции дозации диоксидхлора";
            messageText[5] = "🟥 Ошибка водоподготовка";
            messageText[6] = "🟥 Низкий диоксид хлора \\<1\\,0";
            messageText[7] = "🟥 Высокий диоксид хлора \\>3\\,0";
            messageText[8] = "🟥 Уровень в отделители NH3 \\<\\= 10\\%";
            messageText[9] = "🟥 Уровень наполнения газгольдера \\>\\= 95\\%";
            messageText[10] = "🟥 Давление в танке хранения СО2 \\>\\= 16 Бар";
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

            for (int i = 0; i < messageText.Length; i++)
            {
                if (!string.IsNullOrEmpty(messageText[i]))
                {
                    messageText_SMS[i] = messageText[i].Replace("\\", "");
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
                //MessageBox.Show(ex.Message);
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
                //MessageBox.Show(ex.Message);
            }
        }
        public async void ReadWriteS7()
        {

            DateTime s1 = DateTime.Now;

            // Установка соединения с PostgreSQL
            NpgsqlConnection PGCon = new NpgsqlConnection(
                "Host=localhost;" +
                "Username=postgres;" +
                "Password=ekb271023;" +
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
                        //MessageBox.Show(exe.Message);
                    }
                }
                PGCon.Close();
            }

            if (PGCon.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    
                    DateTime s2 = DateTime.Now;

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
                           //MessageBox.Show(ex.Message);
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
                                        //MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                            createMessage = null;
                        }
                        firstScan = true;
                        counterMessages++;
                    }

                    plcClient.Disconnect();

                    TimeSpan s3 = DateTime.Now.Subtract(s2);

                    timeLabel_s7_4.Invoke(new Action(() => timeLabel_s7_4.Text = "Время сообщений: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterMessages));

                    // Задержка на опрос каждые 500 мсек
                    int sleepmsek = Math.Abs(500 - ((int)Math.Round(s3.TotalMilliseconds, 0)));
                    Thread.Sleep(sleepmsek);

                    s2 = DateTime.Now;

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
                            //MessageBox.Show(exe.Message);
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
                if (i >= 3)
                {
                    //MessageBox.Show("Зашли в метод WriteMessages - Try Telegramm");
                    var webProxy = new WebProxy(Host: "10.129.24.100", Port: 8080)
                    {
                        //Credentials if needed:
                        //Credentials = new NetworkCredential("USERNAME", "PASSWORD")
                    };
                    var httpClient = new HttpClient(new HttpClientHandler { Proxy = webProxy, UseProxy = true });
                    var botClient = new TelegramBotClient("6526345857:AAEq4ogV-4EDUjRLHfvItJFwrSprQI4gUbk", httpClient);

                    //MessageBox.Show("Telegramm Proxy и клиент сконфигурированы");

                    if (messageType[i] == "⬆️")
                    {
                        Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
                        chatId: "-1002240306880",//chatId,
                        text: messageType[i] + messageText[i],
                        parseMode: ParseMode.MarkdownV2,
                        disableNotification: true);

                        //MessageBox.Show("Telegramm Сообщение отправлено");
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
                        chatId: "-1002240306880",//chatId,
                        text: messageType[i] + messageText[i] + duration,
                        parseMode: ParseMode.MarkdownV2,
                        disableNotification: true);
                        //MessageBox.Show("Telegramm Сообщение закрывающее отправлено");
                    }
                }
                
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Зашли в метод WriteMessages - Catch Exception Telegramm Exception: --- " + ex.ToString());
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
                        //MessageBox.Show(exe.Message);
                    }
                }
            }

            ///// Messages SMS /////
            string[] telephoneNumbers = new string[50];

            telephoneNumbers[0] = "+79617698639";
            telephoneNumbers[1] = "+79120365922";
            telephoneNumbers[2] = "+79120365912";
            telephoneNumbers[3] = "+79612208424";

            foreach (string phoneNumber in telephoneNumbers)
            {
                if (i >= 3)
                {
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        // Prepare target
                        UdpTarget target = new UdpTarget((IPAddress)new IpAddress("10.129.31.118"));
                        // Create a SET PDU

                        Pdu pdu = new Pdu(PduType.Set);
                        pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.2.0"), new OctetString(phoneNumber));

                        if (messageType[i] == "⬆️")
                        {
                            pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.1.0"), new OctetString(messageType[i] + messageText_SMS[i]));
                        }
                        else
                        {
                            pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.1.0"), new SnmpSharpNet.OctetString(messageType[i] + messageText_SMS[i] + " (Длительность: " + Math.Round(messageDuration[i].TotalSeconds, 2) + " с)"));
                        }
                        pdu.VbList.Add(new Oid("1.3.6.1.4.1.21796.4.10.2.3.0"), new Integer32(1));

                        // Set Agent security parameters
                        AgentParameters aparam = new AgentParameters(SnmpVersion.Ver2, new OctetString("public"));
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
            counterHSS_0 = 0;
            counterHSS_1 = 0;
            counterHSS_1_add = 0;
            counterHSS_2 = 0;
            counterHSS_3 = 0;
            counterHSS_4 = 0;
            counterDB_mb = 0;
            counter_mb = 0;

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
                //MessageBox.Show(ex.Message);
            }
        }
        private void Button_notRead_mb_Click(object sender, EventArgs e)
        {
            counterTime_mb = 0;
            counterHSS_0 = 0;
            counterHSS_1 = 0;
            counterHSS_1_add = 0;
            counterHSS_2 = 0;
            counterHSS_3 = 0;
            counterHSS_4 = 0;
            counterDB_mb = 0;
            counter_mb = 0;

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
                //MessageBox.Show(ex.Message);
            }
        }
        public void ReadWriteModbus()
        {
            // Установка соединения с PostgreSQL
            NpgsqlConnection PGCon = new NpgsqlConnection(
                "Host=localhost;" +
                "Username=postgres;" +
                "Password=ekb271023;" +
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
                        //MessageBox.Show(exe.Message);
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
                    TimeSpan s3 = new TimeSpan();

                    if (!firstStartMB)
                    {
                        var cmd_select_time = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "SELECT date_time FROM _minutes_table ORDER BY date_time DESC LIMIT 1"
                        };

                        NpgsqlDataReader reader = cmd_select_time.ExecuteReader();
                        reader.Read();

                        seconds_last_mb = reader.GetDateTime(0).Second;
                        minutes_last_mb = reader.GetDateTime(0).Minute;
                        hours_last_mb = reader.GetDateTime(0).Hour;
                        days_last_mb = reader.GetDateTime(0).Day;
                        counterTime_mb++;

                        s3 = DateTime.Now.Subtract(s2);
                        timeLabel_mb_1.Invoke(new Action(() => timeLabel_mb_1.Text = "Время обнов. даты: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterTime_mb));
                    }
                    firstStartMB = true;
                    

                    s2 = DateTime.Now;

                    // Connect to HSS_0 (10.129.47.194)
                    TcpClient client = new TcpClient("10.129.47.194", 502);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                    List<ushort> modbusList = new List<ushort>();

                    for (int i = 0; i <= 9; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    List<float> values = new List<float>();

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

                    counterHSS_0++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_2.Invoke(new Action(() => timeLabel_mb_2.Text = "Время TH_HSS_0: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterHSS_0));
                    s2 = DateTime.Now;

                    // Connect to HSS_1 (10.129.47.197)
                    client = new TcpClient("10.129.47.197", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 39; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    for (int j = 0; j <= 39; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterHSS_1++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_3.Invoke(new Action(() => timeLabel_mb_3.Text = "Время TH_HSS_1: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterHSS_1));
                    s2 = DateTime.Now;

                    // Connect to HSS_1_add (10.129.47.195)
                    client = new TcpClient("10.129.47.195", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 29; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
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

                    counterHSS_1_add++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_4.Invoke(new Action(() => timeLabel_mb_4.Text = "Время TH_HSS_1_add: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterHSS_1_add));
                    s2 = DateTime.Now;

                    // Connect to HSS_2 (10.129.47.198)
                    client = new TcpClient("10.129.47.198", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 19; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    for (int j = 0; j <= 19; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterHSS_2++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_5.Invoke(new Action(() => timeLabel_mb_5.Text = "Время TH_HSS_2: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterHSS_2));
                    s2 = DateTime.Now;

                    // Connect to HSS_3 (10.129.47.199)
                    client = new TcpClient("10.129.47.199", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 49; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
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

                    counterHSS_3++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_6.Invoke(new Action(() => timeLabel_mb_6.Text = "Время TH_HSS_3: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterHSS_3));

                    s2 = DateTime.Now;
                    // Connect to HSS_4 (10.129.47.200)
                    client = new TcpClient("10.129.47.200", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 19; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
                        modbusList.Add(inputs[0]);
                    }

                    for (int j = 0; j <= 19; j += 2)
                    {
                        ushort[] buffer = { modbusList[j], modbusList[j + 1] };
                        byte[] bytes = new byte[4];
                        bytes[3] = (byte)(buffer[1] & 0xFF);
                        bytes[2] = (byte)(buffer[1] >> 8);
                        bytes[1] = (byte)(buffer[0] & 0xFF);
                        bytes[0] = (byte)(buffer[0] >> 8);
                        values.Add(BitConverter.ToSingle(bytes, 0));
                    }

                    counterHSS_4++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_8.Invoke(new Action(() => timeLabel_mb_8.Text = "Время TH_HSS_4: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterHSS_4));
                    
                    s2 = DateTime.Now;
                    List<string> myList = new List<string>();

                    // Удаление пятого элемента в массиве, т.к. в этом преобразователе он не несёт никакой полезной информации 
                    int x = values.Count / 5;

                    for (int i = 4, j = 0; j < x; i += 5, j++)
                    {
                        values.RemoveAt(i - j);
                    }

                    // Работа с моментальными расходами
                    if (values_last[0] != 0 || values_last[0] == 0)
                    {
                        for (int i = 0; i < values.Count; i+=4)
                        {
                            values_sum[i] = values_sum[i] + ((float)((((values[i] + values_last[i]) / 2) / 3600) * (DateTime.Now.Subtract(date_time_last).TotalSeconds)));
                            values_last[i] = values[i];
                        }
                        date_time_last = DateTime.Now;

                    }
                    else
                    {
                        for (int i = 0; i < values.Count; i++)
                        {
                            values_last[i] = values[i];
                        }
                        date_time_last = DateTime.Now;
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
                            CommandText = "INSERT INTO _seconds_table (id, value, date_time) VALUES " + sqlValues
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
                            CommandText = "INSERT INTO _minutes_table (id, value, date_time) VALUES " + sqlValues
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
                            CommandText = "INSERT INTO _hours_table (id, value, date_time) VALUES " + sqlValues
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
                            CommandText = "INSERT INTO _days_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_day_insert.ExecuteNonQuery();
                    }

                    counterDB_mb++;

                    s3 = DateTime.Now.Subtract(s2);
                    timeLabel_mb_7.Invoke(new Action(() => timeLabel_mb_7.Text = "Время записи: " + Math.Round(s3.TotalMilliseconds, 0) + " мс Счётчик: " + counterDB_mb));
                    s2 = DateTime.Now;

                    progressBarRead_mb.Invoke(new Action(() => progressBarRead_mb.Style = ProgressBarStyle.Marquee));

                    counter_mb++;

                    timeLabel_mb.Invoke(new Action(() => timeLabel_mb.Text = "Время последнего цикла: " + Math.Round(DateTime.Now.Subtract(s1).TotalMilliseconds, 0) + " мс Счётчик: " + counter_mb));

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
                            //MessageBox.Show(exe.Message);
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
                    ReadWriteModbus();
                }
            }
        }
        private void BgWReadModBus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarRead_mb.Invoke(new Action(() => progressBarRead_mb.Value = 0));
            progressBarRead_mb.Invoke(new Action(() => progressBarRead_mb.Style = ProgressBarStyle.Blocks));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (messageType[0] == "⬆️")
            {
                messageType[0] = "⬇️";
            }
            else
            {
                messageType[0] = "⬆️";
            }
            WriteMessages(0);
        }
    }
}