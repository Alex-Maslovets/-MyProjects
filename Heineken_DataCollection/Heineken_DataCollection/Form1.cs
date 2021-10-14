using Npgsql;
using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.Sockets;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;

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
        }

        private void Button_Read_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerRead.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorkerRead.RunWorkerAsync();
            }
        }

        private void Button_notRead_Click(object sender, EventArgs e)
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
                        Stopwatch sw = Stopwatch.StartNew();
                        sw.Start();
                        
                        // Установка соединения с PLC
                        S7Client plcClient = new S7Client();
                        //connectionClient.S7Client = new S7Client();
                        int result = plcClient.ConnectTo("192.168.127.150", 0, 1);
                        if (result != 0) MessageBox.Show(plcClient.ErrorText(result));
                        
                        // Установка соединения с PostgreSQL
                        NpgsqlConnection PGCon = new NpgsqlConnection("Host=localhost;Username=postgres;Password=123456789;Database=postgres");
                        PGCon.Open();
                        
                        List<string> myList = new List<string>();

                        byte[] db1Buffer = new byte[800];
                        result = plcClient.DBRead(2000, 432, 800, db1Buffer);
                        if (result != 0)
                        {
                            Console.WriteLine("Error: " + plcClient.ErrorText(result));
                        }

                        for (int i = 0; i <= 199; i++) {
                            double db1ddd4 = S7.GetRealAt(db1Buffer, 4*i);
                            myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')");
                        }
                        
                        var sqlValues = String.Join(", ", myList.ToArray());
                        
                        // Запиись данных в PostgreSQL
                        var cmd_insert = new NpgsqlCommand
                        {
                            Connection = PGCon,
                            CommandText = "INSERT INTO _test_table (id, value, date_time) VALUES " + sqlValues
                        };
                        cmd_insert.ExecuteNonQuery();

                        progressBarRead.Invoke(new Action(() => progressBarRead.Style = ProgressBarStyle.Marquee));

                        sw.Stop();
                        Console.WriteLine("Read: {0:N0} ticks", sw.ElapsedTicks);

                        // Закрытие соединений с PLC и PostgreSQL
                        plcClient.Disconnect();
                        PGCon.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void BackgroundWorkerRead_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBarRead.Invoke(new Action(() => progressBarRead.Value = 0));
            progressBarRead.Invoke(new Action(() => progressBarRead.Style = ProgressBarStyle.Blocks));
        }



        // Tест для Modbus
        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            TcpClient client = new TcpClient("10.129.31.151",502);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

            // read five input values
            ushort startAddress = 0;
            ushort numInputs = 100;
            //bool[] inputs = master.ReadInputs(startAddress, numInputs);
            ushort[] inputs = master.ReadHoldingRegisters(startAddress,numInputs);
            for (int i = 0; i < numInputs; i++)
                Console.WriteLine("Input {0}={1}", startAddress + i, inputs[i]);
            sw.Stop();
            Console.WriteLine("Read: {0:N0} ticks", sw.ElapsedTicks);
        }
    }
}
