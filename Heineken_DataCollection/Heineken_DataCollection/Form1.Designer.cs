
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
            this.Button_Read = new System.Windows.Forms.Button();
            this.Button_notRead = new System.Windows.Forms.Button();
            this.progressBarRead = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerRead = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Button_Read
            // 
            this.Button_Read.Location = new System.Drawing.Point(12, 12);
            this.Button_Read.Name = "Button_Read";
            this.Button_Read.Size = new System.Drawing.Size(176, 23);
            this.Button_Read.TabIndex = 2;
            this.Button_Read.Text = "Пуск";
            this.Button_Read.UseVisualStyleBackColor = true;
            this.Button_Read.Click += new System.EventHandler(this.Button_Read_Click);
            // 
            // Button_notRead
            // 
            this.Button_notRead.Location = new System.Drawing.Point(12, 41);
            this.Button_notRead.Name = "Button_notRead";
            this.Button_notRead.Size = new System.Drawing.Size(176, 23);
            this.Button_notRead.TabIndex = 3;
            this.Button_notRead.Text = "Стоп";
            this.Button_notRead.UseVisualStyleBackColor = true;
            this.Button_notRead.Click += new System.EventHandler(this.Button_notRead_Click);
            // 
            // progressBarRead
            // 
            this.progressBarRead.Location = new System.Drawing.Point(12, 70);
            this.progressBarRead.Name = "progressBarRead";
            this.progressBarRead.Size = new System.Drawing.Size(176, 23);
            this.progressBarRead.Step = 1;
            this.progressBarRead.TabIndex = 5;
            this.progressBarRead.MarqueeAnimationSpeed = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 106);
            this.Controls.Add(this.progressBarRead);
            this.Controls.Add(this.Button_notRead);
            this.Controls.Add(this.Button_Read);
            this.Name = "Form1";
            this.Text = "HeinekenDC";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Button_Read;
        private System.Windows.Forms.Button Button_notRead;
        private System.Windows.Forms.ProgressBar progressBarRead;
        private System.ComponentModel.BackgroundWorker backgroundWorkerRead;
    }
}

