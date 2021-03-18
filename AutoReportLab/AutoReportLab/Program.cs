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
    }
}