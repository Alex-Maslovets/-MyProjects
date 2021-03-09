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
using System.IO;
using System.Resources;

namespace Heineken_DL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                result = client.DBRead(2000, 432 + 4*i, 4, db1Buffer);
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
            for (int t = 0; t < subs.Length; t ++)
            {
                string sub = subs[t];
                string[] subsubs = sub.Split('=');

                for (int i = 0; i < subsubs.Length; i ++)
                {
                    if (i == 1)
                    Console.WriteLine($"SubSubstring: {subsubs[i]}");
                }
                Console.WriteLine(t);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.AddXY(1, 1);
            chart1.Series["Series1"].Points.AddXY(2, 4);
            chart1.Series["Series1"].Points.AddXY(3, 9);
            chart1.Series["Series1"].Points.AddXY(4, 16);
            chart1.Series["Series1"].Points.AddXY(5, 25);
            chart1.Series["Series1"].Points.AddXY(6, 36);
            chart1.Series["Series1"].Points.AddXY(7, 49);
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
            //string workingDirectory = Environment.CurrentDirectory;
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

            //Console.WriteLine(projectDirectory + "/_Resources/configPGSQL.xml");

            List <string> tempList = new List<string>();
            for (int i = 0; i < comboB_PGSQL_savedConf.Items.Count; i++)
            {
                tempList.Add((comboB_PGSQL_savedConf.SelectedItem as ComboboxItem).Value.ToString());
            }

            //string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = string.Format("{0}Resources\\configPGSQL.xml", Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")));

            //Console.WriteLine(FileName);
            XMLSave.WriteToXmlFile<List<string>>(FileName, tempList);

            //XMLSave.WriteToXmlFile<List<string>>("C:/Users/alexo/OneDrive/Документы/GitHub/-MyProjects/Heineken_DL/Heineken_DL/_Resources/configPGSQL.xml", tempList);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            
            richTextBox1.Clear();
            List<string> tempList = new List<string>();
            tempList.Add("newString 1");
            tempList.Add("newString 2");
            tempList.Add("newString 3");
            tempList.Add("newString 4");
            tempList.Add("newString 5");
            string[] myArray = tempList.ToArray();
            foreach (string str in myArray)
            {
                richTextBox1.Text += str + "\n";
            }   
        }
    }
}
