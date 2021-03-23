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
                        //AddWorker();
                        break;
                    case "3":
                        //SetBossForWorker();
                        break;
                    case "4":
                        tasks.ReadTaskFile();
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

        
        
    }
}