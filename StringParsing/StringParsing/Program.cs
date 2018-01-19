using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringParsing
{
    class Program
    {
        static void Main(string[] args)
        {

            string s = "DayShift1:IssacNewtonID:2email:afsdfsd@asdfsdafsad.com#DayShift2:Available#Nightshift1:Adnan";
            string[] n = s.Split('#');

            foreach(string t in n)
            {
                Console.WriteLine(t);
            }

            Console.ReadKey();

        }
    }
}
