using System;

namespace TestWithTime
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            DateTime date1 = DateTime.Now;
            
            /*while (true)
            {
                if (Console.ReadLine() == "stop")
                {
                    break;
                }
            }*/
            
            System.Threading.Thread.Sleep(1000);
            
            DateTime date2 = DateTime.Now;

            Console.WriteLine("{0} seconds", date1.ToString("ss.fff"));
            Console.WriteLine("{0} seconds", date2.ToString("ss.fff"));
            Console.WriteLine("__________________________________________________");
            
            TimeSpan interval = date2-date1;
            Console.WriteLine("Interval : {0}", interval.Seconds*1000 + interval.Milliseconds);
        }
    }
}