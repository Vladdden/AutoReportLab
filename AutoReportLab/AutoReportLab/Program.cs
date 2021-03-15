using System;

namespace AutoReportLab
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Workers workers = new Workers();
            foreach (var worker in workers.workerList)
            {
                Console.WriteLine(worker.PrintWorker());
            }
        }
    }
}