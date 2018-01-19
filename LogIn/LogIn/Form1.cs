using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace LogIn
{
    public partial class Form1 : Form
    {
        private string WORKINGPATH = "Assignment 2";

        public Form1()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // This method gets the current directory path. Attaches the directory path at the end for the databases to work
        private string GetDatabasePath()
        {
            string currentpath = Directory.GetCurrentDirectory();
            int end_index = currentpath.LastIndexOf("Assignment 2");
            if(end_index < 0)
            {
                MessageBox.Show("Error finding database location");
                return "";
            }
            else
            {
                end_index += WORKINGPATH.Length;
                string path = currentpath.Substring(0, end_index) + @"\Databases\";
                return path;
            }
        }

        private void LogIn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            // Check for LogIn information
            try
            {
                string DatabasePath = GetDatabasePath();
                if(DatabasePath == "")
                {
                    throw (new Exception("Error finding Database path"));
                }
                //SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=LogInData;Integrated Security=True;");
                SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Integrated Security=True;AttachDbFileName="+DatabasePath+"LogInData.mdf");
                string command = "SELECT Count(*) from LOGIN where USERNAME ='" + UserName.Text + "'and PASSWORD='" + Password.Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(command, conn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {

                    MainWindow mw = new MainWindow();
                    this.Hide();
                    this.Cursor = Cursors.Default;
                    mw.Show();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Log In Failed!");
                }
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Arrow;
                MessageBox.Show("Exception while checking log-in details:{0}", ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
