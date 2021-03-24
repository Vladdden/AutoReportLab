using System;

namespace AutoReportLab
{
    public class TasksManageSystem
    {
        public TasksManageSystem(int status)
        {
            tasks = new Tasks();
            workerStatus = status;
        }

        private Tasks tasks;
        private Workers workers = new Workers();
        private readonly int workerStatus;
        
        public void Main()
        {
            string choise = "";
            while (choise != "0")
            {
                Console.Clear();
                Console.Write("Добро пожаловать в систему управления задачами!");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить задачу.");
                Console.WriteLine("2. Изменить задачу.");
                Console.WriteLine("3. Поиск задач по критериям.");
                Console.WriteLine("4. Посмотреть задачи своих подчиненных.");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        AddNewTask();
                        break;
                    case "2":
                        UpdateTasks(tasks.PrintAllTasks(true));
                        break;
                    case "3":
                        //SetBossForWorker();
                        break;
                    case "4":
                        //tasks.ReadTaskFile();
                        break;
                    case "0":
                        break;
                }
                Console.WriteLine("Нажмите Enter, чтобы продолжить");
                Console.ReadKey();
            }
        }

        private void AddNewTask()
        {
            Console.Write("Введите имя задачи: ");
            string name = Console.ReadLine();
            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();
            Console.WriteLine("Выберите исполняющего сотрудника:");
            int worker = workers.ChangeLeader(workerStatus, true);
            Console.Write("Введите комментарий к задаче: ");
            string comment = Console.ReadLine();
            tasks.AddTasks(name, description, worker, 1, comment);
            Console.ReadKey();
        }

        private void UpdateTasks(int ID)
        {
            int number, num;
            string newValue;
            tasks.PrintOneTask(ID);
            Console.WriteLine("Выберите действие (номер):");
            Console.WriteLine("1. Изменить статус.");
            Console.WriteLine("2. Изменить исполняющего сотрудника.");
            Console.WriteLine("3. Изменить комментарий.");
            Console.Write("Выбор: ");
            string choise = Console.ReadLine();
            Int32.TryParse(choise, out number);
            while (number != 1 && number != 2 && number != 3)
            {
                Console.Write("Введен некорректный номер.\nПовторите попытку: ");
                choise = Console.ReadLine();
                Int32.TryParse(choise, out number);
            }
            Console.Clear();
            tasks.PrintOneTask(ID);
            switch (number)
            {
                case 1:
                    Console.WriteLine("Введите новый статус(номер): ");
                    Console.WriteLine("1. Открыта.");
                    Console.WriteLine("2. Активна.");
                    Console.WriteLine("3. Решена.");
                    newValue = Console.ReadLine();
                    Int32.TryParse(newValue, out num);
                    while (num != 1 && num != 2 && num != 3)
                    {
                        Console.Write("Введен некорректный номер.\nПовторите попытку: ");
                        newValue = Console.ReadLine();
                        Int32.TryParse(newValue, out num);
                    }
                    tasks.UpdateTasks(ID, number, num.ToString());
                    break;
                case 2:
                    Console.WriteLine("Выберите исполняющего сотрудника:");
                    int worker = workers.ChangeLeader(workerStatus, true);
                    tasks.UpdateTasks(ID, number, worker.ToString());

                    break;
                case 3:
                    Console.Write("Введите новый комментарий: ");
                    newValue = Console.ReadLine();
                    tasks.UpdateTasks(ID, number, newValue);
                    break;
            }
        }
        
    }
}