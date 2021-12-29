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
                    try
                    {
                        DateTime s1 = DateTime.Now;

                        // Установка соединения с PLC
                        S7Client plcClient = new S7Client();
                        int result = plcClient.ConnectTo("10.129.31.147", 0, 2);

                        if (result != 0)
                        {
                            string v = plcClient.ErrorText(result);
                            string errorText = v;
                        }

                        // Установка соединения с PostgreSQL
                        NpgsqlConnection PGCon = new NpgsqlConnection("Host=10.129.20.179;Username=postgres;Password=123456;Database=postgres");
                        PGCon.Open();
                        
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

                        // Закрытие соединений с PLC и PostgreSQL
                        plcClient.Disconnect();
                        PGCon.Close();
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        string writePath = @"C:\Users\admin\Desktop\messageArchive.txt";
                        string text = ex.Message;
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                                sw.WriteLine(text + DateTime.Now);
                        }
                        catch (Exception exe)
                        {
                            MessageBox.Show(exe.Message);
                        }
                    }
                }
            }
        }
        private void BackgroundWorkerRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Value = 0));
            progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Blocks));
        }

        // Tест для Modbus
        private void Button_Read_mb_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime s1 = DateTime.Now;
                TcpClient client = new TcpClient("192.168.255.15", 502);
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                List<ushort> modbusList = new List<ushort>();

                // read eight input values
                //for (int i = 0; i <= 7; i++)
                //{
                //ushort startAddress = (ushort)(1 + 15 * i);
                //ushort numInputs = 1;
                //ushort[] inputs = master.ReadHoldingRegisters(startAddress, 1);
                //modbusList.Add(inputs[0]);
                //}

                for (int i = 0; i <= 19; i++)
                {
                    ushort startAddress = (ushort)(1301 + i);
                    //ushort numInputs = 1;
                    ushort[] inputs = master.ReadInputRegisters(startAddress, 1);
                    modbusList.Add(inputs[0]);
                }

                    timeLabel_mb.Invoke(new Action(() => timeLabel_mb.Text = "Время последнего цикла: " + DateTime.Now.Subtract(s1)));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BgWReadModBus_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
        }

        private void BgWReadModBus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Value = 0));
            //progressBarRead_s7.Invoke(new Action(() => progressBarRead_s7.Style = ProgressBarStyle.Blocks));
        }
    }
}
