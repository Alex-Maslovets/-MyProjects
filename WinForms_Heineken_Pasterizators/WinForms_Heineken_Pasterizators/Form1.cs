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
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.IO;

namespace WinForms_Heineken_Pasterizators
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //for (int i = 1; i <= 10; i++)
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

                        string s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
                        Debug.WriteLine(s);
                        
                        // Create and connect the client
                        var client = new S7Client();
                        int result = client.ConnectTo("192.168.100.150", 0, 1);
                        if (result == 0)
                        {
                            //Console.WriteLine("Connected to 192.168.100.150");
                        }
                        else
                        {
                            Console.WriteLine(client.ErrorText(result));
                        }

                        List<bool> byte1 = new List<bool>();
                        byte1 = DB_ReadBitArray(client, 2000, 40832, 0, 8);

                        foreach (bool b in byte1)
                        {
                            Debug.WriteLine(b);
                        }
                        
                        //ResourceManager resManager = new ResourceManager("WinForms_Heineken_Pasterizators.Recources", GetType().Assembly);
                        //Image myImage = (Image)(resManager.GetObject("Valve_Vertical_1.png"));
                        //pictureBox2.Image = myImage;

                        string x = "Valve_Vertical_" + Convert.ToInt32(byte1[0]);

                        pb_KEG_V74.Image = (Image)Properties.Resources.ResourceManager.GetObject(x);

                        /*                       for (int t = 0; t < 10; t++)
                                               {
                                                   switch (pictureBox2.Tag.ToString())
                                                   {
                                                       case ("VV_0"):
                                                           pictureBox2.Image = Properties.Resources.Valve_Vertical_1;
                                                           pictureBox2.Tag = pictureBox2.Tag.ToString().Remove(pictureBox2.Tag.ToString().Length - 1, 1) + "1";
                                                           break;
                                                       case ("VV_1"):
                                                           pictureBox2.Image = Properties.Resources.Valve_Vertical_0;
                                                           pictureBox2.Tag = pictureBox2.Tag.ToString().Remove(pictureBox2.Tag.ToString().Length - 1, 1) + "0";
                                                           break;
                                                   }
                                               }
                       */
                        /*
                        List<string> myList = new List<string>();

                        for (int i = 0; i <= 99; i++)
                        {
                            byte[] db1Buffer = new byte[4];
                            result = client.DBRead(2000, 432 + 4 * i, 4, db1Buffer);
                            if (result != 0)
                            {
                                Console.WriteLine("Error: " + client.ErrorText(result));
                            }

                            double db1ddd4 = S7.GetRealAt(db1Buffer, 0);
                            myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + s + "')");
                        }
                        */
                        //var test = String.Join(", ", myList.ToArray());
                        /*
                                    byte[] db1Buffer = new byte[1600];
                                    result = client.DBRead(2000, 432, 1600, db1Buffer);
                                    if (result != 0)
                                    {
                                        Console.WriteLine("Error: " + client.ErrorText(result));
                                    }

                                    List<string> myList = new List<string>();

                                    for (int i = 0; i <= 399; i++)
                                    {
                                        double db1ddd4 = S7.GetRealAt(db1Buffer, 4*i);
                                        //Console.WriteLine("Real[" + i +"] = " + db1ddd4);
                                        myList.Add("(" + i + "," + db1ddd4.ToString().Replace(",", ".") + ",'" + s + "')");
                                    }

                                    var test = String.Join(", ", myList.ToArray());
                        */

                        //Disconnect the client
                        client.Disconnect();

                        s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
                        Debug.WriteLine(s);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    // Perform a time consuming operation and report progress.
                    Thread.Sleep(500);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }

        public List<bool> DB_ReadBitArray(S7Client client, int DBNumber, int StartByte, int StartBit, int Size)
        {
            List<bool> tempList = new List<bool>();
            try {

                int quotient = Math.DivRem(StartBit + Size, 8, out int remainder);

                byte[] DBBuffer = new byte[remainder == 0 ? quotient : quotient + 1];
                int result = client.DBRead(DBNumber, StartByte, quotient, DBBuffer);
                if (result != 0)
                {
                    Console.WriteLine("Error: (Read Bit Array from DB" + DBNumber + ".DBX" + StartByte + "." + StartBit + " Text: " + client.ErrorText(result));
                }
                for (int i = StartBit; i < StartBit + Size; i++)
                {
                    int quotientFor = Math.DivRem(i, 8, out int remainderFor);
                    int temp_Pos = quotientFor;
                    int temp_Bit = i - 8 * quotientFor;

                    bool value = S7.GetBitAt(DBBuffer, temp_Pos, temp_Bit);

                    tempList.Add(value);
                }
                return tempList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tempList.Clear();
                return tempList;
            }
            
        }
    }
}
