using Npgsql;
using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net.Sockets;
using Modbus.Data;
using Modbus.Device;
using System.IO;

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using X.Extensions.Logging.Telegram;

namespace Heineken_DataCollection
{
    public partial class Form1 : Form
    {
        public Form1()
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
        }

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
                    // Установка соединения с PLC
                    S7Client plcClient = new S7Client();
                    int result = plcClient.ConnectTo("10.129.31.147", 0, 2);

                    // Установка соединения с PostgreSQL
                    NpgsqlConnection PGCon = new NpgsqlConnection("Host=10.129.20.179;Username=postgres;Password=123456;Database=postgres");
                    PGCon.Open();

                    try
                    {
                        DateTime s1 = DateTime.Now;
                        
                        List<string> myList = new List<string>();

                        byte[] db1Buffer = new byte[128];
                        
                        result = plcClient.DBRead(20, 0, 128, db1Buffer);
                        if (result != 0)
                        {
                            Console.WriteLine("Error: " + plcClient.ErrorText(result));
                        }
                        
                        for (int i = 0; i <= 31; i++) {
                            double db1ddd4 = S7.GetRealAt(db1Buffer, 4*i);
                            myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                        
                        plcClient.Disconnect();

                        // Соединение и считывание данных с контроллера в энергоблоке
                        result = plcClient.ConnectTo("10.129.31.135", 0, 3);
                        byte[] db2Buffer = new byte[20];

                        result = plcClient.DBRead(2000, 1558, 20, db2Buffer);
                        if (result != 0)
                        {
                            Console.WriteLine("Error: " + plcClient.ErrorText(result));
                        }

                        for (int i = 32; i <= 36; i++)
                        {
                            double db2ddd4 = S7.GetRealAt(db2Buffer, 4 * (i-32));
                            myList.Add("(" + i + "," + db2ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }

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
                        //MessageBox.Show(ex.Message);
                        string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";
                        string text = ex.Message;
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                                //sw.WriteLine(text + DateTime.Now);
                                sw.Write(DateTime.Now + "; " + text + ";\n");
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show(exe.Message);
                        }
                    }
                    // Закрытие соединений с PLC и PostgreSQL
                    plcClient.Disconnect();
                    PGCon.Close();
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
                    // Установка соединения с PostgreSQL
                    NpgsqlConnection PGCon = new NpgsqlConnection("Host=10.129.20.179;Username=postgres;Password=123456;Database=postgres");
                    PGCon.Open();

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
                        string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";
                        string text = ex.Message;
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                                sw.Write(DateTime.Now + "; " + text + ";\n");
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show(exe.Message);
                        }
                    }

                    // Закрытие соединений с PLC и PostgreSQL
                    PGCon.Close();
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
            {
                var options = new TelegramLoggerOptions
                {
                    AccessToken = "1234567890:AAAaaAAaa_AaAAaa-AAaAAAaAAaAaAaAAAA",
                    ChatId = "-0000000000000",
                    LogLevel = LogLevel.Information,
                    Source = "TEST APP",
                    UseEmoji = true
                };

                var factory = LoggerFactory.Create(builder =>
                {
                    builder
                        .ClearProviders()
                        .AddTelegram(options)
                        .AddConsole(); ;
                }
                );

                var logger1 = factory.CreateLogger<ExampleClass>();
                var logger2 = factory.CreateLogger<AnotherExampleClass>();

                for (var i = 0; i < 1; i++)
                {
                    logger1.LogTrace($"Message {i}");
                    logger2.LogDebug($"Debug message text {i}");
                    logger1.LogInformation($"Information message text {i}");

                    try
                    {
                        throw new SystemException("Exception message description. <br /> This message contains " +
                                                  "<html> <tags /> And some **special** symbols _");
                    }
                    catch (Exception exception)
                    {
                        logger2.LogWarning(exception, $"Warning message text {i}");
                        logger1.LogError(exception, $"Error message  text {i}");
                        logger2.LogCritical(exception, $"Critical error message  text {i}");
                    }

                    Task.WaitAll(Task.Delay(500));
                }


                Console.WriteLine("Hello World!");
                Console.ReadKey();
            }
    }
}
