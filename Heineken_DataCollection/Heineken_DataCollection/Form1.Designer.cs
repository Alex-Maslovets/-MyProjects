
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
            this.Button_Connect = new System.Windows.Forms.Button();
            this.Button_disConnect = new System.Windows.Forms.Button();
            this.Button_Read = new System.Windows.Forms.Button();
            this.Button_notRead = new System.Windows.Forms.Button();
            this.progressBarConncet = new System.Windows.Forms.ProgressBar();
            this.progressBarRead = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerRead = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Button_Connect
            // 
            this.Button_Connect.Location = new System.Drawing.Point(76, 12);
            this.Button_Connect.Name = "Button_Connect";
            this.Button_Connect.Size = new System.Drawing.Size(176, 23);
            this.Button_Connect.TabIndex = 0;
            this.Button_Connect.Text = "Подключиться";
            this.Button_Connect.UseVisualStyleBackColor = true;
            this.Button_Connect.Click += new System.EventHandler(this.Button_Connect_Click);
            // 
            // Button_disConnect
            // 
            this.Button_disConnect.Location = new System.Drawing.Point(76, 41);
            this.Button_disConnect.Name = "Button_disConnect";
            this.Button_disConnect.Size = new System.Drawing.Size(176, 23);
            this.Button_disConnect.TabIndex = 1;
            this.Button_disConnect.Text = "Отключиться";
            this.Button_disConnect.UseVisualStyleBackColor = true;
            this.Button_disConnect.Click += new System.EventHandler(this.Button_disConnect_Click);
            // 
            // Button_Read
            // 
            this.Button_Read.Location = new System.Drawing.Point(76, 99);
            this.Button_Read.Name = "Button_Read";
            this.Button_Read.Size = new System.Drawing.Size(176, 23);
            this.Button_Read.TabIndex = 2;
            this.Button_Read.Text = "Пуск";
            this.Button_Read.UseVisualStyleBackColor = true;
            this.Button_Read.Click += new System.EventHandler(this.Button_Read_Click);
            // 
            // Button_notRead
            // 
            this.Button_notRead.Location = new System.Drawing.Point(76, 128);
            this.Button_notRead.Name = "Button_notRead";
            this.Button_notRead.Size = new System.Drawing.Size(176, 23);
            this.Button_notRead.TabIndex = 3;
            this.Button_notRead.Text = "Стоп";
            this.Button_notRead.UseVisualStyleBackColor = true;
            this.Button_notRead.Click += new System.EventHandler(this.Button_notRead_Click);
            // 
            // progressBarConncet
            // 
            this.progressBarConncet.Location = new System.Drawing.Point(76, 70);
            this.progressBarConncet.Name = "progressBarConncet";
            this.progressBarConncet.Size = new System.Drawing.Size(176, 23);
            this.progressBarConncet.TabIndex = 4;
            // 
            // progressBarRead
            // 
            this.progressBarRead.Location = new System.Drawing.Point(76, 157);
            this.progressBarRead.Name = "progressBarRead";
            this.progressBarRead.Size = new System.Drawing.Size(176, 23);
            this.progressBarRead.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 213);
            this.Controls.Add(this.progressBarRead);
            this.Controls.Add(this.progressBarConncet);
            this.Controls.Add(this.Button_notRead);
            this.Controls.Add(this.Button_Read);
            this.Controls.Add(this.Button_disConnect);
            this.Controls.Add(this.Button_Connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Connect;
        private System.Windows.Forms.Button Button_disConnect;
        private System.Windows.Forms.Button Button_Read;
        private System.Windows.Forms.Button Button_notRead;
        private System.Windows.Forms.ProgressBar progressBarConncet;
        private System.Windows.Forms.ProgressBar progressBarRead;
        private System.ComponentModel.BackgroundWorker backgroundWorkerRead;
    }
}

