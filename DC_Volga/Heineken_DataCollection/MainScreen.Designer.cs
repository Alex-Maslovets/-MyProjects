﻿
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
            this.backgroundWorkerRead = new System.ComponentModel.BackgroundWorker();
            this.bgWReadModBus = new System.ComponentModel.BackgroundWorker();
            this.bgWMessages = new System.ComponentModel.BackgroundWorker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.timeLabel_s7_4 = new System.Windows.Forms.Label();
            this.timeLabel_s7 = new System.Windows.Forms.Label();
            this.progressBarRead_s7 = new System.Windows.Forms.ProgressBar();
            this.Button_notRead_s7 = new System.Windows.Forms.Button();
            this.Button_Read_s7 = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.timeLabel_s7_4);
            this.tabPage1.Controls.Add(this.timeLabel_s7);
            this.tabPage1.Controls.Add(this.progressBarRead_s7);
            this.tabPage1.Controls.Add(this.Button_notRead_s7);
            this.tabPage1.Controls.Add(this.Button_Read_s7);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 144);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "S7";
            // 
            // timeLabel_s7_4
            // 
            this.timeLabel_s7_4.AutoSize = true;
            this.timeLabel_s7_4.Location = new System.Drawing.Point(6, 100);
            this.timeLabel_s7_4.Name = "timeLabel_s7_4";
            this.timeLabel_s7_4.Size = new System.Drawing.Size(106, 13);
            this.timeLabel_s7_4.TabIndex = 16;
            this.timeLabel_s7_4.Text = "Время сообщений: ";
            // 
            // timeLabel_s7
            // 
            this.timeLabel_s7.AutoSize = true;
            this.timeLabel_s7.Location = new System.Drawing.Point(6, 120);
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
            this.Button_notRead_s7.Text = "СТОП";
            this.Button_notRead_s7.UseVisualStyleBackColor = true;
            this.Button_notRead_s7.Click += new System.EventHandler(this.Button_notRead_s7_Click);
            // 
            // Button_Read_s7
            // 
            this.Button_Read_s7.Location = new System.Drawing.Point(6, 6);
            this.Button_Read_s7.Name = "Button_Read_s7";
            this.Button_Read_s7.Size = new System.Drawing.Size(346, 23);
            this.Button_Read_s7.TabIndex = 8;
            this.Button_Read_s7.Text = "ПУСК";
            this.Button_Read_s7.UseVisualStyleBackColor = true;
            this.Button_Read_s7.Click += new System.EventHandler(this.Button_Read_s7_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(366, 170);
            this.tabControl.TabIndex = 8;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(389, 191);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainScreen";
            this.Text = "Data_Collection_Volga";
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorkerRead;
        private System.ComponentModel.BackgroundWorker bgWReadModBus;
        private System.ComponentModel.BackgroundWorker bgWMessages;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label timeLabel_s7_4;
        private System.Windows.Forms.Label timeLabel_s7;
        private System.Windows.Forms.ProgressBar progressBarRead_s7;
        private System.Windows.Forms.Button Button_notRead_s7;
        private System.Windows.Forms.Button Button_Read_s7;
        private System.Windows.Forms.TabControl tabControl;
    }
}

