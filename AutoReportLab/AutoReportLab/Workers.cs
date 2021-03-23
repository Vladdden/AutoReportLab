using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoReportLab
{
    public class Workers
    {
        internal readonly int workerStatus;
        internal readonly int workerID;
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
                    string str = Login();
                    string[] values = str.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                    workerID = Convert.ToInt32(values[0]);
                    workerStatus = Convert.ToInt32(values[1]);
                }
                else
                    CreateFileUsers();
            }
        }

        private string Login()
        {
            bool loginStatus = false;
            string status, ret = "";
            string login;
            string[] values;
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
                    if (worker.LoginWorker(login, password) != "0")
                    {
                        loginStatus = true;
                        ret = worker.LoginWorker(login, password);
                    }
                }

                if (!loginStatus)
                {
                    Console.WriteLine("Введены неверные имя пользователя или пароль.\nДля продолжения нажмите Enter...");
                    Console.ReadKey();
                }
            } while (!loginStatus);
            Console.WriteLine($"Добро пожаловать, {login}");
            return ret;
        }
        
        private void CreateFileUsers()
        {
            string path = $"{pathToWorkersDirectory}/users.txt";
            using (FileStream streamWriter = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter adminWriter = new StreamWriter(streamWriter))
                {
                    Worker teamLeader = new Worker(1, 100, "Admin", "1234");
                    adminWriter.WriteLine($"{teamLeader.GetID()};{teamLeader.GetStatus()};{teamLeader.GetName()};{teamLeader.GetPassword()};{teamLeader.GetLeaderID()};{teamLeader.GetEmployee()}");
                    adminWriter.Close();
                }
            }

            Console.WriteLine("Файл с пользователем создан. Для продолжения нажмите Enter.");
            Console.ReadKey();
            Program.Main(new string[] { });
        }

        public int AddWorker(int Status, string Name, string Password, int LeaderId = 1, string Employees = "no")
        {
            ReadUsersFromFile();
            int ID = workerList.Count + 1;
            Worker worker = new Worker(ID, Status, Name, Password, LeaderId, Employees);
            workerList.Add(worker);        
            UpdateWorkersFile();
            ReadUsersFromFile();
            if (workerList.Count == ID && workerList[workerList.Count - 1].GetName() == Name)
            {
                Console.WriteLine("Пользователь успешно добавлен.");
                return ID;
            }
            else
            {
                Console.WriteLine("Ошибка при создании пользователя.");
                return -1;
            }
        }

        public int LeaderCheck()
        {
            int trueID = -1;
            Console.WriteLine(" ________________________ ");
            Console.WriteLine("| ID |        Name       |");
            foreach (var worker in workerList)
            {
                if (worker.GetStatus() >= 10)
                {
                    Console.WriteLine("|----|-------------------|");
                    Console.WriteLine("| {0,-3}| {1,-18}|", worker.GetID(), worker.GetName());
                }
            }
            Console.WriteLine("|____|___________________|");
            
            bool check = false;
            do
            {
                Console.Write("Введите ID сотрудника-руководителя: ");
                int leaderID = Convert.ToInt32(Console.ReadLine());
                foreach (var worker in workerList)
                {
                    if (worker.GetStatus() >= 10 && leaderID == worker.GetID())
                    {
                        check = true;
                        trueID = worker.GetID();
                    }
                }
                if (!check)
                {
                    Console.WriteLine("Пользователь с таким ID не обнаружен.");
                    Console.WriteLine("Для продолжения нажмите Enter...");
                    Console.ReadKey();
                }
            } while (!check);
            return trueID;
        }

        public void ShowhIerarchy()
        {
            Ierarchy();
            Console.ReadKey();
        }

        public void Ierarchy(int n = -1, int LeaderID = -1, int status = 1000, bool recursive = true)
        {
            n++;
            status /= 10;
            string str = new string ('\t', n);
            foreach (var worker in workerList)
            {
                if (worker.GetStatus() == status && (worker.GetLeaderID() == -1 || worker.GetLeaderID() == LeaderID))
                {
                    Console.WriteLine(str + worker.GetID() + "." + worker.GetName());
                    if (recursive)
                        Ierarchy(n, worker.GetID(), worker.GetStatus());
                }
            }
        }
        public int PrintAllWorkers(int status = 1000)
        {
            Console.WriteLine(" __________________________________________________________________ ");
            Console.WriteLine("| ID | Status |        Name       | Leader ID |      Employees     |");
            foreach (var worker in workerList)
            {
                if (worker.GetStatus() <= status)
                {
                    Console.WriteLine("|____|________|___________________|___________|____________________|");
                    Console.WriteLine("| {0,-3}| {1,-7}| {2,-18}| {3,-10}| {4,-18} |", worker.GetID(), worker.GetStatus(), worker.GetName(), worker.GetLeaderID(), worker.GetEmployee());
                }
            }
            Console.WriteLine("|____|________|___________________|___________|____________________|");

            return 0;
        }
        public void UpdateLeaders(int id, string value)
        {
            string[] values = value.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var val in values)
            {
                foreach (var worker in workerList)
                {
                    if (worker.GetID() == Convert.ToInt32(val))
                        worker.SetLeaderID(id);
                }
            }
            UpdateWorkersFile();
            ReadUsersFromFile();
        }
        public int ChangeLeader(int status, bool isTask)
        {
            PrintAllWorkers(status);
            bool check = false;
            do
            {
                Console.Write("Введите ID сотрудника: ");
                int id = Convert.ToInt32(Console.ReadLine());
                foreach (var worker in workerList)
                {
                    if (worker.GetStatus() <= status && worker.GetID() == id)
                    {
                        check = true;
                        if (!isTask)
                        {
                            worker.SetLeaderID(LeaderCheck());
                            UpdateWorkersFile();
                            ReadUsersFromFile();
                            Console.WriteLine("Новое значение успешно установлено.");
                        }
                        else return worker.GetID();
                        break;
                    }
                }
                if (!check)
                {
                    Console.WriteLine("Пользователь с таким ID не обнаружен.");
                    Console.WriteLine("Для продолжения нажмите Enter...");
                    Console.ReadKey();
                }
            } while (!check);
            return 0;
        }
        
        public string EmployeesCheck(int Status)
        {
            string employees = "";
            Console.WriteLine(" ________________________ ");
            Console.WriteLine("| ID |        Name       |");
            foreach (var worker in workerList)
            {
                //Console.WriteLine("|----|-------------------|");
                if (worker.GetStatus() < Status)
                {
                    Console.WriteLine("|----|-------------------|");
                    Console.WriteLine("| {0,-3}| {1,-18}|", worker.GetID(), worker.GetName());
                }
            }
            Console.WriteLine("|____|___________________|");
            bool check = false;
            bool choise;
            Console.WriteLine("Для выхода введите 0 (ноль).");
            do
            {
                choise = false;
                Console.Write($"Введите ID подчиненного-сотрудника ({employees}): ");
                int employeeID = Convert.ToInt32(Console.ReadLine());
                foreach (var worker in workerList)
                {
                    if (worker.GetStatus() < Status && employeeID == worker.GetID())
                    {
                        employees += $"{employeeID},";
                        choise = true;
                    }
                    else if (employeeID == 0) check = true;
                }
                if (!choise && !check)
                {
                    Console.WriteLine("Пользователь с таким ID не обнаружен.");
                    Console.WriteLine("Для продолжения нажмите Enter...");
                    Console.ReadKey();
                }
            } while (!check);
            return employees;
        }
        
        private void ReadUsersFromFile()
        {
            workerList.Clear();
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
                        Console.WriteLine(values.Length);
                        Console.WriteLine(info);
                        break;
                    }
                }
            }
        } 
        
        private void UpdateWorkersFile()
        {
            string path = $"{pathToWorkersDirectory}/users.txt";
            File.Delete(path);
            foreach (var worker in workerList)
            {
                using (FileStream updateFileInfo = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    StreamWriter writer = new StreamWriter(updateFileInfo); 
                    string str = $"{worker.GetID()};{worker.GetStatus()};{worker.GetName()};{worker.GetPassword()};{worker.GetLeaderID()};{worker.GetEmployee()}";
                    writer.WriteLine(str);
                    writer.Close();
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
        protected string employee;

        public Worker(int ID, int Status, string Name, string Password, int LeaderId, string Employee)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = LeaderId;
            employee = Employee;
        }

        public Worker(int ID, int Status, string Name, string Password, int LeaderId)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = LeaderId;
            employee = "no";
        }

        public Worker(int ID, int Status, string Name, string Password, string Employee)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = -1;
            employee = Employee;
        }

        public Worker(int ID, int Status, string Name, string Password)
        {
            id = ID;
            status = Status;
            name = Name;
            password = Password;
            leaderID = -1;
            employee = "no";
        }

        public string LoginWorker(string Login, string Password)
        {
            if (Login == name && Password == password) return $"{id};{status}";
            else return "0";
        }

        public string GetName() { return name; }
        public string GetPassword() { return password; }
        public int GetStatus() { return status; }
        public int GetID() { return id; }
        public int GetLeaderID() { return leaderID; }
        public string GetEmployee() { return employee; }
        
        public void SetStatus(int Status) { status = Status; }
        public void SetLeaderID(int LeaderID) { leaderID = LeaderID; }
        public void SetEmployees(string Employee) { employee = Employee; }

    }
}