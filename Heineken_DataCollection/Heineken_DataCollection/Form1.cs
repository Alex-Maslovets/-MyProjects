using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharp7;
using Npgsql;
using System.Diagnostics;

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

        public static class connectionClient {
            public static S7Client S7Client;
            public static NpgsqlConnection PGCon;
        }

        private void Button_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                // Установка соединения с PLC
                connectionClient.S7Client = new S7Client();
                int result = connectionClient.S7Client.ConnectTo("192.168.100.150", 0, 1);
                if (result == 0)
                {
                    Console.WriteLine("Connected to 192.168.100.150");
                }
                else
                {
                    MessageBox.Show(connectionClient.S7Client.ErrorText(result));
                }

                // Установка соединения с PostgreSQL
                string connectString = "Host=localhost;Username=postgre;Password=123456789;Database=_test_table";
                connectionClient.PGCon = new NpgsqlConnection(connectString);
                connectionClient.PGCon.Open();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_disConnect_Click(object sender, EventArgs e)
        {
            try {
                // Закрытие соединений с PLC и PostgreSQL
                connectionClient.S7Client.Disconnect();
                connectionClient.PGCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void Button_Read_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            if (backgroundWorkerRead.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorkerRead.RunWorkerAsync();
            }
            sw.Stop();
            Console.WriteLine("Array.Copy: {0:N0} ticks", sw.ElapsedTicks);
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
                        List<string> myList = new List<string>();
                        int result;
                        for (int i = 0; i <= 99; i++)
                        {
                            byte[] db1Buffer = new byte[4];
                            result = connectionClient.S7Client.DBRead(2000, 432 + 4 * i, 4, db1Buffer);
                            if (result != 0)
                            {
                                Console.WriteLine("Error: " + connectionClient.S7Client.ErrorText(result));
                            }

                            double db1ddd4 = S7.GetRealAt(db1Buffer, 0);
                            myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + "')");
                        }
                        var test = String.Join(", ", myList.ToArray());

                        // Запиись данных в PostgreSQL
                        var cmd_insert = new NpgsqlCommand();

                        cmd_insert.Connection = connectionClient.PGCon;
                        cmd_insert.CommandText = "INSERT INTO _test_table (id, value, date_time) VALUES " + test;
                        cmd_insert.ExecuteNonQuery();
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

        }
    }
}
