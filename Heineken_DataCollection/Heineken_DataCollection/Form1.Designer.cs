
namespace Heineken_DataCollection
{
    partial class Form1
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
            this.timeLabel_s7 = new System.Windows.Forms.Label();
            this.progressBarRead_s7 = new System.Windows.Forms.ProgressBar();
            this.Button_notRead_s7 = new System.Windows.Forms.Button();
            this.Button_Read_s7 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.timeLabel_mb = new System.Windows.Forms.Label();
            this.Button_Read_mb = new System.Windows.Forms.Button();
            this.bgWReadModBus = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
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
            // timeLabel_s7
            // 
            this.timeLabel_s7.AutoSize = true;
            this.timeLabel_s7.Location = new System.Drawing.Point(6, 101);
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
            // timeLabel_mb
            // 
            this.timeLabel_mb.AutoSize = true;
            this.timeLabel_mb.Location = new System.Drawing.Point(6, 52);
            this.timeLabel_mb.Name = "timeLabel_mb";
            this.timeLabel_mb.Size = new System.Drawing.Size(144, 13);
            this.timeLabel_mb.TabIndex = 12;
            this.timeLabel_mb.Text = "Время последнего цикла : ";
            // 
            // Button_Read_mb
            // 
            this.Button_Read_mb.Location = new System.Drawing.Point(9, 6);
            this.Button_Read_mb.Name = "Button_Read_mb";
            this.Button_Read_mb.Size = new System.Drawing.Size(86, 23);
            this.Button_Read_mb.TabIndex = 7;
            this.Button_Read_mb.Text = "ModBus";
            this.Button_Read_mb.UseVisualStyleBackColor = true;
            this.Button_Read_mb.Click += new System.EventHandler(this.Button_Read_mb_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(274, 170);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(274, 196);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(274, 222);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(274, 248);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 12;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(274, 274);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "IP-адрес";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Rack";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Slot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Размер массива в байтах";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(152, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Номер DB";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(69, 300);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Стартовая позиция чтения";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(274, 300);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 326);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(183, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Колличество считываемых Real\'ов";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(274, 326);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 432);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tabControl);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "HeinekenDC";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox7;
    }
}

