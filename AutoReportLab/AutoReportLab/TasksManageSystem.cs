using System;

namespace AutoReportLab
{
    public class TasksManageSystem
    {
        private Tasks tasks;
        public TasksManageSystem()
        {
            tasks = new Tasks();
        }

        public void Main()
        {
            string choise = "";
            while (choise != "0")
            {
                Console.Clear();
                Console.Write("Добро пожаловать в систему управления задачами!");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. .");
                Console.WriteLine("2. .");
                Console.WriteLine("3. .");
                Console.WriteLine("4. .");
                Console.WriteLine("5. .");
                
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        //tasksManageSystem.Main();
                        break;
                    case "2":
                        //AddWorker();
                        break;
                    case "3":
                        //SetBossForWorker();
                        break;
                    case "4":
                        //Console.WriteLine("Ваши подчиненные:");
                        //workers.Ierarchy(-1, workers.workerID, workers.workerStatus, false);
                        break;
                    case "5":
                        //workers.ShowhIerarchy();
                        break;
                    case "0":
                        break;
                }
                Console.WriteLine("Нажмите Enter, чтобы продолжить");
                Console.ReadKey();
            }
        }
    }
}