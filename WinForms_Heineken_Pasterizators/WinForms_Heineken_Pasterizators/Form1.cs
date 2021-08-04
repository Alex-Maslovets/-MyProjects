using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Sharp7;
using System.Threading;
using System.Diagnostics;

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
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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

                        string s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
                        DateTime s1 = DateTime.Now;
                        Debug.WriteLine(s);
                        
                        // Create and connect the client
                        var client = new S7Client();
                        int result = client.ConnectTo("192.168.127.55", 0, 2);
                        if (result == 0)
                        {
                            //Console.WriteLine("Connected to 192.168.100.150");
                        }
                        else
                        {
                            Console.WriteLine(client.ErrorText(result));
                        }

                        // --- Work with Bits --- //
                        List<bool> bitsInBytes = new List<bool>();
                        //bitsInBytes = DB_ReadBitArray(client, 2000, 40832, 0, 32);

                        /* Images names:
                        1) 2PosValves_
                        2.1) Indicator_
                        2.2) Indicator_Alarm_
                        2.3) Indicator_Warn_
                        3) Pump_Down_
                        4) Pump_Left_
                        5) Pump_Right_
                        6) RegValve_Horizont_
                        7) RegValve_Vertical_
                        8) SafeValve_Down_
                        9) SafeValve_Left_
                        10) SafeValve_Right_
                        11) Valve_Horizont_
                        12) Valve_Vertical_
                        13) Valve23_
                         */

                        // --- KEGS --- //
                        bitsInBytes = DB_ReadBitArray(client, 100, 1232, 0, 32);

                        pb_KEG_V73.Image = (Image)Properties.Resources.ResourceManager.GetObject("2PosValves_" + Convert.ToInt32(bitsInBytes[2]));
                        pb_KEG_V72.Image = (Image)Properties.Resources.ResourceManager.GetObject("2PosValves_" + Convert.ToInt32(bitsInBytes[3]));
                        pb_KEG_V71.Image = (Image)Properties.Resources.ResourceManager.GetObject("2PosValves_" + Convert.ToInt32(bitsInBytes[4]));
                        pb_KEG_M03.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Down_" + Convert.ToInt32(bitsInBytes[5]));
                        pb_KEG_M04.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Left_" + Convert.ToInt32(bitsInBytes[7]));

                        pb_KEG_V69.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Left_" + Convert.ToInt32(bitsInBytes[8]));
                        pb_KEG_M21.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Down_" + Convert.ToInt32(bitsInBytes[9]));
                        pb_KEG_V22.Image = (Image)Properties.Resources.ResourceManager.GetObject("RegValve_Vertical_" + Convert.ToInt32(bitsInBytes[10]));
                        pb_KEG_V23.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[11]));
                        pb_KEG_V15_Paster.Image = (Image)Properties.Resources.ResourceManager.GetObject("RegValve_Vertical_" + Convert.ToInt32(bitsInBytes[12]));
                        pb_KEG_V05.Image = (Image)Properties.Resources.ResourceManager.GetObject("RegValve_Horizont_" + Convert.ToInt32(bitsInBytes[12]));
                        pb_KEG_V74.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[14]));
                        pb_KEG_V86.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[15]));
                        
                        pb_KEG_V87.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Down_" + Convert.ToInt32(bitsInBytes[16]));
                        pb_KEG_V75.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[17]));
                        pb_KEG_V20.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[18]));
                        pb_KEG_V14.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[19]));
                        pb_KEG_INDICAT03.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[20]));
                        pb_KEG_INDICAT04.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[21]));
                        pb_KEG_INDICAT05.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[22]));
                        pb_KEG_INDICAT06.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[23]));
                        
                        pb_KEG_V23_Buffer.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve23_" + Convert.ToInt32(bitsInBytes[24]));
                        pb_KEG_V22_Buffer.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Right_" + Convert.ToInt32(bitsInBytes[25]));
                        pb_KEG_V24.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[26]));
                        pb_KEG_V25.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[27]));
                        pb_KEG_V15.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[28]));
                        pb_KEG_V16.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[29]));
                        pb_KEG_INDICAT02.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[30]));
                        pb_KEG_M30.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Right_" + Convert.ToInt32(bitsInBytes[31]));

                        // --- PETS --- //
                        bitsInBytes.Clear();
                        bitsInBytes = DB_ReadBitArray(client, 100, 1232, 0, 32);

                        pb_PET_V70.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[0]));
                        pb_PET_V71.Image = (Image)Properties.Resources.ResourceManager.GetObject("2PosValves_" + Convert.ToInt32(bitsInBytes[1]));
                        pb_PET_V72.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[2]));
                        pb_PET_M03.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Down_" + Convert.ToInt32(bitsInBytes[3]));
                        pb_PET_M04.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Left_" + Convert.ToInt32(bitsInBytes[4]));
                        pb_PET_V69.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Left_" + Convert.ToInt32(bitsInBytes[5]));
                        pb_PET_M21.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Down_" + Convert.ToInt32(bitsInBytes[6]));
                        pb_PET_V22.Image = (Image)Properties.Resources.ResourceManager.GetObject("RegValve_Vertical_" + Convert.ToInt32(bitsInBytes[7]));
                        
                        pb_PET_V23.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[8]));
                        pb_PET_V05.Image = (Image)Properties.Resources.ResourceManager.GetObject("RegValve_Horizont_" + Convert.ToInt32(bitsInBytes[9]));
                        pb_PET_V85.Image = (Image)Properties.Resources.ResourceManager.GetObject("2PosValves_" + Convert.ToInt32(bitsInBytes[10]));
                        pb_PET_V86.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[11]));
                        pb_PET_V87.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Down_" + Convert.ToInt32(bitsInBytes[12]));
                        pb_PET_V20.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[13]));
                        pb_PET_V14.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[14]));
                        pb_PET_V15.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[15]));

                        pb_PET_V16.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[16]));
                        pb_PET_INDICAT03.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[17]));
                        pb_PET_INDICAT04.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[18]));
                        pb_PET_INDICAT05.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[19]));
                        pb_PET_INDICAT06.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[20]));
                        pb_PET_V22_Buffer.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Right_" + Convert.ToInt32(bitsInBytes[21]));
                        pb_PET_V23_Buffer.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve23_" + Convert.ToInt32(bitsInBytes[22]));
                        pb_PET_V24.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[23]));

                        pb_PET_V25.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[24]));
                        pb_PET_INDICAT02.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[25]));
                        pb_PET_M30.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Right_" + Convert.ToInt32(bitsInBytes[26]));

                        // --- GLASS --- //
                        bitsInBytes.Clear();
                        bitsInBytes = DB_ReadBitArray(client, 100, 1232, 0, 32);

                        pb_GLASS_V4539.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[0]));
                        pb_GLASS_V4538.Image = (Image)Properties.Resources.ResourceManager.GetObject("2PosValves_" + Convert.ToInt32(bitsInBytes[1]));
                        pb_GLASS_V4544.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[2]));
                        pb_GLASS_M4503.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Down_" + Convert.ToInt32(bitsInBytes[3]));
                        pb_GLASS_M4504.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Left_" + Convert.ToInt32(bitsInBytes[4]));
                        pb_GLASS_V4519.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Left_" + Convert.ToInt32(bitsInBytes[5]));
                        pb_GLASS_V4522.Image = (Image)Properties.Resources.ResourceManager.GetObject("RegValve_Vertical_" + Convert.ToInt32(bitsInBytes[6]));
                        pb_GLASS_V4523.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[7]));


                        pb_GLASS_M4521.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Down_" + Convert.ToInt32(bitsInBytes[8]));
                        pb_GLASS_V4505.Image = (Image)Properties.Resources.ResourceManager.GetObject("RegValve_Horizont_" + Convert.ToInt32(bitsInBytes[9]));
                        pb_GLASS_V5509.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[10]));
                        pb_GLASS_V5503.Image = (Image)Properties.Resources.ResourceManager.GetObject("2PosValves_" + Convert.ToInt32(bitsInBytes[11]));
                        pb_GLASS_V5514.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[12]));
                        pb_GLASS_V5520.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[13]));
                        pb_GLASS_INDICAT06.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[14]));
                        pb_GLASS_INDICAT05.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[15]));

                        pb_GLASS_INDICAT04.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[16]));
                        pb_GLASS_INDICAT02.Image = (Image)Properties.Resources.ResourceManager.GetObject("Indicator_" + Convert.ToInt32(bitsInBytes[17]));
                        pb_GLASS_V5516.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[18]));
                        pb_GLASS_V5515.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[19]));
                        pb_GLASS_M5519.Image = (Image)Properties.Resources.ResourceManager.GetObject("Pump_Right_" + Convert.ToInt32(bitsInBytes[20]));
                        pb_GLASS_V5521.Image = (Image)Properties.Resources.ResourceManager.GetObject("SafeValve_Right_" + Convert.ToInt32(bitsInBytes[21]));
                        pb_GLASS_V5524.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Horizont_" + Convert.ToInt32(bitsInBytes[22]));
                        pb_GLASS_V5525.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve_Vertical_" + Convert.ToInt32(bitsInBytes[23]));

                        //pb_GLASS_V5523.Image = (Image)Properties.Resources.ResourceManager.GetObject("Valve23_" + Convert.ToInt32(bitsInBytes[22]));

                        // --- Work with Reals --- //
                        List<double> realsFromS7 = new List<double>();
                        //realsFromS7 = DB_ReadRealArray(client, 2000, 432, 13);
                        // --- KEGS --- //
                        realsFromS7 = DB_ReadRealArray(client, 100, 432, 13);

                        AI_KEG_14PT.Invoke(new Action(() => AI_KEG_14PT.Text = realsFromS7[0].ToString()));
                        AI_KEG_08FT.Invoke(new Action(() => AI_KEG_08FT.Text = realsFromS7[1].ToString()));
                        AI_KEG_11PT.Invoke(new Action(() => AI_KEG_11PT.Text = realsFromS7[3].ToString()));
                        AI_KEG_10TT.Invoke(new Action(() => AI_KEG_10TT.Text = realsFromS7[4].ToString()));
                        AI_KEG_12PT.Invoke(new Action(() => AI_KEG_12PT.Text = realsFromS7[5].ToString()));
                        AI_KEG_PE.Invoke(new Action(() => AI_KEG_PE.Text = realsFromS7[6].ToString()));
                        AI_KEG_24TT.Invoke(new Action(() => AI_KEG_24TT.Text = realsFromS7[7].ToString()));
                        AI_KEG_17TT.Invoke(new Action(() => AI_KEG_17TT.Text = realsFromS7[8].ToString()));
                        AI_KEG_01QT.Invoke(new Action(() => AI_KEG_01QT.Text = realsFromS7[9].ToString()));
                        AI_KEG_07PT.Invoke(new Action(() => AI_KEG_07PT.Text = realsFromS7[10].ToString()));
                        AI_KEG_65PT.Invoke(new Action(() => AI_KEG_65PT.Text = realsFromS7[11].ToString()));

                        // --- PETS --- //
                        realsFromS7.Clear();
                        realsFromS7 = DB_ReadRealArray(client, 100, 432, 13);

                        AI_PET_14PT.Invoke(new Action(() => AI_PET_14PT.Text = realsFromS7[0].ToString()));
                        AI_PET_08FT.Invoke(new Action(() => AI_PET_08FT.Text = realsFromS7[1].ToString()));
                        AI_PET_11PT.Invoke(new Action(() => AI_PET_11PT.Text = realsFromS7[3].ToString()));
                        AI_PET_10TT.Invoke(new Action(() => AI_PET_10TT.Text = realsFromS7[4].ToString()));
                        AI_PET_12PT.Invoke(new Action(() => AI_PET_12PT.Text = realsFromS7[5].ToString()));
                        AI_PET_PE.Invoke(new Action(() => AI_PET_PE.Text = realsFromS7[6].ToString()));
                        AI_PET_24TT.Invoke(new Action(() => AI_PET_24TT.Text = realsFromS7[7].ToString()));
                        AI_PET_17TT.Invoke(new Action(() => AI_PET_17TT.Text = realsFromS7[8].ToString()));
                        AI_PET_01QT.Invoke(new Action(() => AI_PET_01QT.Text = realsFromS7[9].ToString()));
                        AI_PET_07PT.Invoke(new Action(() => AI_PET_07PT.Text = realsFromS7[10].ToString()));
                        AI_PET_65PT.Invoke(new Action(() => AI_PET_65PT.Text = realsFromS7[11].ToString()));

                        // --- GLASS --- //
                        realsFromS7.Clear();
                        realsFromS7 = DB_ReadRealArray(client, 100, 432, 13);

                        AI_GLASS_14PT.Invoke(new Action(() => AI_GLASS_14PT.Text = realsFromS7[0].ToString()));
                        AI_GLASS_08FT.Invoke(new Action(() => AI_GLASS_08FT.Text = realsFromS7[1].ToString()));
                        AI_GLASS_10TT.Invoke(new Action(() => AI_GLASS_10TT.Text = realsFromS7[2].ToString()));
                        AI_GLASS_12PT.Invoke(new Action(() => AI_GLASS_12PT.Text = realsFromS7[3].ToString()));
                        AI_GLASS_PE.Invoke(new Action(() => AI_GLASS_PE.Text = realsFromS7[4].ToString()));
                        AI_GLASS_24TT.Invoke(new Action(() => AI_GLASS_24TT.Text = realsFromS7[5].ToString()));
                        AI_GLASS_01QT.Invoke(new Action(() => AI_GLASS_01QT.Text = realsFromS7[6].ToString()));
                        AI_GLASS_07PT.Invoke(new Action(() => AI_GLASS_07PT.Text = realsFromS7[7].ToString()));
                        AI_GLASS_65PT.Invoke(new Action(() => AI_GLASS_65PT.Text = realsFromS7[8].ToString()));

                        //Disconnect the client
                        client.Disconnect();

                        s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
                        Debug.WriteLine(s);
                        timeLabel.Invoke(new Action(() => timeLabel.Text = "Время последнего цикла: " + DateTime.Now.Subtract(s1)));
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

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

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

        public List<double> DB_ReadRealArray(S7Client client, int DBNumber, int Start, int Size)
        {
            List<double> tempList = new List<double>();
            try
            {
                byte[] DBBuffer = new byte[4 * Size];
                int result = client.DBRead(DBNumber, Start, 4 * Size, DBBuffer);
                if (result != 0)
                {
                    Console.WriteLine("Error: (Read Real Array from DB" + DBNumber + ".DBD" + Start + " Text: " + client.ErrorText(result));
                }
                for (int i = 0; i < Size; i++)
                {
                    double value = S7.GetRealAt(DBBuffer, 4 * i);
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
