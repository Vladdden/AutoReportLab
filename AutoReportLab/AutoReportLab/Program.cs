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
                // TODO выстроить иерархию табуляциями в виде ступеней.
                // Влад
                //      Вова
                //      Игорь
                //          Антон
                //          Таня
                //      Петя
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

        public static void AddWorker()
        {
            //AddWorker(int Status, string Name, string Password, int LeaderId = 0, string Employees = "no")
            Console.Write("Введите имя пользователя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Руководители:");
            string leaderID = workers.LeaderCheck().ToString();
            // TODO Выводить список сотрудников и выбирать руководителя, если он до этого не имел руководящую должность --) менять его статус
            Console.WriteLine("Варианты должностей: ");
            Console.WriteLine("1. Руководитель.");
            Console.WriteLine("2. Рабочий. ");
            Console.Write("Введите номер должности сотрудника: ");
            string status = Console.ReadLine();
            string employees = "";
            if (Convert.ToInt32(status) == 1)
            {
                Console.WriteLine("Выбор сотрудников, находящихся в подчинении:");
                employees = workers.EmployeesCheck(10);
                // TODO тоже обновить поле "Руководителя" для этих тел 
                // TODO вывести пользователей и через запятую указать айдишники
                // TODO создать массив из этих айдишников, который будет передаваться в функцию для обновления инфы.

            }
            Console.WriteLine("Введите пароль пользователя.");
            string pass = Console.ReadLine();
            if (leaderID != "" && employees == "") 
                workers.AddWorker(Convert.ToInt32(status), name, pass, Convert.ToInt32(leaderID), employees);
            else if (leaderID != "" && employees == "")
                workers.AddWorker(Convert.ToInt32(status), name, pass, Convert.ToInt32(leaderID));
            else if (leaderID == "" && employees != "")
                workers.AddWorker(Convert.ToInt32(status), name, pass, Employees: employees);
            else workers.AddWorker(Convert.ToInt32(status), name, pass);
            Console.WriteLine("Пользователь успешно добавлен.");
            Console.ReadKey();
        }
    }
}