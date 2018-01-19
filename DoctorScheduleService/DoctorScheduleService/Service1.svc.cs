using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace DoctorScheduleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string DoctorSchedule()
        {
            try
            {
                SqlConnection DoctorScheduleConnection = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=DoctorSchedule;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
                string command = @"SELECT * FROM DoctorAvailability";
                SqlDataAdapter adp = new SqlDataAdapter(command, DoctorScheduleConnection);
                DataTable dt = new DataTable();
                dt.TableName = "DoctorAvailability";
                adp.Fill(dt);
                MemoryStream ms = new MemoryStream();
                dt.WriteXml(ms);
                ms.Flush();
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();


            }
            catch (Exception ex)
            {
                return "Error: {" + ex.ToString() + "}";
            }
        }
    

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
