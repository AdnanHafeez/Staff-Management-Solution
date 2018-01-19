using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Data;
using System.IO;
using System.Text;

namespace LogIn
{

    class Communicator
    {
        public static int MESSAGE_SIZE = 1024;
        public int port
        {
            get; set;
        }
        public string hostname
        {
            get; set;
        }
        public TcpClient tcp;
        public NetworkStream stream;


        public Communicator()
        {
            //Set Default Port and Servername
            port = 1234;
            hostname = "127.0.0.1";
        }

        public Communicator(int Port, string Hostname)
        {
            port = Port;
            hostname = Hostname;
        }

        // Starts a communication connection with server on defined port and a hostname
        public void StartConnection()
        {
            try
            {
                
                tcp = new TcpClient(hostname, port);
                stream = tcp.GetStream();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string SendMessage(string message)
        {
            if(tcp == null)
            {
                throw new System.Exception("Exception: TCP Not initialized");
            }

            try
            {
                byte[] bytestring = Encoding.ASCII.GetBytes(message);
                stream.Write(bytestring, 0, bytestring.Length);

                byte[] buffer = new byte[MESSAGE_SIZE];
                int NumberOfBytes = 0;
                StringBuilder CompleteMessage = new StringBuilder();
                do {
                    NumberOfBytes = stream.Read(buffer, 0, MESSAGE_SIZE);
                    CompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(buffer, 0, NumberOfBytes));
                } while (stream.DataAvailable);
                stream.Flush();
                return CompleteMessage.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Disconnect()
        {
            tcp.Dispose();
            stream.Dispose();
            
        }      
    }
        static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1();
            Application.Run(form1);
        }
    }
}
