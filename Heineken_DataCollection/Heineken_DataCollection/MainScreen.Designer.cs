
namespace Heineken_DataCollection
{
    partial class MainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorkerRead = new System.ComponentModel.BackgroundWorker();
            this.bgWReadModBus = new System.ComponentModel.BackgroundWorker();
            this.bgWMessages = new System.ComponentModel.BackgroundWorker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.timeLabel_mb_6 = new System.Windows.Forms.Label();
            this.timeLabel_mb_5 = new System.Windows.Forms.Label();
            this.timeLabel_mb_7 = new System.Windows.Forms.Label();
            this.timeLabel_mb_4 = new System.Windows.Forms.Label();
            this.timeLabel_mb_3 = new System.Windows.Forms.Label();
            this.timeLabel_mb_2 = new System.Windows.Forms.Label();
            this.timeLabel_mb_1 = new System.Windows.Forms.Label();
            this.Button_notRead_mb = new System.Windows.Forms.Button();
            this.progressBarRead_mb = new System.Windows.Forms.ProgressBar();
            this.timeLabel_mb = new System.Windows.Forms.Label();
            this.Button_Read_mb = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.timeLabel_s7_5 = new System.Windows.Forms.Label();
            this.timeLabel_s7_4 = new System.Windows.Forms.Label();
            this.timeLabel_s7_3 = new System.Windows.Forms.Label();
            this.timeLabel_s7_2 = new System.Windows.Forms.Label();
            this.timeLabel_s7_1 = new System.Windows.Forms.Label();
            this.timeLabel_s7 = new System.Windows.Forms.Label();
            this.progressBarRead_s7 = new System.Windows.Forms.ProgressBar();
            this.Button_notRead_s7 = new System.Windows.Forms.Button();
            this.Button_Read_s7 = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.timeLabel_mb_8 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.timeLabel_mb_8);
            this.tabPage2.Controls.Add(this.timeLabel_mb_6);
            this.tabPage2.Controls.Add(this.timeLabel_mb_5);
            this.tabPage2.Controls.Add(this.timeLabel_mb_7);
            this.tabPage2.Controls.Add(this.timeLabel_mb_4);
            this.tabPage2.Controls.Add(this.timeLabel_mb_3);
            this.tabPage2.Controls.Add(this.timeLabel_mb_2);
            this.tabPage2.Controls.Add(this.timeLabel_mb_1);
            this.tabPage2.Controls.Add(this.Button_notRead_mb);
            this.tabPage2.Controls.Add(this.progressBarRead_mb);
            this.tabPage2.Controls.Add(this.timeLabel_mb);
            this.tabPage2.Controls.Add(this.Button_Read_mb);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(358, 280);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Modbus";
            // 
            // timeLabel_mb_6
            // 
            this.timeLabel_mb_6.AutoSize = true;
            this.timeLabel_mb_6.Location = new System.Drawing.Point(6, 200);
            this.timeLabel_mb_6.Name = "timeLabel_mb_6";
            this.timeLabel_mb_6.Size = new System.Drawing.Size(83, 13);
            this.timeLabel_mb_6.TabIndex = 24;
            this.timeLabel_mb_6.Text = "Время TH_Filtr:";
            // 
            // timeLabel_mb_5
            // 
            this.timeLabel_mb_5.AutoSize = true;
            this.timeLabel_mb_5.Location = new System.Drawing.Point(6, 180);
            this.timeLabel_mb_5.Name = "timeLabel_mb_5";
            this.timeLabel_mb_5.Size = new System.Drawing.Size(107, 13);
            this.timeLabel_mb_5.TabIndex = 23;
            this.timeLabel_mb_5.Text = "Время TH_EnBlock:";
            // 
            // timeLabel_mb_7
            // 
            this.timeLabel_mb_7.AutoSize = true;
            this.timeLabel_mb_7.Location = new System.Drawing.Point(6, 240);
            this.timeLabel_mb_7.Name = "timeLabel_mb_7";
            this.timeLabel_mb_7.Size = new System.Drawing.Size(85, 13);
            this.timeLabel_mb_7.TabIndex = 22;
            this.timeLabel_mb_7.Text = "Время записи: ";
            // 
            // timeLabel_mb_4
            // 
            this.timeLabel_mb_4.AutoSize = true;
            this.timeLabel_mb_4.Location = new System.Drawing.Point(6, 160);
            this.timeLabel_mb_4.Name = "timeLabel_mb_4";
            this.timeLabel_mb_4.Size = new System.Drawing.Size(89, 13);
            this.timeLabel_mb_4.TabIndex = 21;
            this.timeLabel_mb_4.Text = "Время TH_VAO:";
            // 
            // timeLabel_mb_3
            // 
            this.timeLabel_mb_3.AutoSize = true;
            this.timeLabel_mb_3.Location = new System.Drawing.Point(6, 140);
            this.timeLabel_mb_3.Name = "timeLabel_mb_3";
            this.timeLabel_mb_3.Size = new System.Drawing.Size(88, 13);
            this.timeLabel_mb_3.TabIndex = 20;
            this.timeLabel_mb_3.Text = "Время TH_BLO:";
            // 
            // timeLabel_mb_2
            // 
            this.timeLabel_mb_2.AutoSize = true;
            this.timeLabel_mb_2.Location = new System.Drawing.Point(6, 120);
            this.timeLabel_mb_2.Name = "timeLabel_mb_2";
            this.timeLabel_mb_2.Size = new System.Drawing.Size(92, 13);
            this.timeLabel_mb_2.TabIndex = 19;
            this.timeLabel_mb_2.Text = "Время TH_Pack:";
            // 
            // timeLabel_mb_1
            // 
            this.timeLabel_mb_1.AutoSize = true;
            this.timeLabel_mb_1.Location = new System.Drawing.Point(6, 100);
            this.timeLabel_mb_1.Name = "timeLabel_mb_1";
            this.timeLabel_mb_1.Size = new System.Drawing.Size(107, 13);
            this.timeLabel_mb_1.TabIndex = 18;
            this.timeLabel_mb_1.Text = "Время обнов. даты:";
            // 
            // Button_notRead_mb
            // 
            this.Button_notRead_mb.Location = new System.Drawing.Point(6, 35);
            this.Button_notRead_mb.Name = "Button_notRead_mb";
            this.Button_notRead_mb.Size = new System.Drawing.Size(346, 23);
            this.Button_notRead_mb.TabIndex = 14;
            this.Button_notRead_mb.Text = "Стоп";
            this.Button_notRead_mb.UseVisualStyleBackColor = true;
            this.Button_notRead_mb.Click += new System.EventHandler(this.Button_notRead_mb_Click);
            // 
            // progressBarRead_mb
            // 
            this.progressBarRead_mb.Location = new System.Drawing.Point(6, 64);
            this.progressBarRead_mb.MarqueeAnimationSpeed = 50;
            this.progressBarRead_mb.Name = "progressBarRead_mb";
            this.progressBarRead_mb.Size = new System.Drawing.Size(346, 23);
            this.progressBarRead_mb.Step = 1;
            this.progressBarRead_mb.TabIndex = 13;
            // 
            // timeLabel_mb
            // 
            this.timeLabel_mb.AutoSize = true;
            this.timeLabel_mb.Location = new System.Drawing.Point(6, 260);
            this.timeLabel_mb.Name = "timeLabel_mb";
            this.timeLabel_mb.Size = new System.Drawing.Size(144, 13);
            this.timeLabel_mb.TabIndex = 12;
            this.timeLabel_mb.Text = "Время последнего цикла : ";
            // 
            // Button_Read_mb
            // 
            this.Button_Read_mb.Location = new System.Drawing.Point(6, 6);
            this.Button_Read_mb.Name = "Button_Read_mb";
            this.Button_Read_mb.Size = new System.Drawing.Size(346, 23);
            this.Button_Read_mb.TabIndex = 7;
            this.Button_Read_mb.Text = "Пуск";
            this.Button_Read_mb.UseVisualStyleBackColor = true;
            this.Button_Read_mb.Click += new System.EventHandler(this.Button_Read_mb_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.timeLabel_s7_5);
            this.tabPage1.Controls.Add(this.timeLabel_s7_4);
            this.tabPage1.Controls.Add(this.timeLabel_s7_3);
            this.tabPage1.Controls.Add(this.timeLabel_s7_2);
            this.tabPage1.Controls.Add(this.timeLabel_s7_1);
            this.tabPage1.Controls.Add(this.timeLabel_s7);
            this.tabPage1.Controls.Add(this.progressBarRead_s7);
            this.tabPage1.Controls.Add(this.Button_notRead_s7);
            this.tabPage1.Controls.Add(this.Button_Read_s7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 256);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "S7";
            // 
            // timeLabel_s7_5
            // 
            this.timeLabel_s7_5.AutoSize = true;
            this.timeLabel_s7_5.Location = new System.Drawing.Point(6, 180);
            this.timeLabel_s7_5.Name = "timeLabel_s7_5";
            this.timeLabel_s7_5.Size = new System.Drawing.Size(85, 13);
            this.timeLabel_s7_5.TabIndex = 17;
            this.timeLabel_s7_5.Text = "Время записи: ";
            // 
            // timeLabel_s7_4
            // 
            this.timeLabel_s7_4.AutoSize = true;
            this.timeLabel_s7_4.Location = new System.Drawing.Point(6, 160);
            this.timeLabel_s7_4.Name = "timeLabel_s7_4";
            this.timeLabel_s7_4.Size = new System.Drawing.Size(106, 13);
            this.timeLabel_s7_4.TabIndex = 16;
            this.timeLabel_s7_4.Text = "Время сообщений: ";
            // 
            // timeLabel_s7_3
            // 
            this.timeLabel_s7_3.AutoSize = true;
            this.timeLabel_s7_3.Location = new System.Drawing.Point(6, 140);
            this.timeLabel_s7_3.Name = "timeLabel_s7_3";
            this.timeLabel_s7_3.Size = new System.Drawing.Size(78, 13);
            this.timeLabel_s7_3.TabIndex = 15;
            this.timeLabel_s7_3.Text = "Время PLC_2:";
            // 
            // timeLabel_s7_2
            // 
            this.timeLabel_s7_2.AutoSize = true;
            this.timeLabel_s7_2.Location = new System.Drawing.Point(6, 120);
            this.timeLabel_s7_2.Name = "timeLabel_s7_2";
            this.timeLabel_s7_2.Size = new System.Drawing.Size(96, 13);
            this.timeLabel_s7_2.TabIndex = 14;
            this.timeLabel_s7_2.Text = "Время PLC_3679:";
            // 
            // timeLabel_s7_1
            // 
            this.timeLabel_s7_1.AutoSize = true;
            this.timeLabel_s7_1.Location = new System.Drawing.Point(6, 100);
            this.timeLabel_s7_1.Name = "timeLabel_s7_1";
            this.timeLabel_s7_1.Size = new System.Drawing.Size(107, 13);
            this.timeLabel_s7_1.TabIndex = 13;
            this.timeLabel_s7_1.Text = "Время обнов. даты:";
            // 
            // timeLabel_s7
            // 
            this.timeLabel_s7.AutoSize = true;
            this.timeLabel_s7.Location = new System.Drawing.Point(6, 200);
            this.timeLabel_s7.Name = "timeLabel_s7";
            this.timeLabel_s7.Size = new System.Drawing.Size(141, 13);
            this.timeLabel_s7.TabIndex = 11;
            this.timeLabel_s7.Text = "Время последнего цикла: ";
            // 
            // progressBarRead_s7
            // 
            this.progressBarRead_s7.Location = new System.Drawing.Point(6, 64);
            this.progressBarRead_s7.MarqueeAnimationSpeed = 50;
            this.progressBarRead_s7.Name = "progressBarRead_s7";
            this.progressBarRead_s7.Size = new System.Drawing.Size(346, 23);
            this.progressBarRead_s7.Step = 1;
            this.progressBarRead_s7.TabIndex = 10;
            // 
            // Button_notRead_s7
            // 
            this.Button_notRead_s7.Location = new System.Drawing.Point(6, 35);
            this.Button_notRead_s7.Name = "Button_notRead_s7";
            this.Button_notRead_s7.Size = new System.Drawing.Size(346, 23);
            this.Button_notRead_s7.TabIndex = 9;
            this.Button_notRead_s7.Text = "Стоп";
            this.Button_notRead_s7.UseVisualStyleBackColor = true;
            this.Button_notRead_s7.Click += new System.EventHandler(this.Button_notRead_s7_Click);
            // 
            // Button_Read_s7
            // 
            this.Button_Read_s7.Location = new System.Drawing.Point(6, 6);
            this.Button_Read_s7.Name = "Button_Read_s7";
            this.Button_Read_s7.Size = new System.Drawing.Size(346, 23);
            this.Button_Read_s7.TabIndex = 8;
            this.Button_Read_s7.Text = "Пуск";
            this.Button_Read_s7.UseVisualStyleBackColor = true;
            this.Button_Read_s7.Click += new System.EventHandler(this.Button_Read_s7_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(366, 306);
            this.tabControl.TabIndex = 8;
            // 
            // timeLabel_mb_8
            // 
            this.timeLabel_mb_8.AutoSize = true;
            this.timeLabel_mb_8.Location = new System.Drawing.Point(6, 220);
            this.timeLabel_mb_8.Name = "timeLabel_mb_8";
            this.timeLabel_mb_8.Size = new System.Drawing.Size(82, 13);
            this.timeLabel_mb_8.TabIndex = 26;
            this.timeLabel_mb_8.Text = "Время Electro: ";
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 390);
            this.Controls.Add(this.tabControl);
            this.KeyPreview = true;
            this.Name = "MainScreen";
            this.Text = "HeinekenDC";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorkerRead;
        private System.ComponentModel.BackgroundWorker bgWReadModBus;
        private System.ComponentModel.BackgroundWorker bgWMessages;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Button_notRead_mb;
        private System.Windows.Forms.ProgressBar progressBarRead_mb;
        private System.Windows.Forms.Label timeLabel_mb;
        private System.Windows.Forms.Button Button_Read_mb;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label timeLabel_s7;
        private System.Windows.Forms.ProgressBar progressBarRead_s7;
        private System.Windows.Forms.Button Button_notRead_s7;
        private System.Windows.Forms.Button Button_Read_s7;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Label timeLabel_s7_1;
        private System.Windows.Forms.Label timeLabel_s7_5;
        private System.Windows.Forms.Label timeLabel_s7_4;
        private System.Windows.Forms.Label timeLabel_s7_3;
        private System.Windows.Forms.Label timeLabel_s7_2;
        private System.Windows.Forms.Label timeLabel_mb_7;
        private System.Windows.Forms.Label timeLabel_mb_4;
        private System.Windows.Forms.Label timeLabel_mb_3;
        private System.Windows.Forms.Label timeLabel_mb_2;
        private System.Windows.Forms.Label timeLabel_mb_1;
        private System.Windows.Forms.Label timeLabel_mb_6;
        private System.Windows.Forms.Label timeLabel_mb_5;
        private System.Windows.Forms.Label timeLabel_mb_8;
    }
}

