using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LogIn
{
    public partial class ScheduleDetails : Form
    {

        public bool Form_Closed = false;
        public string Schedule_details
        {
            get;
            set;
        }
        public ScheduleDetails()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Form_Closed = true;
            this.Close();
        }

        private void ScheduleDetails_Load(object sender, EventArgs e)
        {

        }

        // Check if all the values have been selected. If yes, then enable the get schedule button
        private void Schedule_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Schedule_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void Schedule_Start_Date_ValueChanged(object sender, EventArgs e)
        {
            if (Schedule_Type.Text != "" && Schedule_Location.Text != "" && Schedule_Start_Date.Checked == true)
            {
                Get_Schedule.Enabled = true;

            }

            else
            {
                Get_Schedule.Enabled = false;
            }
        }

        private void Get_Schedule_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = GetDataSetForScheduleInfo();

                Schedule_details = CreateXMLFromDataTable(dt);
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Starting Communicator Error:" + ex.ToString());
            }
        }

        private DataTable GetDataSetForScheduleInfo()
        {
            try
            {
                DataTable dt = new DataTable();
               
                dt.TableName = "Schedule_Info";
                // Create New Columns for the datatable
                dt.Columns.Add(new DataColumn("Schedule_Type", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Location", Type.GetType("System.String")));
                dt.Columns.Add(new DataColumn("Week_Ending", Type.GetType("System.String")));
                              
                
                // Add required values into the table
                dt.Rows.Add(Schedule_Type.Text, Schedule_Location.Text, Schedule_Start_Date.Text);
                return dt;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while creating dataset:" + ex.ToString());
                throw ex;
            }
        }

        string CreateXMLFromDataTable(DataTable dt)
        {
            try
            {
                MemoryStream memory = new MemoryStream();

                dt.WriteXml(memory);
                memory.Flush();
                memory.Position = 0;
                StreamReader sr = new StreamReader(memory);
                string mystr = sr.ReadToEnd();
                return mystr;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
