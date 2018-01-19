using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace LogIn
{
    public partial class MainWindow : Form
    {
        public string ScheduleString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GoBack_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Form1 form1 = new Form1();
            form1.Show();
            this.Cursor = Cursors.Default;
            this.Close();
        }

       
        private void createScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Create_Schedule_Click(object sender, EventArgs e)
        {
            // Get information about what the schedule needs to be created for
            ScheduleDetails sd = new ScheduleDetails();
            sd.ShowDialog();
            if(sd.Form_Closed)
            {
                // Don't do anything if the form is closed
                return;
            }
            ScheduleString = sd.Schedule_details;
            // Just for debugging purposes
            StatusLog.Text = ScheduleString+"\n";

            sd.Close();


            try
            {

                this.Cursor = Cursors.WaitCursor;
                Communicator cs = new Communicator();
                cs.StartConnection();

                // Start sending message
                string rec = cs.SendMessage(ScheduleString);

                if (rec.Contains("Details"))
                {
                    StatusLog.Text += "Details Sent Successfully";
                }


                while (true)
                {
                    Thread.Sleep(1000);
                    cs = new Communicator();
                    cs.StartConnection();
                    rec = cs.SendMessage("ping");
                    StatusLog.Text += "Ping Sent\n";
                    if (rec.Contains("Timeout"))
                    {
                        StatusLog.Text += "Timeout request received\n";
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show("Server Timed out. Disconnecting", "NO RESPONSE");
                        break;
                    }
                    if (rec.Contains("Success"))
                    {
                        StatusLog.Text += rec;
                        this.Cursor = Cursors.Arrow;
                        ParseAndShowSchedules(rec);
                        break;
                    }
                    if (rec.Contains("Failure"))
                    {
                        StatusLog.Text += rec;
                        this.Cursor = Cursors.Arrow;
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Arrow;

                if (ex.GetType().IsAssignableFrom(typeof(System.Net.Sockets.SocketException)))
                {
                    
                    MessageBox.Show("Error connecting server. Please make sure server is running","CONNECTION ERROR");
                }
                else
                {

                    StatusLog.Text += ex.ToString();
                }
            }
                     
        }

        private DataTable DataTableFormatted(DataTable dt)
        {
            try {
                DataTable temp = new DataTable();

                temp.TableName = "Formatted_Scheudles";
                temp.Columns.Add(new DataColumn("Monday", Type.GetType("System.String")));
                temp.Columns.Add(new DataColumn("Tuesday", Type.GetType("System.String")));
                temp.Columns.Add(new DataColumn("Wednesday", Type.GetType("System.String")));
                temp.Columns.Add(new DataColumn("Thursday", Type.GetType("System.String")));
                temp.Columns.Add(new DataColumn("Friday", Type.GetType("System.String")));
                temp.Columns.Add(new DataColumn("Saturday", Type.GetType("System.String")));
                temp.Columns.Add(new DataColumn("Sunday", Type.GetType("System.String")));

                string[] Monday = new string[4];
                string[] Tuesday = new string[4];
                string[] Wednesday = new string[4];
                string[] Thursday = new string[4];
                string[] Friday = new string[4];
                string[] Saturday = new string[4];
                string[] Sunday = new string[4];

                string MondayString = dt.Rows[0]["Monday"].ToString();
                string TuesdayString = dt.Rows[0]["Tuesday"].ToString();
                string WednesdayString = dt.Rows[0]["Wednesday"].ToString();
                string ThursdayString = dt.Rows[0]["Thursday"].ToString();
                string FridayString = dt.Rows[0]["Friday"].ToString();
                string SaturdayString = dt.Rows[0]["Saturday"].ToString();
                string SundayString = dt.Rows[0]["Sunday"].ToString();

                string[] s = MondayString.Split('#');

                if(s.Length == 4)
                {
                    int i = 0;
                  foreach(string t in s)
                    {
                        Monday[i] = t;
                        i++;
                    }
                }
                else
                {
                    throw (new Exception("Monday string not split properly"));
                }

                s = TuesdayString.Split('#');

                if (s.Length == 4)
                {
                    int i = 0;
                    foreach (string t in s)
                    {
                        Tuesday[i] = t;
                        i++;
                    }
                }
                else
                {
                    throw (new Exception("Tuesday string not split properly"));
                }

                s = WednesdayString.Split('#');

                if (s.Length == 4)
                {
                    int i = 0;
                    foreach (string t in s)
                    {
                        Wednesday[i] = t;
                        i++;
                    }
                }
                else
                {
                    throw (new Exception("Wednesday string not split properly"));
                }

                s = ThursdayString.Split('#');

                if (s.Length == 4)
                {
                    int i = 0;
                    foreach (string t in s)
                    {
                        Thursday[i] = t;
                        i++;
                    }
                }
                else
                {
                    throw (new Exception("Thursday string not split properly"));
                }

                s = FridayString.Split('#');

                if (s.Length == 4)
                {
                    int i = 0;
                    foreach (string t in s)
                    {
                        Friday[i] = t;
                        i++;
                    }
                }
                else
                {
                    throw (new Exception("Friday string not split properly"));
                }
                s = SaturdayString.Split('#');

                if (s.Length == 4)
                {
                    int i = 0;
                    foreach (string t in s)
                    {
                        Saturday[i] = t;
                        i++;
                    }
                }
                else
                {
                    throw (new Exception("Saturday string not split properly"));
                }

                s = SundayString.Split('#');

                if (s.Length == 4)
                {
                    int i = 0;
                    foreach (string t in s)
                    {
                        Sunday[i] = t;
                        i++;
                    }
                }
                else
                {
                    throw (new Exception("Sunday string not split properly"));
                }
                

                for(int i = 0; i < 4; i++)
                {
                    temp.Rows.Add(Monday[i], Tuesday[i], Wednesday[i], Thursday[i], Friday[i], Saturday[i], Sunday[i]);
                }

                return temp;

            }

           
            catch (Exception ex)
            {
                throw (new Exception("In Data Table Formatted:" + ex.ToString()));
            }

        }
        
   


        // Show the finaly produced schedules on the datagrid
        private void ParseAndShowSchedules(string schedules)
        {
            try
            {
                // Remove success string at the end
                int ind = schedules.IndexOf("Success");
                schedules = schedules.Remove(ind);
                MemoryStream ms = new MemoryStream();
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(schedules);
                ms.Write(buffer, 0, schedules.Length);

                ms.Flush();
                ms.Position = 0;

                // Get the location
                DataSet ds = new DataSet();
                ds.DataSetName = "FinalSchedules";
                ds.ReadXml(ms);

                DataTable temp = DataTableFormatted(ds.Tables[0]);
                ShowScheduleGrid.DataSource = temp;
                //ShowScheduleGrid.AutoGenerateColumns = true;

            }
            catch(Exception ex)
            {
                StatusLog.Text += "Error while displaying the schedules";
                StatusLog.Text += ex.ToString();
            }
        }

        private void SendAndReceviedData(RichTextBox statuslog)
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;
                Communicator cs = new Communicator();
                cs.StartConnection();

                // Start sending message
                string rec = cs.SendMessage(ScheduleString);

                // Start listening for response
                while (true)
                {
                    Thread.Sleep(1000);
                    cs = new Communicator();
                    cs.StartConnection();
                    rec = cs.SendMessage("ping");
                    StatusLog.Text += "Ping Sent\n";
                    if (rec.Contains("Timeout"))
                    {
                        StatusLog.Text += "Timeout request received\n";
                        break;
                    }

                    // Schedules created successfully
                    else if(rec.Contains("SUCCESS"))
                    {
                        ParseAndShowSchedules(rec);
                        break;
                    }


                }                
            }
            catch (Exception ex)
            {
               // this.Cursor = Cursors.Arrow;
                statuslog.Text += ex.ToString();
            }
        }

        private void StartSendingAndRecevieving_DoWork(object sender, DoWorkEventArgs e)
        {
            SendAndReceviedData(StatusLog);

        }
    }
}
