using System;

namespace AutoReportLab
{
    internal class Program
    {
        private static Workers workers;
        public Program()
        {
            workers = new Workers();
        }
        public static void Main(string[] args)
        {
            
            bool isExit = false;
            switch (workers.workerStatus)
            {
                case 100:
                    while (!isExit)
                        isExit = UserIsTeamLeader();
                    break;
                case 10:
                    while (!isExit)
                        isExit = UserIsBoss();
                    break;
                case 1:
                    while (!isExit)
                        isExit = UserIsBoss();
                    break;
            }
        }

        public static bool UserIsTeamLeader()
        {
            
        }
        
        public static bool UserIsBoss()
        {
            
        }
        
        public static bool UserIsWorker()
        {
            
        }
    }
}