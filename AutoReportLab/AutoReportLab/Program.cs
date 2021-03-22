using System;

namespace AutoReportLab
{
    internal class Program
    {
        private static Workers workers = new Workers();
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
                        isExit = UserIsWorker();
                    break;
            }
        }

        public static bool UserIsTeamLeader()
        {
            string choise = "";
            while (choise != "0")
            {
                Console.Clear();
                Console.WriteLine("Выберите действие (или выход):");
                Console.WriteLine("1. Сиситема управления заданиями.");
                Console.WriteLine("2. Добавить сотрудника.");
                Console.WriteLine("3. Назначить руководителя.");
                Console.WriteLine("4. Просмотреть своих подчиненных.");
                Console.WriteLine("5. Просмотреть иерархию сотрудников.");
                
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        //Fio();
                        break;
                    case "2":
                        AddWorker();
                        break;
                    case "3":
                        SetBossForWorker();
                        break;
                    case "4":
                        Console.WriteLine("Ваши подчиненные:");
                        workers.Ierarchy(-1, workers.workerID, workers.workerStatus, false);
                        break;
                    case "5":
                        workers.ShowhIerarchy();
                        break;
                    case "0":
                        return true;
                }
                Console.WriteLine();
                Console.WriteLine("Нажмите Enter, чтобы продолжить");
                Console.ReadKey();
            }
            return false;
        }
        
        public static bool UserIsBoss()
        {
            string choise = "";
            while (choise != "0")
            {
                Console.Clear();
                Console.WriteLine("Выберите действие (или выход):");
                Console.WriteLine("1. .");
                Console.WriteLine("2. .");
                Console.WriteLine("3. Просмотреть своих подчиненных.");
                Console.WriteLine("4. Просмотреть иерархию сотрудников.");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        //Fio();
                        break;
                    case "2":
                        //Rectangle();
                        break;
                    case "3":
                        Console.WriteLine("Ваши подчиненные:");
                        workers.Ierarchy(-1, workers.workerID, workers.workerStatus, false);
                        break;
                    case "4":
                        //Cube();
                        break;
                    case "0":
                        return true;
                }
                Console.WriteLine();
                Console.WriteLine("Нажмите Enter, чтобы продолжить");
                Console.ReadKey();
            }
            return false;
        }
        
        public static bool UserIsWorker()
        {
            string choise = "";
            while (choise != "0")
            {
                Console.Clear();
                Console.WriteLine("Выберите действие (или выход):");
                Console.WriteLine("1. .");
                Console.WriteLine("2. Список задач.");
                Console.WriteLine("3. Отчет за день.");
                Console.WriteLine("4. Просмотреть иерархию сотрудников.");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        //Fio();
                        break;
                    case "2":
                        //Rectangle();
                        break;
                    case "3":
                        //Average();
                        break;
                    case "4":
                        //Cube();
                        break;
                    case "0":
                        return true;
                }
                Console.WriteLine();
                Console.WriteLine("Нажмите Enter, чтобы продолжить");
                Console.ReadKey();
            }
            return false;
        }

        public static void AddWorker()
        {
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            bool tr = false;
            string employees = "";
            string leaderID = "";
            int status;
            do
            {
                Console.WriteLine("Варианты должностей: ");
                Console.WriteLine("1. Руководитель.");
                Console.WriteLine("2. Рабочий. ");
                Console.Write("Введите номер должности сотрудника: ");
                status = Convert.ToInt32(Console.ReadLine());
                
                if (status == 1)
                {
                    status = 10;
                    tr = true;
                    Console.WriteLine("Выбор сотрудников, находящихся в подчинении:");
                    employees = workers.EmployeesCheck(10);
                }
                else if (status == 2)
                {
                    Console.WriteLine("Руководители:");
                    leaderID = workers.LeaderCheck().ToString();
                    status = 1;
                    tr = true;
                }
                else
                {
                    Console.Write("Ошибка ввода.");
                    tr = false;
                }
            } while (!tr);
            Console.WriteLine("Введите пароль пользователя.");
            string pass = Console.ReadLine();
            // TODO пустые строки
            int id;
            if (leaderID != "" && employees != "") 
                id = workers.AddWorker(status, name, pass, Convert.ToInt32(leaderID), employees);
            else if (leaderID != "" && employees == "")
                id = workers.AddWorker(status, name, pass, Convert.ToInt32(leaderID));
            else if (leaderID == "" && employees != "")
                id = workers.AddWorker(status, name, pass, Employees: employees);
            else id = workers.AddWorker(status, name, pass);
            if (id > 0) workers.UpdateLeaders(id, employees);    
            Console.ReadKey();
        }

        public static void SetBossForWorker()
        {
            Console.WriteLine("Выберите, кому назначить руководителя:");
            workers.ChangeLeader(10);
        }

    }
}