using System;
using System.IO;

namespace AutoReportLab
{
    public class Workers
    {
        public Workers()
        {
            string pathToWorkersDirectory = $"{Directory.GetCurrentDirectory()}/Workers";
            Console.WriteLine(pathToWorkersDirectory);
            if (!Directory.Exists(pathToWorkersDirectory))
            {
                Directory.CreateDirectory(pathToWorkersDirectory);
                File.Create($"{pathToWorkersDirectory}/users.txt").Dispose();
                using (StreamWriter infoWriter = new StreamWriter($"{pathToWorkersDirectory}/users.txt"))
                {
                    Worker teamLeader = new Worker(0, 0, "Admin", "1234");
                    infoWriter.WriteLine(teamLeader.PrintWorker());
                    infoWriter.Close();
                }
            }
            else
            {
                
            }
        }
    }

    public class Worker
    {
        private int id;
        private int status;
        private string name;
        private string password;
        private int leaderID;
        private string employees;

        public Worker(int ID, int Status, string Name, string Password, int LeaderId, string Employees)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = LeaderId;
            employees = Employees;
        }
        public Worker(int ID, int Status, string Name, string Password, int LeaderId)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = LeaderId;
            employees = "no";
        }
        public Worker(int ID, int Status, string Name, string Password, string Employees)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = -1;
            employees = Employees;
        }
        public Worker(int ID, int Status, string Name, string Password)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = -1;
            employees = "no";
        }

        public string PrintWorker()
        {
            return $"{id};{status};{name};{password};{leaderID};{employees};";
        }
    }
}