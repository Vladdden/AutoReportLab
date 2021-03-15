using System;
using System.Collections.Generic;
using System.IO;

namespace AutoReportLab
{
    public class Workers
    {
        public List<Worker> workerList = new List<Worker>();
        
        private string pathToWorkersDirectory = $"{Directory.GetCurrentDirectory()}/Workers";
        
        public Workers()
        {
            Console.WriteLine(pathToWorkersDirectory);
            if (!Directory.Exists(pathToWorkersDirectory))
            {
                Directory.CreateDirectory(pathToWorkersDirectory);
                CreateFileUsers();
            }
            else
            {
                if (File.Exists($"{pathToWorkersDirectory}/users.txt"))
                    ReadUsersFromFile();
                else
                    CreateFileUsers();

            }
        }

        private void CreateFileUsers()
        {
            File.Create($"{pathToWorkersDirectory}/users.txt").Dispose();
            using (StreamWriter adminWriter = new StreamWriter($"{pathToWorkersDirectory}/users.txt"))
            {
                Worker teamLeader = new Worker(0, 0, "Admin", "1234");
                adminWriter.WriteLine(teamLeader.PrintWorker());
                adminWriter.Close();
            }
        }

        private void ReadUsersFromFile()
        {
            using (StreamReader usersReader = new StreamReader($"{pathToWorkersDirectory}/users.txt"))
            {
                string info;
                while (!usersReader.EndOfStream)
                {
                    info = usersReader.ReadLine();
                    string[] values = info.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 6)
                    {
                        Worker worker = new Worker( 
                            Convert.ToInt32(values[0]), 
                            Convert.ToInt32(values[1]), 
                            values[2], 
                            values[3], 
                            Convert.ToInt32(values[4]), 
                            values[5]);
                        workerList.Add(worker);
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат записи.");
                        break;
                    }
                }
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