using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace NurseScheduleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string NurseSchedule()
        {
            try
            {
                SqlConnection NurseScheduleConnection = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=DoctorSchedule;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
                string command = @"SELECT * FROM NurseAvailability";
                SqlDataAdapter adp = new SqlDataAdapter(command, NurseScheduleConnection);
                DataTable dt = new DataTable();
                dt.TableName = "NurseAvailability";
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
