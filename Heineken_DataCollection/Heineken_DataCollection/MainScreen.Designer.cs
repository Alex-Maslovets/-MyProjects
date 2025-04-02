
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            backgroundWorkerRead = new System.ComponentModel.BackgroundWorker();
            bgWReadModBus = new System.ComponentModel.BackgroundWorker();
            bgWMessages = new System.ComponentModel.BackgroundWorker();
            tabPage2 = new System.Windows.Forms.TabPage();
            timeLabel_mb_8 = new System.Windows.Forms.Label();
            timeLabel_mb_6 = new System.Windows.Forms.Label();
            timeLabel_mb_5 = new System.Windows.Forms.Label();
            timeLabel_mb_7 = new System.Windows.Forms.Label();
            timeLabel_mb_4 = new System.Windows.Forms.Label();
            timeLabel_mb_3 = new System.Windows.Forms.Label();
            timeLabel_mb_2 = new System.Windows.Forms.Label();
            timeLabel_mb_1 = new System.Windows.Forms.Label();
            Button_notRead_mb = new System.Windows.Forms.Button();
            progressBarRead_mb = new System.Windows.Forms.ProgressBar();
            timeLabel_mb = new System.Windows.Forms.Label();
            Button_Read_mb = new System.Windows.Forms.Button();
            tabPage1 = new System.Windows.Forms.TabPage();
            timeLabel_s7_5 = new System.Windows.Forms.Label();
            timeLabel_s7_4 = new System.Windows.Forms.Label();
            timeLabel_s7_3 = new System.Windows.Forms.Label();
            timeLabel_s7_2 = new System.Windows.Forms.Label();
            timeLabel_s7_1 = new System.Windows.Forms.Label();
            timeLabel_s7 = new System.Windows.Forms.Label();
            progressBarRead_s7 = new System.Windows.Forms.ProgressBar();
            Button_notRead_s7 = new System.Windows.Forms.Button();
            Button_Read_s7 = new System.Windows.Forms.Button();
            tabControl = new System.Windows.Forms.TabControl();
            button1 = new System.Windows.Forms.Button();
            tabPage2.SuspendLayout();
            tabPage1.SuspendLayout();
            tabControl.SuspendLayout();
            SuspendLayout();
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.Transparent;
            tabPage2.Controls.Add(timeLabel_mb_8);
            tabPage2.Controls.Add(timeLabel_mb_6);
            tabPage2.Controls.Add(timeLabel_mb_5);
            tabPage2.Controls.Add(timeLabel_mb_7);
            tabPage2.Controls.Add(timeLabel_mb_4);
            tabPage2.Controls.Add(timeLabel_mb_3);
            tabPage2.Controls.Add(timeLabel_mb_2);
            tabPage2.Controls.Add(timeLabel_mb_1);
            tabPage2.Controls.Add(Button_notRead_mb);
            tabPage2.Controls.Add(progressBarRead_mb);
            tabPage2.Controls.Add(timeLabel_mb);
            tabPage2.Controls.Add(Button_Read_mb);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Size = new System.Drawing.Size(419, 325);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Modbus";
            // 
            // timeLabel_mb_8
            // 
            timeLabel_mb_8.AutoSize = true;
            timeLabel_mb_8.Location = new System.Drawing.Point(7, 254);
            timeLabel_mb_8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_8.Name = "timeLabel_mb_8";
            timeLabel_mb_8.Size = new System.Drawing.Size(87, 15);
            timeLabel_mb_8.TabIndex = 26;
            timeLabel_mb_8.Text = "Время Electro: ";
            // 
            // timeLabel_mb_6
            // 
            timeLabel_mb_6.AutoSize = true;
            timeLabel_mb_6.Location = new System.Drawing.Point(7, 231);
            timeLabel_mb_6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_6.Name = "timeLabel_mb_6";
            timeLabel_mb_6.Size = new System.Drawing.Size(88, 15);
            timeLabel_mb_6.TabIndex = 24;
            timeLabel_mb_6.Text = "Время TH_Filtr:";
            // 
            // timeLabel_mb_5
            // 
            timeLabel_mb_5.AutoSize = true;
            timeLabel_mb_5.Location = new System.Drawing.Point(7, 208);
            timeLabel_mb_5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_5.Name = "timeLabel_mb_5";
            timeLabel_mb_5.Size = new System.Drawing.Size(110, 15);
            timeLabel_mb_5.TabIndex = 23;
            timeLabel_mb_5.Text = "Время TH_EnBlock:";
            // 
            // timeLabel_mb_7
            // 
            timeLabel_mb_7.AutoSize = true;
            timeLabel_mb_7.Location = new System.Drawing.Point(7, 277);
            timeLabel_mb_7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_7.Name = "timeLabel_mb_7";
            timeLabel_mb_7.Size = new System.Drawing.Size(89, 15);
            timeLabel_mb_7.TabIndex = 22;
            timeLabel_mb_7.Text = "Время записи: ";
            // 
            // timeLabel_mb_4
            // 
            timeLabel_mb_4.AutoSize = true;
            timeLabel_mb_4.Location = new System.Drawing.Point(7, 185);
            timeLabel_mb_4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_4.Name = "timeLabel_mb_4";
            timeLabel_mb_4.Size = new System.Drawing.Size(91, 15);
            timeLabel_mb_4.TabIndex = 21;
            timeLabel_mb_4.Text = "Время TH_VAO:";
            // 
            // timeLabel_mb_3
            // 
            timeLabel_mb_3.AutoSize = true;
            timeLabel_mb_3.Location = new System.Drawing.Point(7, 162);
            timeLabel_mb_3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_3.Name = "timeLabel_mb_3";
            timeLabel_mb_3.Size = new System.Drawing.Size(90, 15);
            timeLabel_mb_3.TabIndex = 20;
            timeLabel_mb_3.Text = "Время TH_BLO:";
            // 
            // timeLabel_mb_2
            // 
            timeLabel_mb_2.AutoSize = true;
            timeLabel_mb_2.Location = new System.Drawing.Point(7, 138);
            timeLabel_mb_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_2.Name = "timeLabel_mb_2";
            timeLabel_mb_2.Size = new System.Drawing.Size(93, 15);
            timeLabel_mb_2.TabIndex = 19;
            timeLabel_mb_2.Text = "Время TH_Pack:";
            // 
            // timeLabel_mb_1
            // 
            timeLabel_mb_1.AutoSize = true;
            timeLabel_mb_1.Location = new System.Drawing.Point(7, 115);
            timeLabel_mb_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb_1.Name = "timeLabel_mb_1";
            timeLabel_mb_1.Size = new System.Drawing.Size(114, 15);
            timeLabel_mb_1.TabIndex = 18;
            timeLabel_mb_1.Text = "Время обнов. даты:";
            // 
            // Button_notRead_mb
            // 
            Button_notRead_mb.Location = new System.Drawing.Point(7, 40);
            Button_notRead_mb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_notRead_mb.Name = "Button_notRead_mb";
            Button_notRead_mb.Size = new System.Drawing.Size(404, 27);
            Button_notRead_mb.TabIndex = 14;
            Button_notRead_mb.Text = "Стоп";
            Button_notRead_mb.UseVisualStyleBackColor = true;
            Button_notRead_mb.Click += Button_notRead_mb_Click;
            // 
            // progressBarRead_mb
            // 
            progressBarRead_mb.Location = new System.Drawing.Point(7, 74);
            progressBarRead_mb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            progressBarRead_mb.MarqueeAnimationSpeed = 50;
            progressBarRead_mb.Name = "progressBarRead_mb";
            progressBarRead_mb.Size = new System.Drawing.Size(404, 27);
            progressBarRead_mb.Step = 1;
            progressBarRead_mb.TabIndex = 13;
            // 
            // timeLabel_mb
            // 
            timeLabel_mb.AutoSize = true;
            timeLabel_mb.Location = new System.Drawing.Point(7, 300);
            timeLabel_mb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_mb.Name = "timeLabel_mb";
            timeLabel_mb.Size = new System.Drawing.Size(154, 15);
            timeLabel_mb.TabIndex = 12;
            timeLabel_mb.Text = "Время последнего цикла : ";
            // 
            // Button_Read_mb
            // 
            Button_Read_mb.Location = new System.Drawing.Point(7, 7);
            Button_Read_mb.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_Read_mb.Name = "Button_Read_mb";
            Button_Read_mb.Size = new System.Drawing.Size(404, 27);
            Button_Read_mb.TabIndex = 7;
            Button_Read_mb.Text = "Пуск";
            Button_Read_mb.UseVisualStyleBackColor = true;
            Button_Read_mb.Click += Button_Read_mb_Click;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.Transparent;
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(timeLabel_s7_5);
            tabPage1.Controls.Add(timeLabel_s7_4);
            tabPage1.Controls.Add(timeLabel_s7_3);
            tabPage1.Controls.Add(timeLabel_s7_2);
            tabPage1.Controls.Add(timeLabel_s7_1);
            tabPage1.Controls.Add(timeLabel_s7);
            tabPage1.Controls.Add(progressBarRead_s7);
            tabPage1.Controls.Add(Button_notRead_s7);
            tabPage1.Controls.Add(Button_Read_s7);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(419, 325);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "S7";
            // 
            // timeLabel_s7_5
            // 
            timeLabel_s7_5.AutoSize = true;
            timeLabel_s7_5.Location = new System.Drawing.Point(7, 208);
            timeLabel_s7_5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_s7_5.Name = "timeLabel_s7_5";
            timeLabel_s7_5.Size = new System.Drawing.Size(89, 15);
            timeLabel_s7_5.TabIndex = 17;
            timeLabel_s7_5.Text = "Время записи: ";
            // 
            // timeLabel_s7_4
            // 
            timeLabel_s7_4.AutoSize = true;
            timeLabel_s7_4.Location = new System.Drawing.Point(7, 185);
            timeLabel_s7_4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_s7_4.Name = "timeLabel_s7_4";
            timeLabel_s7_4.Size = new System.Drawing.Size(116, 15);
            timeLabel_s7_4.TabIndex = 16;
            timeLabel_s7_4.Text = "Время сообщений: ";
            // 
            // timeLabel_s7_3
            // 
            timeLabel_s7_3.AutoSize = true;
            timeLabel_s7_3.Location = new System.Drawing.Point(7, 162);
            timeLabel_s7_3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_s7_3.Name = "timeLabel_s7_3";
            timeLabel_s7_3.Size = new System.Drawing.Size(80, 15);
            timeLabel_s7_3.TabIndex = 15;
            timeLabel_s7_3.Text = "Время PLC_2:";
            // 
            // timeLabel_s7_2
            // 
            timeLabel_s7_2.AutoSize = true;
            timeLabel_s7_2.Location = new System.Drawing.Point(7, 138);
            timeLabel_s7_2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_s7_2.Name = "timeLabel_s7_2";
            timeLabel_s7_2.Size = new System.Drawing.Size(98, 15);
            timeLabel_s7_2.TabIndex = 14;
            timeLabel_s7_2.Text = "Время PLC_3679:";
            // 
            // timeLabel_s7_1
            // 
            timeLabel_s7_1.AutoSize = true;
            timeLabel_s7_1.Location = new System.Drawing.Point(7, 115);
            timeLabel_s7_1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_s7_1.Name = "timeLabel_s7_1";
            timeLabel_s7_1.Size = new System.Drawing.Size(114, 15);
            timeLabel_s7_1.TabIndex = 13;
            timeLabel_s7_1.Text = "Время обнов. даты:";
            // 
            // timeLabel_s7
            // 
            timeLabel_s7.AutoSize = true;
            timeLabel_s7.Location = new System.Drawing.Point(7, 231);
            timeLabel_s7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            timeLabel_s7.Name = "timeLabel_s7";
            timeLabel_s7.Size = new System.Drawing.Size(151, 15);
            timeLabel_s7.TabIndex = 11;
            timeLabel_s7.Text = "Время последнего цикла: ";
            // 
            // progressBarRead_s7
            // 
            progressBarRead_s7.Location = new System.Drawing.Point(7, 74);
            progressBarRead_s7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            progressBarRead_s7.MarqueeAnimationSpeed = 50;
            progressBarRead_s7.Name = "progressBarRead_s7";
            progressBarRead_s7.Size = new System.Drawing.Size(404, 27);
            progressBarRead_s7.Step = 1;
            progressBarRead_s7.TabIndex = 10;
            // 
            // Button_notRead_s7
            // 
            Button_notRead_s7.Location = new System.Drawing.Point(7, 40);
            Button_notRead_s7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_notRead_s7.Name = "Button_notRead_s7";
            Button_notRead_s7.Size = new System.Drawing.Size(404, 27);
            Button_notRead_s7.TabIndex = 9;
            Button_notRead_s7.Text = "Стоп";
            Button_notRead_s7.UseVisualStyleBackColor = true;
            Button_notRead_s7.Click += Button_notRead_s7_Click;
            // 
            // Button_Read_s7
            // 
            Button_Read_s7.Location = new System.Drawing.Point(7, 7);
            Button_Read_s7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Button_Read_s7.Name = "Button_Read_s7";
            Button_Read_s7.Size = new System.Drawing.Size(404, 27);
            Button_Read_s7.TabIndex = 8;
            Button_Read_s7.Text = "Пуск";
            Button_Read_s7.UseVisualStyleBackColor = true;
            Button_Read_s7.Click += Button_Read_s7_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Location = new System.Drawing.Point(14, 12);
            tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(427, 353);
            tabControl.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(8, 292);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(196, 27);
            button1.TabIndex = 18;
            button1.Text = "Тестовое сообщение";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(454, 372);
            Controls.Add(tabControl);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "MainScreen";
            Text = "Siberia__DC";
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabControl.ResumeLayout(false);
            ResumeLayout(false);

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
        private System.Windows.Forms.Button button1;
    }
}

