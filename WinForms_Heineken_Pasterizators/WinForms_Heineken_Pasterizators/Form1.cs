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

namespace WinForms_Heineken_Pasterizators
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            System.Diagnostics.Debug.WriteLine(s);

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
                System.Diagnostics.Debug.WriteLine(b);
            }
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

            // Disconnect the client
            client.Disconnect();

            s = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            System.Diagnostics.Debug.WriteLine(s);
        }

        public List<bool> DB_ReadBitArray(S7Client client, int DBNumber, int StartByte, int StartBit, int Size)
        {
            List<bool> tempList = new List<bool>();

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

        private void button2_Click(object sender, EventArgs e)
        {
            notMain();
        }
    }
}
