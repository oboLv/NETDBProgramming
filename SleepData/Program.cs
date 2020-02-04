using System;
using System.IO;

namespace SleepData
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            // specify path for data file
            string file = AppDomain.CurrentDomain.BaseDirectory + "data.txt";
            
            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter(file);
                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();
            }
            else if (resp == "2")
            {
                StreamReader sR = new StreamReader(file);
                while(!sR.EndOfStream)
                {
                    string roughRead = sR.ReadLine();
                    string[] tableEle = new string[] { "Su", "Mo", "Tu", "We", "Th", "Fr", "Sa", "Tot", "Avg" };
                    string[] roughArray = roughRead.Split(',');
                    string[] splitArray = roughArray[1].Split('|');
                    Console.WriteLine("Week of {0:d}", roughArray[0]);
                    string tableFormat = "{0,3}\t{1,3}\t{2,3}\t{3,3}\t{4,3}\t{5,3}\t{6,3}\t{7,3}\t{8,3}";
                    Console.WriteLine(tableFormat, tableEle[0], tableEle[1], tableEle[2], tableEle[3], tableEle[4], tableEle[5], tableEle[6], tableEle[7], tableEle[8]);
                    Console.WriteLine(tableFormat, "--", "--", "--", "--", "--", "--", "--", "--", "--");
                    int[] intArray = new int[7];
                    for (int i = 0; i < 7; i++)
                    {
                        intArray[i] = Convert.ToInt32(splitArray[i]);
                    }
                    int total = 0;
                    for(int i = 0; i < 7; i++)
                    {
                        total += intArray[i];
                    }
                    double avg = (double)total / 7;
                    double cleanAvg = Math.Round(avg, 2);
                    Console.WriteLine(tableFormat, intArray[0], intArray[1], intArray[2], intArray[3], intArray[4], intArray[5], intArray[6], total, cleanAvg);
                }
                Console.ReadKey();
                
            }
        }
    }
}
