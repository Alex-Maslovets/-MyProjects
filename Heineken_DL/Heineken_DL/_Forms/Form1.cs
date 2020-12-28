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

        private void button1_Click(object sender, EventArgs e)
        {
            string s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff");
            Console.WriteLine(s);

            var cs = "Host=localhost;Username=postgres;Password=123456789;Database=postgres";

            var con = new NpgsqlConnection(cs);
            con.Open();

            /*var sql = "SELECT version()";

            var cmd = new NpgsqlCommand(sql, con);

            var version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"PostgreSQL version: {version}");
            */
            var cmd_insert = new NpgsqlCommand();
            cmd_insert.Connection = con;
           
            cmd_insert.CommandText = "INSERT INTO _test_table (id, value, date_time) VALUES ( 2, 33, '" + s + "')";
            cmd_insert.ExecuteNonQuery();

            con.Close();

            s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff");
            Console.WriteLine(s);
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
    }
}
