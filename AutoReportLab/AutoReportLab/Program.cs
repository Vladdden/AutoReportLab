using System;

namespace AutoReportLab
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Workers workers = new Workers();
            Console.WriteLine(workers.workerStatus);
        }
    }
}