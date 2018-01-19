namespace LogIn
{
    partial class MainWindow
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
            this.StatusLog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Schedule_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Create_Schedule = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_Schedule = new System.Windows.Forms.ToolStripMenuItem();
            this.Open_Schedule = new System.Windows.Forms.ToolStripMenuItem();
            this.sendNotificationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartSendingAndRecevieving = new System.ComponentModel.BackgroundWorker();
            this.ShowScheduleGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowScheduleGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusLog
            // 
            this.StatusLog.Location = new System.Drawing.Point(109, 390);
            this.StatusLog.Name = "StatusLog";
            this.StatusLog.Size = new System.Drawing.Size(602, 154);
            this.StatusLog.TabIndex = 4;
            this.StatusLog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(344, 374);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Status Log";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Schedule_MenuItem,
            this.sendNotificationsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(979, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Schedule_MenuItem
            // 
            this.Schedule_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Create_Schedule,
            this.Save_Schedule,
            this.Open_Schedule});
            this.Schedule_MenuItem.Name = "Schedule_MenuItem";
            this.Schedule_MenuItem.Size = new System.Drawing.Size(67, 20);
            this.Schedule_MenuItem.Text = "Schedule";
            // 
            // Create_Schedule
            // 
            this.Create_Schedule.Name = "Create_Schedule";
            this.Create_Schedule.Size = new System.Drawing.Size(146, 22);
            this.Create_Schedule.Text = "Create";
            this.Create_Schedule.Click += new System.EventHandler(this.Create_Schedule_Click);
            // 
            // Save_Schedule
            // 
            this.Save_Schedule.Enabled = false;
            this.Save_Schedule.Name = "Save_Schedule";
            this.Save_Schedule.Size = new System.Drawing.Size(146, 22);
            this.Save_Schedule.Text = "Save";
            // 
            // Open_Schedule
            // 
            this.Open_Schedule.Enabled = false;
            this.Open_Schedule.Name = "Open_Schedule";
            this.Open_Schedule.Size = new System.Drawing.Size(146, 22);
            this.Open_Schedule.Text = "Open Existing";
            // 
            // sendNotificationsToolStripMenuItem
            // 
            this.sendNotificationsToolStripMenuItem.Enabled = false;
            this.sendNotificationsToolStripMenuItem.Name = "sendNotificationsToolStripMenuItem";
            this.sendNotificationsToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.sendNotificationsToolStripMenuItem.Text = "Send Notifications";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // StartSendingAndRecevieving
            // 
            this.StartSendingAndRecevieving.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StartSendingAndRecevieving_DoWork);
            // 
            // ShowScheduleGrid
            // 
            this.ShowScheduleGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.ShowScheduleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShowScheduleGrid.Location = new System.Drawing.Point(109, 60);
            this.ShowScheduleGrid.Name = "ShowScheduleGrid";
            this.ShowScheduleGrid.Size = new System.Drawing.Size(602, 268);
            this.ShowScheduleGrid.TabIndex = 10;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(979, 572);
            this.Controls.Add(this.ShowScheduleGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StatusLog);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staff Management System";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShowScheduleGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox StatusLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Schedule_MenuItem;
        private System.Windows.Forms.ToolStripMenuItem Create_Schedule;
        private System.Windows.Forms.ToolStripMenuItem Save_Schedule;
        private System.Windows.Forms.ToolStripMenuItem Open_Schedule;
        private System.Windows.Forms.ToolStripMenuItem sendNotificationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker StartSendingAndRecevieving;
        private System.Windows.Forms.DataGridView ShowScheduleGrid;
    }
}