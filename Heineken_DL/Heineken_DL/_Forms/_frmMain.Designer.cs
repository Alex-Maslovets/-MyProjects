
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.b_ModBus = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.stopButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tB_PGSQL_DB = new System.Windows.Forms.TextBox();
            this.tB_PGSQL_password = new System.Windows.Forms.TextBox();
            this.tB_PGSQL_userName = new System.Windows.Forms.TextBox();
            this.tB_PGSQL_host = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.b_PGSQL_deleteConf = new System.Windows.Forms.Button();
            this.b_PGSQL_connect = new System.Windows.Forms.Button();
            this.checkB_createConfig = new System.Windows.Forms.CheckBox();
            this.checkB_chooseConfig = new System.Windows.Forms.CheckBox();
            this.comboB_PGSQL_savedConf = new System.Windows.Forms.ComboBox();
            this.b_PGSQL_saveConf = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.b_ModBus);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(789, 415);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "S7PLC";
            // 
            // b_ModBus
            // 
            this.b_ModBus.Location = new System.Drawing.Point(20, 20);
            this.b_ModBus.Name = "b_ModBus";
            this.b_ModBus.Size = new System.Drawing.Size(75, 23);
            this.b_ModBus.TabIndex = 0;
            this.b_ModBus.Text = "ModBus";
            this.b_ModBus.UseVisualStyleBackColor = true;
            this.b_ModBus.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.Controls.Add(this.stopButton);
            this.tabPage1.Controls.Add(this.startButton);
            this.tabPage1.Controls.Add(this.timeLabel);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.tB_PGSQL_DB);
            this.tabPage1.Controls.Add(this.tB_PGSQL_password);
            this.tabPage1.Controls.Add(this.tB_PGSQL_userName);
            this.tabPage1.Controls.Add(this.tB_PGSQL_host);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.b_PGSQL_deleteConf);
            this.tabPage1.Controls.Add(this.b_PGSQL_connect);
            this.tabPage1.Controls.Add(this.checkB_createConfig);
            this.tabPage1.Controls.Add(this.checkB_chooseConfig);
            this.tabPage1.Controls.Add(this.comboB_PGSQL_savedConf);
            this.tabPage1.Controls.Add(this.b_PGSQL_saveConf);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(789, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(440, 302);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 25;
            this.stopButton.Text = "stopButton";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(440, 273);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 24;
            this.startButton.Text = "startButton";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(458, 328);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(52, 13);
            this.timeLabel.TabIndex = 23;
            this.timeLabel.Text = "timeLabel";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(663, 7);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 394);
            this.listBox1.TabIndex = 22;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(541, 6);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(115, 20);
            this.textBox2.TabIndex = 21;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // tB_PGSQL_DB
            // 
            this.tB_PGSQL_DB.Enabled = false;
            this.tB_PGSQL_DB.Location = new System.Drawing.Point(306, 110);
            this.tB_PGSQL_DB.Name = "tB_PGSQL_DB";
            this.tB_PGSQL_DB.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_DB.TabIndex = 13;
            this.tB_PGSQL_DB.Text = "postgres";
            // 
            // tB_PGSQL_password
            // 
            this.tB_PGSQL_password.Enabled = false;
            this.tB_PGSQL_password.Location = new System.Drawing.Point(306, 136);
            this.tB_PGSQL_password.Name = "tB_PGSQL_password";
            this.tB_PGSQL_password.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_password.TabIndex = 12;
            this.tB_PGSQL_password.Text = "123456789";
            // 
            // tB_PGSQL_userName
            // 
            this.tB_PGSQL_userName.Enabled = false;
            this.tB_PGSQL_userName.Location = new System.Drawing.Point(306, 84);
            this.tB_PGSQL_userName.Name = "tB_PGSQL_userName";
            this.tB_PGSQL_userName.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_userName.TabIndex = 11;
            this.tB_PGSQL_userName.Text = "postgres";
            // 
            // tB_PGSQL_host
            // 
            this.tB_PGSQL_host.Enabled = false;
            this.tB_PGSQL_host.Location = new System.Drawing.Point(306, 58);
            this.tB_PGSQL_host.Name = "tB_PGSQL_host";
            this.tB_PGSQL_host.Size = new System.Drawing.Size(212, 20);
            this.tB_PGSQL_host.TabIndex = 10;
            this.tB_PGSQL_host.Text = "localhost";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(158, 323);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(115, 86);
            this.button5.TabIndex = 20;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // b_PGSQL_deleteConf
            // 
            this.b_PGSQL_deleteConf.Location = new System.Drawing.Point(220, 58);
            this.b_PGSQL_deleteConf.Name = "b_PGSQL_deleteConf";
            this.b_PGSQL_deleteConf.Size = new System.Drawing.Size(80, 21);
            this.b_PGSQL_deleteConf.TabIndex = 19;
            this.b_PGSQL_deleteConf.Text = "УДАЛИТЬ";
            this.b_PGSQL_deleteConf.UseVisualStyleBackColor = true;
            this.b_PGSQL_deleteConf.Click += new System.EventHandler(this.b_PGSQL_deleteConf_Click);
            // 
            // b_PGSQL_connect
            // 
            this.b_PGSQL_connect.Location = new System.Drawing.Point(92, 83);
            this.b_PGSQL_connect.Name = "b_PGSQL_connect";
            this.b_PGSQL_connect.Size = new System.Drawing.Size(120, 21);
            this.b_PGSQL_connect.TabIndex = 18;
            this.b_PGSQL_connect.Text = "ПОДКЛЮЧИТЬСЯ";
            this.b_PGSQL_connect.UseVisualStyleBackColor = true;
            this.b_PGSQL_connect.Click += new System.EventHandler(this.b_PGSQL_connect_Click);
            // 
            // checkB_createConfig
            // 
            this.checkB_createConfig.AutoSize = true;
            this.checkB_createConfig.Location = new System.Drawing.Point(306, 35);
            this.checkB_createConfig.Name = "checkB_createConfig";
            this.checkB_createConfig.Size = new System.Drawing.Size(68, 17);
            this.checkB_createConfig.TabIndex = 17;
            this.checkB_createConfig.Text = "Создать";
            this.checkB_createConfig.UseVisualStyleBackColor = true;
            this.checkB_createConfig.Click += new System.EventHandler(this.checkB_chreateConfig_Click);
            // 
            // checkB_chooseConfig
            // 
            this.checkB_chooseConfig.AutoSize = true;
            this.checkB_chooseConfig.Checked = true;
            this.checkB_chooseConfig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkB_chooseConfig.Location = new System.Drawing.Point(6, 35);
            this.checkB_chooseConfig.Name = "checkB_chooseConfig";
            this.checkB_chooseConfig.Size = new System.Drawing.Size(70, 17);
            this.checkB_chooseConfig.TabIndex = 16;
            this.checkB_chooseConfig.Text = "Выбрать";
            this.checkB_chooseConfig.UseVisualStyleBackColor = true;
            this.checkB_chooseConfig.Click += new System.EventHandler(this.checkB_chooseConfig_Click);
            // 
            // comboB_PGSQL_savedConf
            // 
            this.comboB_PGSQL_savedConf.FormattingEnabled = true;
            this.comboB_PGSQL_savedConf.Location = new System.Drawing.Point(6, 58);
            this.comboB_PGSQL_savedConf.Name = "comboB_PGSQL_savedConf";
            this.comboB_PGSQL_savedConf.Size = new System.Drawing.Size(206, 21);
            this.comboB_PGSQL_savedConf.TabIndex = 15;
            this.comboB_PGSQL_savedConf.SelectedIndexChanged += new System.EventHandler(this.cB_PGSQL_savedConf_SelectedIndexChanged);
            // 
            // b_PGSQL_saveConf
            // 
            this.b_PGSQL_saveConf.Enabled = false;
            this.b_PGSQL_saveConf.Location = new System.Drawing.Point(398, 162);
            this.b_PGSQL_saveConf.Name = "b_PGSQL_saveConf";
            this.b_PGSQL_saveConf.Size = new System.Drawing.Size(120, 21);
            this.b_PGSQL_saveConf.TabIndex = 9;
            this.b_PGSQL_saveConf.Text = "СОХРАНИТЬ";
            this.b_PGSQL_saveConf.UseVisualStyleBackColor = true;
            this.b_PGSQL_saveConf.Click += new System.EventHandler(this.b_PGSQL_saveConf_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 369);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(146, 40);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 323);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 40);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(797, 441);
            this.tabControl1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(821, 478);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Heineken_DL";
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tB_PGSQL_DB;
        private System.Windows.Forms.TextBox tB_PGSQL_password;
        private System.Windows.Forms.TextBox tB_PGSQL_userName;
        private System.Windows.Forms.TextBox tB_PGSQL_host;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button b_PGSQL_deleteConf;
        private System.Windows.Forms.Button b_PGSQL_connect;
        private System.Windows.Forms.CheckBox checkB_createConfig;
        private System.Windows.Forms.CheckBox checkB_chooseConfig;
        private System.Windows.Forms.ComboBox comboB_PGSQL_savedConf;
        private System.Windows.Forms.Button b_PGSQL_saveConf;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button b_ModBus;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label timeLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

