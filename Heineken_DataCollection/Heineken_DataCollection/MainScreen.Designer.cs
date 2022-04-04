
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.timeLabel_s7 = new System.Windows.Forms.Label();
            this.progressBarRead_s7 = new System.Windows.Forms.ProgressBar();
            this.Button_notRead_s7 = new System.Windows.Forms.Button();
            this.Button_Read_s7 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Button_notRead_mb = new System.Windows.Forms.Button();
            this.progressBarRead_mb = new System.Windows.Forms.ProgressBar();
            this.timeLabel_mb = new System.Windows.Forms.Label();
            this.Button_Read_mb = new System.Windows.Forms.Button();
            this.bgWReadModBus = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(366, 152);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.timeLabel_s7);
            this.tabPage1.Controls.Add(this.progressBarRead_s7);
            this.tabPage1.Controls.Add(this.Button_notRead_s7);
            this.tabPage1.Controls.Add(this.Button_Read_s7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 126);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "S7";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "Сообщение в Телеграм";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timeLabel_s7
            // 
            this.timeLabel_s7.AutoSize = true;
            this.timeLabel_s7.Location = new System.Drawing.Point(6, 110);
            this.timeLabel_s7.Name = "timeLabel_s7";
            this.timeLabel_s7.Size = new System.Drawing.Size(144, 13);
            this.timeLabel_s7.TabIndex = 11;
            this.timeLabel_s7.Text = "Время последнего цикла : ";
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Button_notRead_mb);
            this.tabPage2.Controls.Add(this.progressBarRead_mb);
            this.tabPage2.Controls.Add(this.timeLabel_mb);
            this.tabPage2.Controls.Add(this.Button_Read_mb);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(358, 126);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Modbus";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.timeLabel_mb.Location = new System.Drawing.Point(6, 110);
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(288, 110);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 28);
            this.button2.TabIndex = 13;
            this.button2.Text = "Тест";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 167);
            this.Controls.Add(this.tabControl);
            this.KeyPreview = true;
            this.Name = "MainScreen";
            this.Text = "HeinekenDC";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorkerRead;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label timeLabel_s7;
        private System.Windows.Forms.ProgressBar progressBarRead_s7;
        private System.Windows.Forms.Button Button_notRead_s7;
        private System.Windows.Forms.Button Button_Read_s7;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label timeLabel_mb;
        private System.Windows.Forms.Button Button_Read_mb;
        private System.ComponentModel.BackgroundWorker bgWReadModBus;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBarRead_mb;
        private System.Windows.Forms.Button Button_notRead_mb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

