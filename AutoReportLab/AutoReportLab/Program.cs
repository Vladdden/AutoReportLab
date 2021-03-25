using System;

namespace AutoReportLab
{
    internal class Program
    {
        private static Workers workers = new Workers();
        private static TasksManageSystem tasksManageSystem;
        private static Tasks tasks = new Tasks();
        private static Reports reports = new Reports(workers);
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
                Console.WriteLine("Выберите действие (или выход):"); // TODO убрать везде (или выход)
                Console.WriteLine("1. Сиситема управления заданиями.");
                Console.WriteLine("2. Добавить сотрудника.");
                Console.WriteLine("3. Назначить руководителя.");
                Console.WriteLine("4. Просмотреть своих подчиненных.");
                Console.WriteLine("5. Просмотреть свои задачи.");
                Console.WriteLine("6. Просмотреть иерархию сотрудников.");
                Console.WriteLine("7. Посмотреть решенные задачи (свой отчет).");
                
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        tasksManageSystem = new TasksManageSystem(workers);
                        tasksManageSystem.Main();
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
                        ShowWorkerTasks();
                        break;
                    case "6":
                        workers.ShowhIerarchy();
                        break;
                    case "7":
                        tasks.PrintOldTasks(workers.workerID);
                        break;
                    case "0":
                        return true; // TODO убрать return во всех циклах
                }
                Console.WriteLine(); // TODO убрать везде
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
                Console.WriteLine("1. Просмотреть свои задачи.");
                Console.WriteLine("2. Посмотреть решенные задачи (свой отчет).");
                Console.WriteLine("3. Просмотреть своих подчиненных.");
                Console.WriteLine("4. Просмотреть иерархию сотрудников.");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        ShowWorkerTasks();
                        break;
                    case "2":
                        tasks.PrintOldTasks(workers.workerID);
                        break;
                    case "3":
                        Console.WriteLine("Ваши подчиненные:");
                        workers.Ierarchy(-1, workers.workerID, workers.workerStatus, false);
                        break;
                    case "4":
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
        
        public static bool UserIsWorker()
        {
            string choise = "";
            while (choise != "0")
            {
                Console.Clear();
                Console.WriteLine("Выберите действие (или выход):");
                Console.WriteLine("1. Просмотреть свои задачи.");
                Console.WriteLine("2. Посмотреть решенные задачи (свой отчет).");
                Console.WriteLine("3. Просмотреть иерархию сотрудников.");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        ShowWorkerTasks();
                        break;
                    case "2":
                        tasks.PrintOldTasks(workers.workerID);
                        break;
                    case "3":
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

        private static void ShowWorkerTasks()
        {
            tasks.PrintAllTasks(false, workers.workerID);
            Console.WriteLine("Желаете внести изменения (статус, исполняющий сотрудник, комментарий)? ");
            string answer = Console.ReadLine();
            if (answer == "Да" || answer == "да" || answer == "ДА")
            {
                tasksManageSystem = new TasksManageSystem(workers);
                tasksManageSystem.UpdateTasks(tasks.PrintAllTasks(true, workers.workerID));
            }
            else if (answer == "Нет" || answer == "нет" || answer == "НЕТ") return;
        }
        
        private static void SetBossForWorker()
        {
            Console.WriteLine("Выберите, кому назначить руководителя:");
            workers.ChangeLeader(10, false);
        }
    }
}