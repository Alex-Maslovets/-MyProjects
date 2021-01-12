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

            // Установка соединения с PostgreSQL
            var cs = "Host=" + tB_PGSQL_host.Text + ";Username=" + tB_PGSQL_userName.Text + ";Password=" + tB_PGSQL_password.Text + ";Database=" + tB_PGSQL_DB.Text + "";

            var con = new NpgsqlConnection(cs);
            con.Open();
            
            // Запиись данных в PostgreSQL
            var cmd_insert = new NpgsqlCommand();
            cmd_insert.Connection = con;

            cmd_insert.CommandText = "INSERT INTO _test_table (id, value, date_time) VALUES ( 2, 33, '" + s + "')";
            cmd_insert.ExecuteNonQuery();

            // Чтение данных из PostgreSQL
            string sql = "Select * from _test_table";

            using (NpgsqlCommand command = new NpgsqlCommand(sql, con))
            {
                //int val;
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //val = Int32.Parse(reader[0].ToString());
                    Console.WriteLine(reader[0].ToString());
                    Console.WriteLine(reader[1].ToString());
                    Console.WriteLine(reader[2].ToString());
                    //do whatever you like
                }
            }
            // Закрытие соединения
            con.Close();
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
            temp = cB_PGSQL_savedConf.Items.Count + 1;
            item.Text = "Configuration #" + temp.ToString();
            item.Value = "Host=" + tB_PGSQL_host.Text + ";Username=" + tB_PGSQL_userName.Text + ";Password=" + tB_PGSQL_password.Text + ";Database=" + tB_PGSQL_DB.Text + "";

            cB_PGSQL_savedConf.Items.Add(item);

            cB_PGSQL_savedConf.SelectedIndex = 0;

            Console.WriteLine((cB_PGSQL_savedConf.Items[temp - 1] as ComboboxItem).Value.ToString());

        }

        private void cB_PGSQL_savedConf_SelectedIndexChanged(object sender, EventArgs e)
        {
            string temp;
            temp = (cB_PGSQL_savedConf.SelectedItem as ComboboxItem).Value.ToString();
            Console.WriteLine(temp);
            
            string[] subs = temp.Split(';');

            foreach (string sub in subs)
            {
                string[] subsubs = sub.Split('=');

                for (int i = 0; i < subsubs.Length; i ++)
                {
                    if (i == 1)
                    Console.WriteLine($"SubSubstring: {subsubs[i]}" + i.ToString());
                }
                //Console.WriteLine($"Substring: {sub}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.AddXY(1, 2);
            chart1.Series["Series1"].Points.AddXY(2, 4);
            chart1.Series["Series1"].Points.AddXY(4, 8);
            chart1.Series["Series1"].Points.AddXY(8, 16);
            chart1.Series["Series1"].Points.AddXY(16, 32);
            chart1.Series["Series1"].Points.AddXY(32, 64);
            chart1.Series["Series1"].Points.AddXY(64, 128);
        }
    }
}
