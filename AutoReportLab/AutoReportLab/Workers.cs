using System;
using System.Collections.Generic;
using System.IO;

namespace AutoReportLab
{
    public class Workers
    {
        internal readonly int workerStatus;
        private List<Worker> workerList = new List<Worker>();
        private string pathToWorkersDirectory = $"{Directory.GetCurrentDirectory()}/Workers";
        
        
        public Workers()
        {
            if (!Directory.Exists(pathToWorkersDirectory))
            {
                Directory.CreateDirectory(pathToWorkersDirectory);
                CreateFileUsers();
            }
            else
            {
                if (File.Exists($"{pathToWorkersDirectory}/users.txt"))
                {
                    ReadUsersFromFile();
                    workerStatus = Login();
                }
                else
                    CreateFileUsers();

            }
        }

        private int Login()
        {
            bool loginStatus = false;
            int status = 0;
            string login;
            do
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в систему!");
                Console.Write("Введите логин: ");
                login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();
                foreach (var worker in workerList)
                {
                    if (worker.LoginWorker(login, password) != 0)
                    {
                        loginStatus = true;
                        status = worker.LoginWorker(login, password);
                    }
                }

                if (!loginStatus)
                {
                    Console.WriteLine("Введены неверные имя пользователя или пароль.\nДля продолжения нажмите Enter...");
                    Console.ReadKey();
                }
            } while (!loginStatus);
            Console.WriteLine($"Добро пожаловать, {login}");
            return status;
        }
        
        private void CreateFileUsers()
        {
            File.Create($"{pathToWorkersDirectory}/users.txt").Dispose();
            using (StreamWriter adminWriter = new StreamWriter($"{pathToWorkersDirectory}/users.txt"))
            {
                Worker teamLeader = new Worker(0, 100, "Admin", "1234");
                adminWriter.WriteLine(teamLeader.PrintWorker());
                adminWriter.Close();
            }
            Console.WriteLine("Файл с пользователем создан. Перезапустите приложение.");
        }

        private void ReadUsersFromFile()
        {
            // TODO очищать список перед заполнением 
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

    internal class Worker
    {
        protected int id;
        protected int status;
        protected string name;
        protected string password;
        protected int leaderID;
        protected string employees;

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

        public int LoginWorker(string Login, string Password)
        {
            if (Login == name && Password == password) return status;
            else return 0;
        }
        
        public string PrintWorker()
        {
            return $"{id};{status};{name};{password};{leaderID};{employees};";
        }
    }
}