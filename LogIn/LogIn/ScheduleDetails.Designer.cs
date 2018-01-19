namespace LogIn
{
    partial class ScheduleDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Schedule_Type = new System.Windows.Forms.ComboBox();
            this.Schedule_Location = new System.Windows.Forms.ComboBox();
            this.Schedule_Start_Date = new System.Windows.Forms.DateTimePicker();
            this.Get_Schedule = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schedule Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(76, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(23, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "WeekEnding";
            // 
            // Schedule_Type
            // 
            this.Schedule_Type.BackColor = System.Drawing.Color.Gainsboro;
            this.Schedule_Type.FormattingEnabled = true;
            this.Schedule_Type.Items.AddRange(new object[] {
            "Doctor",
            "Nurse"});
            this.Schedule_Type.Location = new System.Drawing.Point(182, 57);
            this.Schedule_Type.Margin = new System.Windows.Forms.Padding(4);
            this.Schedule_Type.Name = "Schedule_Type";
            this.Schedule_Type.Size = new System.Drawing.Size(180, 27);
            this.Schedule_Type.TabIndex = 5;
            this.Schedule_Type.SelectedIndexChanged += new System.EventHandler(this.Schedule_Type_SelectedIndexChanged);
            // 
            // Schedule_Location
            // 
            this.Schedule_Location.FormattingEnabled = true;
            this.Schedule_Location.Items.AddRange(new object[] {
            "Brighton",
            "Camberwell",
            "Clivedon",
            "Eastern",
            "Freemasons",
            "Hawthorn",
            "Richmond"});
            this.Schedule_Location.Location = new System.Drawing.Point(182, 109);
            this.Schedule_Location.Margin = new System.Windows.Forms.Padding(4);
            this.Schedule_Location.Name = "Schedule_Location";
            this.Schedule_Location.Size = new System.Drawing.Size(180, 27);
            this.Schedule_Location.TabIndex = 6;
            this.Schedule_Location.SelectedIndexChanged += new System.EventHandler(this.Schedule_Location_SelectedIndexChanged);
            // 
            // Schedule_Start_Date
            // 
            this.Schedule_Start_Date.CustomFormat = "ddMMMMyyyy - dddd";
            this.Schedule_Start_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Schedule_Start_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Schedule_Start_Date.Location = new System.Drawing.Point(129, 81);
            this.Schedule_Start_Date.Margin = new System.Windows.Forms.Padding(4);
            this.Schedule_Start_Date.Name = "Schedule_Start_Date";
            this.Schedule_Start_Date.Size = new System.Drawing.Size(250, 22);
            this.Schedule_Start_Date.TabIndex = 7;
            this.Schedule_Start_Date.ValueChanged += new System.EventHandler(this.Schedule_Start_Date_ValueChanged);
            // 
            // Get_Schedule
            // 
            this.Get_Schedule.Enabled = false;
            this.Get_Schedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Get_Schedule.Location = new System.Drawing.Point(53, 405);
            this.Get_Schedule.Margin = new System.Windows.Forms.Padding(4);
            this.Get_Schedule.Name = "Get_Schedule";
            this.Get_Schedule.Size = new System.Drawing.Size(215, 66);
            this.Get_Schedule.TabIndex = 9;
            this.Get_Schedule.Text = "Get Schedule";
            this.Get_Schedule.UseVisualStyleBackColor = true;
            this.Get_Schedule.Click += new System.EventHandler(this.Get_Schedule_Click);
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Location = new System.Drawing.Point(276, 405);
            this.Exit.Margin = new System.Windows.Forms.Padding(4);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(215, 66);
            this.Exit.TabIndex = 10;
            this.Exit.Text = "Cancel";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Schedule_Start_Date);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(53, 174);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(438, 174);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Date";
            // 
            // ScheduleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(572, 554);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Get_Schedule);
            this.Controls.Add(this.Schedule_Location);
            this.Controls.Add(this.Schedule_Type);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ScheduleDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScheduleDetails";
            this.Load += new System.EventHandler(this.ScheduleDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Schedule_Type;
        private System.Windows.Forms.ComboBox Schedule_Location;
        private System.Windows.Forms.DateTimePicker Schedule_Start_Date;
        private System.Windows.Forms.Button Get_Schedule;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}