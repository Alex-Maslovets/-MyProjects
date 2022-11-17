
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.backgroundWorkerRead = new System.ComponentModel.BackgroundWorker();
            this.bgWReadModBus = new System.ComponentModel.BackgroundWorker();
            this.bgWMessages = new System.ComponentModel.BackgroundWorker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Button_notRead_mb = new System.Windows.Forms.Button();
            this.progressBarRead_mb = new System.Windows.Forms.ProgressBar();
            this.timeLabel_mb = new System.Windows.Forms.Label();
            this.Button_Read_mb = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.timeLabel_s7 = new System.Windows.Forms.Label();
            this.progressBarRead_s7 = new System.Windows.Forms.ProgressBar();
            this.Button_notRead_s7 = new System.Windows.Forms.Button();
            this.Button_Read_s7 = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.button1 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
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
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(763, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(380, 34);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1";
            treeNode2.Name = "Node2";
            treeNode2.Text = "Node2";
            treeNode3.Name = "Node3";
            treeNode3.Text = "Node3";
            treeNode4.Name = "Node0";
            treeNode4.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.Size = new System.Drawing.Size(377, 107);
            this.treeView1.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(763, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(763, 118);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Обновить";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 332);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

