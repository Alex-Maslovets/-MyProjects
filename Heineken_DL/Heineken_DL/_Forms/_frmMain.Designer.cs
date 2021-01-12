
namespace Heineken_DL
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cB_PGSQL_savedConf = new System.Windows.Forms.ComboBox();
            this.tB_PGSQL_DB = new System.Windows.Forms.TextBox();
            this.tB_PGSQL_password = new System.Windows.Forms.TextBox();
            this.tB_PGSQL_userName = new System.Windows.Forms.TextBox();
            this.tB_PGSQL_host = new System.Windows.Forms.TextBox();
            this.b_PGSQL_saveConf = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 456);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(821, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(788, 438);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cB_PGSQL_savedConf);
            this.tabPage1.Controls.Add(this.tB_PGSQL_DB);
            this.tabPage1.Controls.Add(this.tB_PGSQL_password);
            this.tabPage1.Controls.Add(this.tB_PGSQL_userName);
            this.tabPage1.Controls.Add(this.tB_PGSQL_host);
            this.tabPage1.Controls.Add(this.b_PGSQL_saveConf);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(780, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cB_PGSQL_savedConf
            // 
            this.cB_PGSQL_savedConf.FormattingEnabled = true;
            this.cB_PGSQL_savedConf.Location = new System.Drawing.Point(6, 6);
            this.cB_PGSQL_savedConf.Name = "cB_PGSQL_savedConf";
            this.cB_PGSQL_savedConf.Size = new System.Drawing.Size(206, 21);
            this.cB_PGSQL_savedConf.TabIndex = 15;
            this.cB_PGSQL_savedConf.SelectedIndexChanged += new System.EventHandler(this.cB_PGSQL_savedConf_SelectedIndexChanged);
            // 
            // tB_PGSQL_DB
            // 
            this.tB_PGSQL_DB.Location = new System.Drawing.Point(390, 58);
            this.tB_PGSQL_DB.Name = "tB_PGSQL_DB";
            this.tB_PGSQL_DB.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_DB.TabIndex = 13;
            this.tB_PGSQL_DB.Text = "postgres";
            // 
            // tB_PGSQL_password
            // 
            this.tB_PGSQL_password.Location = new System.Drawing.Point(390, 84);
            this.tB_PGSQL_password.Name = "tB_PGSQL_password";
            this.tB_PGSQL_password.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_password.TabIndex = 12;
            this.tB_PGSQL_password.Text = "123456789";
            // 
            // tB_PGSQL_userName
            // 
            this.tB_PGSQL_userName.Location = new System.Drawing.Point(390, 32);
            this.tB_PGSQL_userName.Name = "tB_PGSQL_userName";
            this.tB_PGSQL_userName.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_userName.TabIndex = 11;
            this.tB_PGSQL_userName.Text = "postgres";
            // 
            // tB_PGSQL_host
            // 
            this.tB_PGSQL_host.Location = new System.Drawing.Point(390, 6);
            this.tB_PGSQL_host.Name = "tB_PGSQL_host";
            this.tB_PGSQL_host.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_host.TabIndex = 10;
            this.tB_PGSQL_host.Text = "localhost";
            // 
            // b_PGSQL_saveConf
            // 
            this.b_PGSQL_saveConf.Location = new System.Drawing.Point(497, 110);
            this.b_PGSQL_saveConf.Name = "b_PGSQL_saveConf";
            this.b_PGSQL_saveConf.Size = new System.Drawing.Size(105, 21);
            this.b_PGSQL_saveConf.TabIndex = 9;
            this.b_PGSQL_saveConf.Text = "СОХРАНИТЬ";
            this.b_PGSQL_saveConf.UseVisualStyleBackColor = true;
            this.b_PGSQL_saveConf.Click += new System.EventHandler(this.b_PGSQL_saveConf_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(66, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 40);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(780, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "S7PLC";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(699, 148);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Chart";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // chart1
            // 
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 177);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 6;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(768, 229);
            this.chart1.TabIndex = 18;
            this.chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 478);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Heineken_DL";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tB_PGSQL_DB;
        private System.Windows.Forms.TextBox tB_PGSQL_password;
        private System.Windows.Forms.TextBox tB_PGSQL_userName;
        private System.Windows.Forms.TextBox tB_PGSQL_host;
        private System.Windows.Forms.Button b_PGSQL_saveConf;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cB_PGSQL_savedConf;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

