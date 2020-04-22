using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewDL
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private List<OdbcSource> sources;

        private void Load_Form(object sender, EventArgs e)
        {
            odbcServers = DataAcess.ListODBCsources();
            Reffesh_ComboBox();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            odbcServers = DataAcess.ListODBCsources();
            Reffesh_ComboBox();
        }

        private void Reffesh_ComboBox()
        {
            comboBox1.Items.Clear();
            foreach (string element in odbcServers)
            {
                comboBox1.Items.Add(element);
            }
        }
    }
}
