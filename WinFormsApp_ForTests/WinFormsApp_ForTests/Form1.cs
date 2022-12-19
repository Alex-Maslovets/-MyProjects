using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Sharp7;


namespace WinFormsApp_ForTests
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static Form1 _Instance;
        public static Form1 Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Form1();
                return _Instance;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.Nodes.Count != 0)
                {
                    if (treeView1.SelectedNode.Parent is null) treeView1.SelectedNode.Nodes.Add(treeView1.SelectedNode.Text);
                }
                else MessageBox.Show("Create new Block");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Click to TreeView element");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Parent == null)
                {
                    treeView1.Nodes.Remove(treeView1.SelectedNode);
                }
                else
                {
                    treeView1.SelectedNode.Parent.Nodes.Remove(treeView1.SelectedNode);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Add("New Message Node");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Form2 form2 = new Form2())
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                            treeView1.SelectedNode.Text = form2.TheValue;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Click to TreeView element");
                    }
                }
            }
        }

        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                MessageBox.Show(e.Node.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            S7Client plcClient = new S7Client();
            int result = plcClient.ConnectTo("192.168.127.150", 0, 1);

            byte[] db2Buffer = new byte[10];

            result = plcClient.DBRead(2000, 8, 10, db2Buffer);
            if (result != 0)
            {
                try
                {
                    MessageBox.Show("Messages; " + DateTime.Now + "; " + plcClient.ErrorText(result) + ";\n");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                Console.WriteLine(db2Buffer[0] + " --- " + db2Buffer[1] + " --- " + db2Buffer[2] + " --- " + db2Buffer[3] + " --- " + db2Buffer[4] + " --- " + db2Buffer[5] + " --- " + db2Buffer[6]);
            }
        }
    }
}