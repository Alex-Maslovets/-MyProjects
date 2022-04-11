using Modbus.Device;
using Npgsql;
using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
//using Telegram.Bot.Exceptions;
//using Telegram.Bot.Extensions.Polling;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.ReplyMarkups;
//using System.Threading;

namespace Heineken_DataCollection
{
    public partial class MainScreen : Form
    {

        const int numberOfMessage = 50;
        bool[] previousMessageState = new bool[numberOfMessage];
        string[] messageText = new string[numberOfMessage];
        bool firstScan = false;

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
            messageText[5] = "🟥 Alarm Reserve 5";
            messageText[6] = "🟥 Нажата аварийная кнопка в аммиачном/СО2 отделении";
            messageText[7] = "🟥 Уровень в отделители NH3 \\>\\= 40\\%";
            messageText[8] = "🟥 Уровень в отделители NH3 \\<\\= 10\\%";
            messageText[9] = "🟥 Уровень наполнения газгольдера \\>\\= 95\\%";
            messageText[10] = "🟥 Давление в танке хранения СО2 \\>\\= 16 Бар";
            messageText[11] = "🟥 Давление на подаче NH3 \\>\\= 6 Бар";
            messageText[12] = "🟥 Давление конденсации \\>\\= 13\\,5 Бар";
            messageText[13] = "🟥 Просадка давления пара";
            messageText[14] = "🟥 Неисправность аммиачного компрессора №1";
            messageText[15] = "🟥 Неисправность аммиачного компрессора №2";
            messageText[16] = "🟥 Неисправность аммиачного компрессора №3";
            messageText[17] = "🟥 Отключение электроэнергии";
            messageText[18] = "🟥 Уровень воды в баке оборотного водоснабжения \\>\\= 95\\%";
            messageText[19] = "🟥 Уровень воды в баке оборотного водоснабжения \\<\\= 25\\%";
            messageText[20] = "🟥 Температура ледяной воды \\>\\= 6\\,5 град\\. С";
            messageText[21] = "🟥 Температура ледяной воды \\<\\= 1\\,8 град\\. С";
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
        public void ReadwriteS7()
        {

            // Установка соединения с PostgreSQL
            NpgsqlConnection PGCon = new NpgsqlConnection("Host=10.129.20.179;" +
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

                string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";

                foreach (var frame in trace.GetFrames())
                {
                    var sb = new StringBuilder();

                    sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                    sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                    sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                    sb.Append($"Метод: {frame.GetMethod()}");

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
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
                    DateTime s1 = DateTime.Now;

                    List<string> myList = new List<string>();

                    byte[] db1Buffer = new byte[128];

                    // Установка соединения с PLC в щитовой ТП-3679
                    S7Client plcClient = new S7Client();
                    int result = plcClient.ConnectTo("10.129.31.147", 0, 2);

                    result = plcClient.DBRead(20, 0, 128, db1Buffer);
                    if (result != 0)
                    {
                        Console.WriteLine("Error: " + plcClient.ErrorText(result));
                    }
                    else
                    {
                        for (int i = 0; i <= 31; i++)
                        {
                            double db1ddd4 = S7.GetRealAt(db1Buffer, 4 * i);
                            myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                    }

                    plcClient.Disconnect();

                    // Соединение и считывание данных с контроллера в энергоблоке
                    result = plcClient.ConnectTo("10.129.31.135", 0, 3);
                    byte[] db2Buffer = new byte[64];

                    result = plcClient.DBRead(2000, 1838, 64, db2Buffer);
                    if (result != 0)
                    {
                        Console.WriteLine("Error: " + plcClient.ErrorText(result));
                    }
                    else
                    {
                        for (int i = 32; i <= 45; i++)
                        {
                            double db2ddd4 = S7.GetRealAt(db2Buffer, 4 * (i - 32));
                            myList.Add("(" + i + "," + db2ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                    }

                    plcClient.Disconnect();

                    var sqlValues = String.Join(", ", myList.ToArray());

                    // Запиись данных в PostgreSQL
                    var cmd_insert = new NpgsqlCommand
                    {
                        Connection = PGCon,
                        CommandText = "INSERT INTO _temp_table (id, value, date_time) VALUES " + sqlValues
                    };
                    cmd_insert.ExecuteNonQuery();

                    progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Marquee));

                    timeLabel_s7.Invoke(new Action(() => timeLabel_s7.Text = "Время последнего цикла: " + DateTime.Now.Subtract(s1)));

                }
                catch (Exception ex)
                {
                    var trace = new StackTrace(ex, true);

                    string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";

                    foreach (var frame in trace.GetFrames())
                    {
                        var sb = new StringBuilder();

                        sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                        sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                        sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                        sb.Append($"Метод: {frame.GetMethod()}");

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
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
        // Read Modbus
        private void Button_Read_mb_Click(object sender, EventArgs e)
        {
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
            NpgsqlConnection PGCon = new NpgsqlConnection("Host=10.129.20.179;" +
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

                string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";

                foreach (var frame in trace.GetFrames())
                {
                    var sb = new StringBuilder();

                    sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                    sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                    sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                    sb.Append($"Метод: {frame.GetMethod()}");

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
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
                    DateTime s2 = DateTime.Now;
                    // Connect to Packaging
                    DateTime s1 = DateTime.Now;
                    TcpClient client = new TcpClient("10.129.31.160", 502);
                    ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                    List<ushort> modbusList = new List<ushort>();

                    for (int i = 0; i <= 29; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
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

                    // Connect to BLO --- Propogators
                    s1 = DateTime.Now;
                    client = new TcpClient("10.129.31.162", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 9; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
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

                    // Connect to VAO
                    s1 = DateTime.Now;
                    client = new TcpClient("10.129.31.163", 502);
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

                    // Connect to EnergyBlock --- WaterReady
                    s1 = DateTime.Now;
                    client = new TcpClient("10.129.31.164", 502);
                    master = ModbusIpMaster.CreateIp(client);

                    modbusList.Clear();

                    for (int i = 0; i <= 9; i++)
                    {
                        ushort startAddress = (ushort)(1301 + i);
                        ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
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


                    // Connect to Filtration
                    s1 = DateTime.Now;
                    client = new TcpClient("10.129.31.161", 502);
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

                    // Запиись данных в PostgreSQL
                    var cmd_insert = new NpgsqlCommand
                    {
                        Connection = PGCon,
                        CommandText = "INSERT INTO _temp_table_mb (id, value, date_time) VALUES " + sqlValues
                    };
                    cmd_insert.ExecuteNonQuery();

                    progressBarRead_mb.Invoke(new Action(() => progressBarRead_mb.Style = ProgressBarStyle.Marquee));

                    timeLabel_mb.Invoke(new Action(() => timeLabel_mb.Text = "Время последнего цикла: " + DateTime.Now.Subtract(s2)));

                }
                catch (Exception ex)
                {
                    var trace = new StackTrace(ex, true);

                    string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";

                    foreach (var frame in trace.GetFrames())
                    {
                        var sb = new StringBuilder();

                        sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                        sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                        sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                        sb.Append($"Метод: {frame.GetMethod()}");

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
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
        // Telegram Messages
        private void ButtonMessages_Click(object sender, EventArgs e)
        {
            try
            {
                if (bgWMessages.IsBusy != true)
                {
                    // Start the asynchronous operation
                    bgWMessages.RunWorkerAsync();
                }
                else
                {
                    try
                    {
                        if (bgWReadModBus.WorkerSupportsCancellation == true)
                        {
                            // Cancel the asynchronous operation
                            bgWMessages.CancelAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BgWMessages_DoWork(object sender, DoWorkEventArgs e)
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
                    ReadMessages();
                }
            }
        }
        private void BgWMessages_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ButtonMessages.Invoke(new Action(() => ButtonMessages.Text = "Пуск"));
            progressBar_Messages.Invoke(new Action(() => progressBar_Messages.Value = 0));
            progressBar_Messages.Invoke(new Action(() => progressBar_Messages.Style = ProgressBarStyle.Blocks));
        }
        public async void ReadMessages()
        {
            try
            {

                DateTime s1 = DateTime.Now;

                bool[] currentMessageState = new bool[numberOfMessage];
                bool[] createMessage = new bool[numberOfMessage];
                string[] messageType = new string[numberOfMessage];
                
                S7Client plcClient = new S7Client();
                // Соединение и считывание данных с контроллера в энергоблоке
                int result = plcClient.ConnectTo("192.168.127.150", 0, 1);
                byte[] db2Buffer = new byte[3];

                result = plcClient.DBRead(2000, 23, 3, db2Buffer);
                if (result != 0)
                {
                    Console.WriteLine("Error: " + plcClient.ErrorText(result));
                }
                else
                {
                    for (int i = 0; i < db2Buffer.Length; i++)
                    {
                        for (int j = 0; j <= 7; j++)
                        {
                            bool bit = S7.GetBitAt(db2Buffer, i, j);
                            currentMessageState[i * 8 + j] = bit;
                        }
                    }
                }

                plcClient.Disconnect();

                for (int i = 0; i < currentMessageState.Length; i++)
                {
                    if (previousMessageState[i] != currentMessageState[i] && currentMessageState[i] == true)
                    {
                        previousMessageState[i] = currentMessageState[i];
                        createMessage[i] = true;
                        messageType[i] = "⬆️";
                    }
                    else if (previousMessageState[i] != currentMessageState[i] && currentMessageState[i] == false)
                    {
                        previousMessageState[i] = currentMessageState[i];
                        createMessage[i] = true;
                        messageType[i] = "⬇️";
                    }
                }

                if (firstScan)
                {
                    for (int i = 0; i < createMessage.Length; i++)
                    {
                        if (createMessage[i] == true)
                        {
                            /*
                            var webProxy = new WebProxy(Host: "10.23.5.4", Port: 80)
                            {
                                // Credentials if needed:
                                // Credentials = new NetworkCredential("USERNAME", "PASSWORD")
                            };
                            var httpClient = new HttpClient(
                                new HttpClientHandler { Proxy = webProxy, UseProxy = true }
                            );
                
                            var botClient = new TelegramBotClient("5211488879:AAEy5YGotJ1bK-vyegu1DaUVI-XDh98vCT4", httpClient);
                            */
                            var botClient = new TelegramBotClient("5211488879:AAEy5YGotJ1bK-vyegu1DaUVI-XDh98vCT4");

                            var me = await botClient.GetMeAsync();

                            Telegram.Bot.Types.Message message = await botClient.SendTextMessageAsync(
                                chatId: "-1001749496684",//chatId,
                                text: messageType[i] + messageText[i],
                            parseMode: ParseMode.MarkdownV2,
                            disableNotification: true);
                        }
                    }
                }
                
                createMessage = null;
                firstScan = true;

                ButtonMessages.Invoke(new Action(() => ButtonMessages.Text = "Стоп"));
                progressBar_Messages.Invoke(new Action(() => progressBar_Messages.Style = ProgressBarStyle.Marquee));
                timeLabel_Messages.Invoke(new Action(() => timeLabel_Messages.Text = "Время последнего цикла: " + DateTime.Now.Subtract(s1)));
            }
            catch (Exception exe)
            {
                var trace = new StackTrace(exe, true);

                string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";

                foreach (var frame in trace.GetFrames())
                {
                    var sb = new StringBuilder();

                    sb.Append($"Файл: {frame.GetFileName()}" + "; ");
                    sb.Append($"Строка: {frame.GetFileLineNumber()}" + "; ");
                    sb.Append($"Столбец: {frame.GetFileColumnNumber()}" + "; ");
                    sb.Append($"Метод: {frame.GetMethod()}");

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                            sw.Write("Modbus; " + DateTime.Now + "; " + sb + ";\n");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public int seconds_last = new int();
        public int minutes_last = new int();
        public int hours_last = new int();
        public int days_last = new int();

        private void Button2_Click(object sender, EventArgs e)
        {


            while (true)
            {

                int seconds_now = DateTime.Now.Second;
                int minutes_now = DateTime.Now.Minute;
                int hours_now = DateTime.Now.Hour;
                int days_now = DateTime.Now.Day;


                if (seconds_now != seconds_last)
                {
                    seconds_last = seconds_now;
                    Console.WriteLine("Seconds_S:" + seconds_last);
                }
                if (minutes_now != minutes_last)
                {
                    minutes_last = minutes_now;
                    Console.WriteLine("Minutes_M:" + minutes_last);
                }
                if (hours_now != hours_last)
                {
                    hours_last = hours_now;
                    Console.WriteLine("Hours_H:" + hours_last);
                }
                if (days_now != days_last)
                {
                    days_last = days_now;
                    Console.WriteLine("Days_D:" + days_last);
                }
            }
        }
    }
}