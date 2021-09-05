using Npgsql;
using ReadWriteS7;
using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Heineken_DL
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Create and connect the client
            var client = new S7Client();
            int result = client.ConnectTo("192.168.100.150", 0, 1);
            if (result == 0)
            {
                Console.WriteLine("Connected to 192.168.100.150");
            }
            else
            {
                Console.WriteLine(client.ErrorText(result));
            }

            Console.WriteLine("\n---- Read DB 2000");

            byte[] db1Buffer = new byte[20];
            result = client.DBRead(2000, 0, 20, db1Buffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }
            int db1dbw2 = S7.GetIntAt(db1Buffer, 2);
            Console.WriteLine("DB1.DBW2: " + db1dbw2);

            double db1ddd4 = S7.GetRealAt(db1Buffer, 4);
            Console.WriteLine("DB1.DBD4: " + db1ddd4);

            double db1dbd8 = S7.GetDIntAt(db1Buffer, 8);
            Console.WriteLine("DB1.DBD8: " + db1dbd8);

            double db1dbd12 = S7.GetDWordAt(db1Buffer, 12);
            Console.WriteLine("DB1.DBD12: " + db1dbd12);

            double db1dbw16 = S7.GetWordAt(db1Buffer, 16);
            Console.WriteLine("DB1.DBD16: " + db1dbw16);

            // Disconnect the client
            client.Disconnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            Console.WriteLine(s);

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
            var test = String.Join(", ", myList.ToArray());
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
            // Disconnect the client

            // Установка соединения с PostgreSQL
            var cs = "Host=" + tB_PGSQL_host.Text + ";Username=" + tB_PGSQL_userName.Text + ";Password=" + tB_PGSQL_password.Text + ";Database=" + tB_PGSQL_DB.Text + "";

            var con = new NpgsqlConnection(cs);
            con.Open();

            // Запиись данных в PostgreSQL
            var cmd_insert = new NpgsqlCommand();
            cmd_insert.Connection = con;

            //cmd_insert.CommandText = "INSERT INTO _test_table (id, value, date_time) VALUES ( 2, 33, '" + s + "')";
            cmd_insert.CommandText = "INSERT INTO _test_table (id, value, date_time) VALUES " + test;
            //           Console.WriteLine(cmd_insert.CommandText);
            cmd_insert.ExecuteNonQuery();

            // Чтение данных из PostgreSQL
            string sql = "Select * from _test_table";

            //Console.WriteLine(s);
            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                //int val;
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //val = Int32.Parse(reader[0].ToString());
                    //Console.WriteLine(reader[0].ToString());
                    //Console.WriteLine(reader[1].ToString());
                    //Console.WriteLine(reader[2].ToString());
                    //do whatever you like
                }
            }

            //s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            //Console.WriteLine(s);

            // Закрытие соединения
            con.Close();
            client.Disconnect();

            s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            Console.WriteLine(s);
        }

        // Класс для записи в комбобокс текста в переменную Text и доп. информации в переменную Value
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void b_PGSQL_saveConf_Click(object sender, EventArgs e)
        {
            ComboboxItem item = new ComboboxItem();
            int temp;
            temp = comboB_PGSQL_savedConf.Items.Count + 1;
            item.Text = "Конфигурация #" + temp.ToString();
            item.Value = "Host=" + tB_PGSQL_host.Text + ";Username=" + tB_PGSQL_userName.Text + ";Password=" + tB_PGSQL_password.Text + ";Database=" + tB_PGSQL_DB.Text + "";

            comboB_PGSQL_savedConf.Items.Add(item);

            comboB_PGSQL_savedConf.SelectedIndex = comboB_PGSQL_savedConf.Items.Count - 1;

            Console.WriteLine((comboB_PGSQL_savedConf.Items[temp - 1] as ComboboxItem).Value.ToString());

            checkB_createConfig.Checked = false;
            checkB_chooseConfig.Checked = true;

            comboB_PGSQL_savedConf.Enabled = true;
            tB_PGSQL_DB.Enabled = false;
            tB_PGSQL_host.Enabled = false;
            tB_PGSQL_password.Enabled = false;
            tB_PGSQL_userName.Enabled = false;
            b_PGSQL_saveConf.Enabled = false;
        }

        private void cB_PGSQL_savedConf_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp;
            temp = (comboB_PGSQL_savedConf.SelectedItem as ComboboxItem).Value.ToString();

            string[] subs = temp.Split(';');

            //foreach (string sub in subs)
            for (int t = 0; t < subs.Length; t++)
            {
                string sub = subs[t];
                string[] subsubs = sub.Split('=');

                for (int i = 0; i < subsubs.Length; i++)
                {
                    if (i == 1)
                        Console.WriteLine($"SubSubstring: {subsubs[i]}");
                }
                Console.WriteLine(t);
            }
        }

        private void checkB_chooseConfig_Click(object sender, EventArgs e)
        {
            checkB_createConfig.Checked = false;
            checkB_chooseConfig.Checked = true;

            comboB_PGSQL_savedConf.Enabled = true;
            tB_PGSQL_DB.Enabled = false;
            tB_PGSQL_host.Enabled = false;
            tB_PGSQL_password.Enabled = false;
            tB_PGSQL_userName.Enabled = false;
            b_PGSQL_saveConf.Enabled = false;
        }

        private void checkB_chreateConfig_Click(object sender, EventArgs e)
        {
            checkB_chooseConfig.Checked = false;
            checkB_createConfig.Checked = true;

            comboB_PGSQL_savedConf.Enabled = false;
            tB_PGSQL_DB.Enabled = true;
            tB_PGSQL_host.Enabled = true;
            tB_PGSQL_password.Enabled = true;
            tB_PGSQL_userName.Enabled = true;
            b_PGSQL_saveConf.Enabled = true;
        }

        private void b_PGSQL_deleteConf_Click(object sender, EventArgs e)
        {
            comboB_PGSQL_savedConf.Items.RemoveAt(comboB_PGSQL_savedConf.SelectedIndex);
        }

        private void b_PGSQL_connect_Click(object sender, EventArgs e)
        {
            List<string> tempList = new List<string>();
            for (int i = 0; i < comboB_PGSQL_savedConf.Items.Count; i++)
            {
                tempList.Add((comboB_PGSQL_savedConf.SelectedItem as ComboboxItem).Value.ToString());
            }
            string FileName = string.Format("{0}Resources\\configPGSQL.xml", Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));

            XMLSave.WriteToXmlFile<List<string>>(FileName, tempList);

        }
        private void button5_Click(object sender, EventArgs e)
        {
            DateTime dtStart = DateTime.Now;
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));

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

            var readClient = new ReadS7();

            /*byte[] db1Buffer = new byte[4];
            result = client.MBRead(510,4,db1Buffer);
            double db1ddd4 = S7.GetRealAt(db1Buffer, 0);

            double mReal = readClient.M_ReadReal(client,510);

            Console.WriteLine(db1ddd4.ToString() + " --- "+ mReal.ToString());
            */

            for (int i = 0; i <= 99; i++)
            {
                /*
                var s7MultiVar = new S7MultiVar(client);
                byte[] db1 = new byte[16];
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 2000, 432, 16, ref db1);
                byte[] db3 = new byte[16];
                s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 2000, 532, 16, ref db3);
                result = s7MultiVar.Read();
                if (result != 0)
                {
                    Console.WriteLine("Error on s7MultiVar.Read()");
                }
                */

                List<double> mReal = readClient.DB_ReadRealArray(client, 2000, 432, 4);
                List<double> mReal_532 = readClient.DB_ReadRealArray(client, 2000, 532, 4);

            }
            /*
            double db2000dbd432 = S7.GetRealAt(db1, 0);
            Console.WriteLine("DB2000.DBD432 = {0}", db2000dbd432);

            db2000dbd432 = S7.GetRealAt(db1, 4);
            Console.WriteLine("DB2000.DBD436 = {0}", db2000dbd432);

            db2000dbd432 = S7.GetRealAt(db1, 8);
            Console.WriteLine("DB2000.DBD440 = {0}", db2000dbd432);
            
            db2000dbd432 = S7.GetRealAt(db1, 12);
            Console.WriteLine("DB2000.DBD444 = {0}", db2000dbd432);

            double db2000dbd532 = S7.GetRealAt(db3, 0);
            Console.WriteLine("DB2000.DBD532 = {0}", db2000dbd532);

            db2000dbd532 = S7.GetRealAt(db3, 4);
            Console.WriteLine("DB2000.DBD536 = {0}", db2000dbd532);

            db2000dbd532 = S7.GetRealAt(db3, 8);
            Console.WriteLine("DB2000.DBD540 = {0}", db2000dbd532);

            db2000dbd532 = S7.GetRealAt(db3, 12);
            Console.WriteLine("DB2000.DBD544 = {0}", db2000dbd532);
            */

            // Закрытие соединения
            client.Disconnect();

            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));

            TimeSpan dtEnd = DateTime.Now.Subtract(dtStart);
            Console.WriteLine(dtEnd.ToString());
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string adress = textBox2.Text.Replace(" ", string.Empty);
                adress = adress.Trim().Replace(" ", string.Empty);
                adress = adress.ToUpper();
                bool result;

                if (adress.StartsWith("M"))
                {
                    result = mAdressCompare(adress);
                    if (result)
                    {
                        listBox1.Items.Add(adress);
                        listBoxUpdate(listBox1);
                    }
                    else MessageBox.Show("Not Add");
                }
                else if (adress.StartsWith("DB"))
                {
                    result = dbAdressCompare(adress);
                    if (result)
                    {
                        listBox1.Items.Add(adress);
                        listBoxUpdate(listBox1);
                    }
                    else MessageBox.Show("Not Add");
                }
                else MessageBox.Show("Not Add");
            }
        }

        public void listBoxUpdate(ListBox listBox)
        {
            string[] arrayStr = listBox.Items.OfType<string>().ToArray();
            listBox.Items.Clear();

            foreach (string str in arrayStr.Distinct())
            {
                listBox.Items.Add(str);
            }
        }


        private bool mAdressCompare(string adress)
        {
            string oneCutAdress = adress.Substring(1);
            // работа с меркерным адресом
            if (oneCutAdress.StartsWith("B") || oneCutAdress.StartsWith("W") || oneCutAdress.StartsWith("D"))
            {
                string twoCutAdress = oneCutAdress.Substring(1);
                if (twoCutAdress.ToCharArray().Length != 0)
                {
                    foreach (char ch in twoCutAdress.ToCharArray())
                    {
                        if (!Char.IsDigit(ch))
                        {
                            return false;
#pragma warning disable CS0162 // Unreachable code detected
                            break;
#pragma warning restore CS0162 // Unreachable code detected
                        }
                    }
                    return true;
                }
                else return false;
            }
            else if (oneCutAdress.Contains("."))
            {
                int mPointAmount = new Regex("[.]").Matches(oneCutAdress).Count;
                if (mPointAmount > 1) return false;
                else return true;
            }
            else return false;
        }
        private bool dbAdressCompare(string adress)
        {
            string oneCutAdress = adress.Substring(2);
            // работа с адресом DB-области
            if (oneCutAdress.Contains("."))
            {
                int dbPointAmount = new Regex("[.]").Matches(oneCutAdress).Count;
                switch (dbPointAmount)
                {
                    case 1:
                        string[] twoStrings = oneCutAdress.Split('.');

                        bool sw1_partOne, sw1_partTwo;

                        if (twoStrings[0].ToCharArray().Length != 0)
                        {
                            sw1_partOne = true;
                            foreach (char ch in twoStrings[0].ToCharArray())
                            {
                                if (!Char.IsDigit(ch))
                                {
                                    sw1_partOne = false;
                                }
                            }
                        }
                        else sw1_partOne = false;

                        if (twoStrings[1].StartsWith("DB"))
                        {
                            string twoStringCautAdress = twoStrings[1].Substring(2);
                            if (twoStringCautAdress.StartsWith("D") || twoStringCautAdress.StartsWith("W") || twoStringCautAdress.StartsWith("B"))
                            {
                                if (twoStringCautAdress.Substring(1).ToCharArray().Length != 0)
                                {
                                    sw1_partTwo = true;
                                    foreach (char ch in twoStringCautAdress.Substring(1).ToCharArray())
                                    {
                                        if (!Char.IsDigit(ch))
                                        {
                                            sw1_partTwo = false;
                                        }
                                    }
                                }
                                else sw1_partTwo = false;
                            }
                            else sw1_partTwo = false;
                        }
                        else sw1_partTwo = false;

                        return sw1_partOne && sw1_partTwo;

#pragma warning disable CS0162 // Unreachable code detected
                        break;
#pragma warning restore CS0162 // Unreachable code detected
                    case 2:
                        string[] threeStrings = oneCutAdress.Split('.');
                        bool sw2_partOne, sw2_partTwo, sw2_partThree;

                        if (threeStrings[0].ToCharArray().Length != 0)
                        {
                            sw2_partOne = true;
                            foreach (char ch in threeStrings[0].ToCharArray())
                            {
                                if (!Char.IsDigit(ch))
                                {
                                    sw2_partOne = false;
                                }
                            }
                        }
                        else sw2_partOne = false;

                        if (threeStrings[1].StartsWith("DBX"))
                        {
                            if (threeStrings[1].Substring(3).ToCharArray().Length != 0)
                            {
                                sw2_partTwo = true;
                                foreach (char ch in threeStrings[1].Substring(3).ToCharArray())
                                {
                                    if (!Char.IsDigit(ch))
                                    {
                                        sw2_partTwo = false;
                                    }
                                }
                            }
                            else sw2_partTwo = false;
                        }
                        else sw2_partTwo = false;

                        if (threeStrings[2].ToCharArray().Length != 0)
                        {
                            bool chIsDigit = true;
                            bool chIsInRange = true;

                            foreach (char ch in threeStrings[2].ToCharArray())
                            {
                                if (!Char.IsDigit(ch))
                                {
                                    chIsDigit = false;
                                }
                                else
                                {
                                    int chToInt = Int32.Parse(ch.ToString());
                                    if (chToInt <= 0 || chToInt >= 7) chIsInRange = false;
                                }
                            }
                            sw2_partThree = chIsDigit && chIsInRange;
                        }
                        else sw2_partThree = false;

                        return sw2_partOne && sw2_partTwo && sw2_partThree;

#pragma warning disable CS0162 // Unreachable code detected
                        break;
#pragma warning restore CS0162 // Unreachable code detected
                    default:
                        return false;
#pragma warning disable CS0162 // Unreachable code detected
                        break;
#pragma warning restore CS0162 // Unreachable code detected
                }
            }
            else return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
            SerialPort serialPort = new SerialPort(); //Create a new SerialPort object.
            serialPort.PortName = "COM1";
            serialPort.BaudRate = 9600;
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.Open();
            
            ModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPort);

            byte slaveID = 1;
            ushort startAddress = 0;
            ushort numOfPoints = 1;
            ushort[] holding_register = master.ReadHoldingRegisters(slaveID, startAddress,
            numOfPoints);
            Console.WriteLine(holding_register);
            Console.ReadKey();
            */
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
                        Console.WriteLine(s);

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

                        // --- Work with Reals --- //
                        //List<double> realsFromS7 = new List<double>();
                        //realsFromS7 = DB_ReadRealArray(client, 2000, 432, 13);
                        //realsFromS7 = DB_ReadRealArray(client, 2000, 432, 600);

                        List<string> myList = new List<string>();

                        for (int i = 0; i <= 199; i++)
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
                        var test = String.Join(", ", myList.ToArray());


                        var cs = "Host=" + tB_PGSQL_host.Text + ";Username=" + tB_PGSQL_userName.Text + ";Password=" + tB_PGSQL_password.Text + ";Database=" + tB_PGSQL_DB.Text + "";

                        var con = new NpgsqlConnection(cs);
                        con.Open();

                        // Запиись данных в PostgreSQL
                        var cmd_insert = new NpgsqlCommand();
                        cmd_insert.Connection = con;

                        cmd_insert.CommandText = "INSERT INTO _test_table (id, value, date_time) VALUES " + test;
                        cmd_insert.ExecuteNonQuery();

                        // Чтение данных из PostgreSQL
                        /*
                        string sql = "Select * from _test_table";

                        //Console.WriteLine(s);
                        using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
                        {
                            //int val;
                            NpgsqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                //val = Int32.Parse(reader[0].ToString());
                                //Console.WriteLine(reader[0].ToString());
                                //Console.WriteLine(reader[1].ToString());
                                //Console.WriteLine(reader[2].ToString());
                                //do whatever you like
                            }
                        }
                        */

                        // Закрытие соединения
                        con.Close();
                        client.Disconnect();

                        s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
                        Console.WriteLine(s);
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

        private void startButton_Click(object sender, EventArgs e)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
            sw.Stop();
            Console.WriteLine("Array.Copy: {0:N0} ticks", sw.ElapsedTicks);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }
    }
}
