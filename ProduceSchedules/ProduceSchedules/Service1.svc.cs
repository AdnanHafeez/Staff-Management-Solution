using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;

namespace ProduceSchedules
{
    public class DailyShifts
    {
        public Worker DayShift1;
        public Worker DayShift2;

        public Worker NightShift1;
        public Worker NightShift2;


        public DailyShifts()
        {
            DayShift1 = new Worker();
            DayShift2 = new Worker();
            NightShift1 = new Worker();
            NightShift2 = new Worker();
        }
        // default Constructor with available leave all the schedule open
        public DailyShifts(string DefaultString)
        {
            DayShift1 = new Worker(DefaultString);
            DayShift2 = new Worker(DefaultString);

            NightShift1 = new Worker(DefaultString);
            NightShift2 = new Worker(DefaultString);

        }

    }


    public class Worker
    {
        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int TotalShifts
        {
            get;
            set;
        }

        public string email
        {
            get;
            set;
        }

        public Worker()
        {
            Name = "";
            TotalShifts = 0;
        }

        public Worker(int id, string name,string e)
        {
            ID = id;
            Name = name;
            email = e;
            TotalShifts = 0;
        }

        public Worker(string name)
        {
            Name = name;
            ID = 0;
            TotalShifts = 0;
        }

    }
    public class GetFinalSchedule
    {
        enum Days { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };

        private int MAX_SHIFTS_ALLOWED = 4;
        private ArrayList DoctorsList = new ArrayList();

        private ArrayList GetListOfWorker(string Shift, int Day, DataTable Availabilities,string ScheduleFor)
        {
            try
            {
                int NumberOfDoctors = Availabilities.Rows.Count;
                string DayOfTheWeek = Enum.GetName(typeof(Days), Day);
                ArrayList al = new ArrayList();

                for (int i = 0; i < NumberOfDoctors; i++)
                {
                    string avail = Availabilities.Rows[i][DayOfTheWeek].ToString();
                    if (avail == Shift)
                    {
                        Worker w = new Worker(Convert.ToInt16(Availabilities.Rows[i]["ID"]), Availabilities.Rows[i][ScheduleFor].ToString(), Availabilities.Rows[i]["Email"].ToString());
                        al.Add(w);
                    }
                }
                return al;
            }
            catch (Exception ex)
            {
                throw (new Exception("Exception received in the method GetListOfWorker:(" + ex.ToString() + ")"));
            }

        }


        private bool WorkerNotWorkingBackToBackShifts(Worker w, ArrayList WeeklySchedule, string shift, int day)
        {
            try
            {

                // First day of the week 
                if (day == 0)
                {
                    DailyShifts ds = (DailyShifts)WeeklySchedule[0];
                    if (shift == "DayShift1")
                    {
                        return true;
                    }
                    else if (shift == "DayShift2")
                    {
                        if (ds.DayShift1.Name != w.Name)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (shift == "NightShift1")
                    {

                        if (ds.DayShift1.Name != w.Name && ds.DayShift2.Name != w.Name)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else if (shift == "NightShift2")
                    {
                        if (ds.DayShift1.Name != w.Name && ds.DayShift2.Name != w.Name && ds.NightShift1.Name != w.Name)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (day > 0)
                {
                    DailyShifts ds1 = (DailyShifts)WeeklySchedule[day - 1];
                    DailyShifts ds = (DailyShifts)WeeklySchedule[day];
                    if (shift == "DayShift1")
                    {

                        if (ds1.NightShift1.Name != w.Name && ds1.NightShift2.Name != w.Name)
                        {
                            return true;
                        }
                        else
                        {

                            return false;
                        }
                    }

                    else if (shift == "DayShift2")
                    {
                        if (ds1.NightShift1.Name != w.Name && ds1.NightShift2.Name != w.Name && ds.DayShift1.Name != w.Name)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (shift == "NightShift1")
                    {
                        if (ds.DayShift1.Name != w.Name && ds.DayShift2.Name != w.Name)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }

                    else if (shift == "NightShift2")
                    {
                        if (ds.DayShift1.Name != w.Name && ds.DayShift2.Name != w.Name && ds.NightShift1.Name != w.Name)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;

            }
            catch (Exception ex)
            {
                throw (new Exception("Error while checking back to back shifts" + ex.ToString()));
            }
        }

        private ArrayList GetTheWorkersThatSatisfyConstraint(ArrayList WorkersList, string ShiftToBeFilled, int day, ArrayList WeeklySchedule)
        {
            try
            {
                ArrayList temp = new ArrayList();
                foreach (Worker w in WorkersList)
                {
                    // find workers from the doctors list that keeps track of total shifts worked so far
                    foreach (Worker tempWorker in DoctorsList)
                    {
                        if (tempWorker.Name == w.Name && tempWorker.ID == w.ID)
                        {
                            if (tempWorker.TotalShifts < MAX_SHIFTS_ALLOWED)
                            {
                                if (WorkerNotWorkingBackToBackShifts(w, WeeklySchedule, ShiftToBeFilled, day))
                                {
                                    temp.Add(w);
                                }
                            }
                        }

                    }

                }

                if (temp.Count > 0)
                {
                    return temp;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw (new Exception("In getting the workers that satisfy contraint" + ex.ToString()));
            }

        }

        private Worker GetRandomWorker(ArrayList WorkerList)
        {
            Random r = new Random();
            int i = r.Next(0, WorkerList.Count);
            Worker w = WorkerList[i] as Worker;

            foreach (Worker doc in DoctorsList)
            {
                if (doc.Name == w.Name && doc.ID == w.ID)
                {
                    doc.TotalShifts++;
                }
            }

            return w;

        }


        private DataTable GetScheduleDataTable(ArrayList ds)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Monday", typeof(string));
            table.Columns.Add("Tuesday", typeof(string));
            table.Columns.Add("Wednesday", typeof(string));
            table.Columns.Add("Thursday", typeof(string));
            table.Columns.Add("Friday", typeof(string));
            table.Columns.Add("Saturday", typeof(string));
            table.Columns.Add("Sunday", typeof(string));

            string[] s = new string[7];
            int i = 0;
            foreach (DailyShifts daily in ds)
            {
                s[i] += "DayShift1: " + daily.DayShift1.Name;
                if(daily.DayShift1.Name != "Available")
                {
                    s[i] += "ID:" + daily.DayShift1.ID;
                    s[i] += "EMAIL:" + daily.DayShift1.email;
                }
                s[i] += "#";

                s[i] += "DayShift2: " + daily.DayShift2.Name;
                if (daily.DayShift2.Name != "Available")
                {
                    s[i] += "ID:" + daily.DayShift2.ID;
                    s[i] += "EMAIL:" +daily.DayShift2.email;
                }
                s[i] += "#";
                s[i] += "NightShift1: " + daily.NightShift1.Name;
                if (daily.NightShift1.Name != "Available")
                {
                    s[i] += "ID:" + daily.NightShift1.ID;
                    s[i] += "EMAIL:" + daily.NightShift1.email;
                }
                s[i] += "#";
                s[i] += "NightShift2: " + daily.NightShift2.Name;
                if (daily.NightShift2.Name != "Available")
                {
                    s[i] += "ID:"+daily.NightShift2.ID;
                    s[i] += "EMAIL:" + daily.NightShift2.email;
                }
                i++;
            }

            table.Rows.Add(s[0], s[1], s[2], s[3], s[4], s[5], s[6]);

            return table;


        }

        public string ProduceSchedule(DataTable Availabilities, string WeekEnding, string ScheduleFor)
        {
            try
            {


                ArrayList Schedules = new ArrayList();

                string DayShift = "DayShift";
                string NightShift = "NightShift";

                int NumberOfDoctors = Availabilities.Rows.Count;

                if (ScheduleFor == "Doctor")
                {
                    // Create list for each worker. That will help in keeping track of the total shifts for each worker
                    for (int i = 0; i < NumberOfDoctors; i++)
                    {
                        string name = Availabilities.Rows[i]["Doctor"].ToString();
                        int id = Convert.ToInt16(Availabilities.Rows[i]["ID"]);
                        string email = Availabilities.Rows[i]["Email"].ToString();
                        DoctorsList.Add(new Worker(id, name,email));
                    }
                }
                if (ScheduleFor == "Nurse")
                {
                    // Create list for each worker. That will help in keeping track of the total shifts for each worker
                    for (int i = 0; i < NumberOfDoctors; i++)
                    {
                        string name = Availabilities.Rows[i]["Nurse"].ToString();
                        int id = Convert.ToInt16(Availabilities.Rows[i]["ID"]);
                        string email = Availabilities.Rows[i]["Email"].ToString();
                        DoctorsList.Add(new Worker(id, name,email));
                    }
                }
                // Initialize daily schedule for a week. Schedule array days are represented by the 
                // index of schedules array
                for (int i = 0; i < 7; i++)
                {
                    Schedules.Add(new DailyShifts("Available"));
                }

                int day = 0;

                // Schedules is getting modified in the foreach loop. Need to fix that
                // Need to increase number of shifts for a worker once added to the schedule


                //foreach(DailyShifts ds in Schedules)
                while (day < 7)
                {

                    ArrayList WorkersThatCanWorkDayShift = GetListOfWorker(DayShift, day, Availabilities,ScheduleFor);
                    ArrayList WorkersThatCanWorkNightShift = GetListOfWorker(NightShift, day, Availabilities,ScheduleFor);

                    if (WorkersThatCanWorkDayShift != null)
                    {
                        // Fill Shifts 1
                        ArrayList temp = GetTheWorkersThatSatisfyConstraint(WorkersThatCanWorkDayShift, "DayShift1", day, Schedules);
                        if (temp != null)
                        {
                            DailyShifts d = Schedules[day] as DailyShifts;
                            d.DayShift1 = GetRandomWorker(temp);
                            Schedules.Insert(day, d);
                            Schedules.RemoveAt(day + 1);

                        }
                        temp = GetTheWorkersThatSatisfyConstraint(WorkersThatCanWorkDayShift, "DayShift2", day, Schedules);
                        if (temp != null)
                        {
                            DailyShifts d = Schedules[day] as DailyShifts;
                            d.DayShift2 = GetRandomWorker(temp);
                            Schedules.Insert(day, d);
                            Schedules.RemoveAt(day + 1);
                        }
                    }

                    if (WorkersThatCanWorkNightShift != null)
                    {
                        // Fill Shifts 1
                        ArrayList temp = GetTheWorkersThatSatisfyConstraint(WorkersThatCanWorkNightShift, "NightShift1", day, Schedules);
                        if (temp != null)
                        {
                            DailyShifts d = Schedules[day] as DailyShifts;
                            d.NightShift1 = GetRandomWorker(temp);
                            Schedules.Insert(day, d);
                            Schedules.RemoveAt(day + 1);
                        }
                        temp = GetTheWorkersThatSatisfyConstraint(WorkersThatCanWorkNightShift, "NightShift2", day, Schedules);
                        if (temp != null)
                        {
                            DailyShifts d = Schedules[day] as DailyShifts;
                            d.NightShift2 = GetRandomWorker(temp);
                            Schedules.Insert(day, d);
                            Schedules.RemoveAt(day + 1);
                        }
                    }

                    day++;
                }

                DataTable dt = GetScheduleDataTable(Schedules);
                dt.TableName = "Doctors_Schedule";

                MemoryStream ms = new MemoryStream();
                dt.WriteXml(ms);
                ms.Flush();
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        public DataTable GetDataTableFromString(string s)
        {
            MemoryStream ms = new MemoryStream();
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(s);
            ms.Write(buffer, 0, s.Length);

            ms.Flush();
            ms.Position = 0;

            // Get the location
            DataSet ds = new DataSet();
            ds.DataSetName = "FinalSchedules";
            ds.ReadXml(ms);
            return ds.Tables[0];
        }


    }


    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
       


        public string GetFinalSchedule(string Availabilities, string WeekEnding, string ScheduleFor)
        {

            GetFinalSchedule schedule = new GetFinalSchedule();
            DataTable Availabilities_dt = schedule.GetDataTableFromString(Availabilities);
            string temp = schedule.ProduceSchedule(Availabilities_dt, WeekEnding, ScheduleFor);
            return temp;
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
