using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Data;
using System.IO;

namespace ServiceController
{

    public class Controller
    {
        private int TIME_OUT_THRESHOLD = 5;
        private string WORKINGPATH = "Assignment 2";
        private int Port;
        private string Hostname;
        private TcpListener listener;
        private IPEndPoint ip;
        private int ping_count = 0;
        private byte[] buffer = new byte[1024];
        private string message; // hold the incoming message
        private string schedule_info; // hold the information about schedules that need to created
        private string final_schedule;
        private string Schedule_Type, Schedule_Location, Start_date, End_Date;
        private bool isChildThreadFailure = false;
        private bool ChildThreadSuccess = false;
        private string ChildThreadFailureReason;
        private System.Threading.Thread InvokeServices;



        public Controller()
        {
            // Default port and hostname for server
            Port = 1234;
            Hostname = "127.0.0.1";
        }

        public Controller(int port, string hostname)
        {
            Port = port;
            Hostname = hostname;
        }

        public void ClearAllBuffers()
        {
            isChildThreadFailure = false;
            ChildThreadSuccess = false;
            final_schedule = "";
            schedule_info = "";
            message = "";
            ChildThreadFailureReason = "";
            ping_count = 0;
            
        }

        public void StartListening()
        {
            try
            {
                Console.WriteLine("Listening for Connection on Port:" + Convert.ToString(Port) + " Host:" + Hostname);
                ip = new IPEndPoint(IPAddress.Loopback, Port);
                listener = new TcpListener(ip);
                listener.Start();
               
                
                while (true)
                {
                    // Listen on the connection for incoming message
                    TcpClient connection = listener.AcceptTcpClient();

                    // Get a stream object for the network stream
                    NetworkStream stream = connection.GetStream();

                    int i = stream.Read(buffer, 0, buffer.Length);
                    
                    message = System.Text.Encoding.ASCII.GetString(buffer, 0, i);

                   // Check to see if the first message is received from client
                    if (message.Contains("Doctor") || message.Contains("Nurse"))
                    {
                        // Reset ping counter for a new connection
                        ping_count = 0;
                        //Send Confirmation message
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes("Schedule Details Successfully Received");
                        stream.Write(msg, 0, msg.Length);

                        // Start a child process to perform all the work to invoke all the appropriate services and gather data 
                        // about the schedules
                        InvokeServices = new System.Threading.Thread(StartCallingServices);
                        schedule_info = message;
                        InvokeServices.Start();
                        continue;

                    }


                    // Check to see if ping is received from the client. Keep telling client that controller is still working on the 
                    // services. Client will produce an error if it takes too long. 
                    if(message.Contains("ping"))
                    {
                        Console.WriteLine("Client pinging for response");
                        string temp = "";
                        
                        ping_count++;
                        if (ping_count <= TIME_OUT_THRESHOLD)
                        {

                            // Check what the status of child thread is
                            if (isChildThreadFailure)
                            {
                                // Service thread failed for some reasons
                                temp = ChildThreadFailureReason;
                                Console.WriteLine("Sending Failure Reason to client");
                                byte[] reply = System.Text.Encoding.ASCII.GetBytes(temp);
                                stream.Write(reply, 0, reply.Length);
                                ClearAllBuffers();
                                continue;
                            }

                            else if (ChildThreadSuccess)
                            {
                                // Send back the success message
                                temp = final_schedule + " Success";
                                Console.WriteLine("Sending success message to client");
                                byte[] reply = System.Text.Encoding.ASCII.GetBytes(temp);
                                stream.Write(reply, 0, reply.Length);
                                ClearAllBuffers();
                                continue;
                            }

                            else
                            {
                                // Send ping success message
                                temp = "Ping Received";
                                Console.WriteLine("Sending ping confirmation");
                                byte[] reply = System.Text.Encoding.ASCII.GetBytes(temp);
                                stream.Write(reply, 0, reply.Length);
                                continue;
                            }


                        }
                        else
                        {
                            // At this point the client should have disconnected and would be preparing for a new connection.
                            // So we need to reset the ping out counter
                            temp = "Operation Timeout";
                            Console.WriteLine("Sending Timeout command to client");
                            ClearAllBuffers();
                            ping_count = 0;

                        }                       

                    }

                  //  connection.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // This method gets the current directory path. Attaches the directory path at the end for the databases to work
        private string GetDatabasePath()
        {
            try
            {
                string currentpath = Directory.GetCurrentDirectory();
                int end_index = currentpath.LastIndexOf("Assignment 2");
                if (end_index < 0)
                {
                    throw new Exception("Error finding database location");
                }
                else
                {
                    end_index += WORKINGPATH.Length;
                    string path = currentpath.Substring(0, end_index) + @"\Databases\";
                    return path;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetDataBasePath: " + ex.ToString());
            }
        }
        private void StartCallingServices()
        {
            try
            {
                Console.WriteLine("In Child Process");
                ParseXML(schedule_info);

                if (Schedule_Type == "Doctor")
                {
                    // Testing the doctor schedule service.
                    ServiceController.DoctorScheduleService.Service1Client ds = new ServiceController.DoctorScheduleService.Service1Client();
                    ServiceController.ProduceSchedules.Service1Client ps = new ServiceController.ProduceSchedules.Service1Client();
                    string path = GetDatabasePath();
                    string avail = ds.DoctorSchedule(path + "DoctorSchedule.mdf");

                    final_schedule = ps.GetFinalSchedule(avail, Start_date,"Doctor");
                    ChildThreadSuccess = true;
                    return;
                }

                if(Schedule_Type == "Nurse")
                {
                    // Call Nurse Schedule Service to get the data

                    ServiceController.NurseScheduleService.Service1Client ns = new ServiceController.NurseScheduleService.Service1Client();
                    string path = GetDatabasePath();
                    string availabilites = ns.NurseSchedule(path + "DoctorSchedule.mdf");
                    ServiceController.ProduceSchedules.Service1Client ps = new ServiceController.ProduceSchedules.Service1Client();
                    final_schedule = ps.GetFinalSchedule(availabilites, Start_date,"Nurse");
                    ChildThreadSuccess = true;
                    return;

                }
               
                // Call patient Service to get the information about patient traffic

                // Combine all the data and generate schedules


                
            }
            catch (Exception ex)
            {
                ChildThreadFailureReason = "Exception thrown in child process: " + Convert.ToString(ex);
                Console.WriteLine("Exception thrown in child process: " + Convert.ToString(ex));
                isChildThreadFailure = true;
            }
        }



        // This method parses the XML file received from client and then calls the appropriate services to retrieve
        // the required information
        public void ParseXML(string scheduleinfo)
        {
            try {

                MemoryStream ms = new MemoryStream();
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(scheduleinfo);
                ms.Write(buffer, 0, scheduleinfo.Length);
                
                ms.Flush();
                ms.Position = 0;
                
                // Get the location
                DataSet ds = new DataSet();
                
                ds.ReadXml(ms);

                // Get the date range
                // Get the type
                Schedule_Type = ds.Tables[0].Rows[0]["Schedule_Type"].ToString();
                Schedule_Location = ds.Tables[0].Rows[0]["Location"].ToString();
                Start_date = ds.Tables[0].Rows[0]["Week_Ending"].ToString();

                Console.WriteLine("Schedule Type = "+ ds.Tables[0].Rows[0]["Schedule_Type"].ToString());
                Console.WriteLine("Schedule Location = " + ds.Tables[0].Rows[0]["Location"].ToString());
                Console.WriteLine("Week Ending = " + ds.Tables[0].Rows[0]["Week_Ending"].ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Controller server = new Controller();
                server.StartListening();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error Received:{0}", ex.ToString());
                Console.WriteLine("Program Exiting");
                Console.ReadKey();
            }

        }
    }
}
