using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

// TODO Функция изменения задач (комментарий, состояние, назначенный сотрудник)
// TODO Функция вывода всех задач


// TODO

namespace AutoReportLab
{
    public class Tasks
    {
        public Tasks()
        {
            if (!Directory.Exists(pathToTasksDirectory))
            {
                Directory.CreateDirectory(pathToTasksDirectory);
                File.Create(Path.Combine(pathToTasksDirectory, "tasks.txt")).Dispose();
            }
            else
            {
                if (File.Exists(Path.Combine(pathToTasksDirectory, "tasks.txt")))
                {
                    ReadTaskFile();
                }
                File.Create(Path.Combine(pathToTasksDirectory, "tasks.txt")).Dispose();
            }
        }
        
        private List<Task> tasksList = new List<Task>();
        private string pathToTasksDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Tasks");
        
        public void AddTasks(string Name, string Description, int Worker, int Status, string Comment = "no")
        {
            ReadTaskFile();
            int ID = tasksList.Count + 1;
            Task task = new Task(ID, Name, Description, Worker, Status, Comment);
            tasksList.Add(task);        
            WriteTaskFile(task);// TODO проверка имени
            ReadTaskFile();
            if (tasksList.Count == ID && tasksList[tasksList.Count - 1].GetName() == Name)
            {
                Console.WriteLine("Задача успешно добавлена.");
            }
            else
            {
                Console.WriteLine("Ошибка при создании задачи.");
            }
        }
        // TODO функция проверки имен (нижний регистр)
        private void WriteTaskFile(Task task)
        {
            string path = Path.Combine(pathToTasksDirectory, $"{task.GetName()}.txt");
            File.Create(path).Dispose();
            using (StreamWriter writer = new StreamWriter(path))
            {
                string str = $"{task.GetID()};{task.GetName()};{task.GetDescription()};{task.GetWorkerID()}:{task.GetWorker()};{task.GetStatusID()}:{task.GetStatus()};{task.GetCommentID()}:{task.GetComment()};{task.GetTime()};";
                writer.WriteLine(str);
            }
        }

        public void UpdateTasks(int ID, int field, string newValue)
        {
            foreach (var task in tasksList)
            {
                if (task.GetID() == ID)
                {
                    switch (field)
                    {
                        case 1:
                            if (newValue == "1")
                            {
                                task.SetStatus(1);
                                task.SetStatusID(task.GetStatusID() + 1);
                                task.SetTime(DateTime.Now);
                            }
                            else if (newValue == "2")
                            {
                                task.SetStatus(2);
                                task.SetStatusID(task.GetStatusID() + 1);
                                task.SetTime(DateTime.Now);
                            }
                            else if (newValue == "3")
                            {
                                task.SetStatus(3);
                                task.SetStatusID(task.GetStatusID() + 1);
                                task.SetTime(DateTime.Now);
                            }
                            AppendTaskFile(task);
                            Console.WriteLine("Значение изменено.");
                            return;
                        case 2:
                            int worker;
                            if (Int32.TryParse(newValue, out worker))
                            {
                                task.SetWorker(worker);
                                task.SetWorkerID(task.GetWorkerID() + 1);
                                task.SetTime(DateTime.Now);
                                AppendTaskFile(task);
                                Console.WriteLine("Значение изменено.");
                            }
                            return;
                        case 3:
                            task.SetComment(newValue);
                            task.SetCommentID(task.GetCommentID() + 1);
                            task.SetTime(DateTime.Now);
                            AppendTaskFile(task); 
                            Console.WriteLine("Значение изменено.");
                            return;
                    }
                }
            }
            Console.WriteLine("Ошибка во время изменения. Проверьте введенные данные и повторите попытку.");
            return;
        }
        
        private void AppendTaskFile(Task task)
        {
            string path = Path.Combine(pathToTasksDirectory, $"{task.GetName()}.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                string str = $"{task.GetID()};{task.GetName()};{task.GetDescription()};{task.GetWorkerID()}:{task.GetWorker()};{task.GetStatusID()}:{task.GetStatus()};{task.GetCommentID()}:{task.GetComment()};{task.GetTime()};";
                writer.WriteLine(str);
            }
        }

        public void PrintOneTask(int id)
        {
            Console.WriteLine(" ____________________________________________________________________________________________ ");
            Console.WriteLine("| ID |  Status  |       Name        | Worker |           Description/Comment              ");
            foreach (var task in tasksList)
            {
                if (task.GetID() == id)
                {
                    string status = "";
                    switch (task.GetStatus())
                    {
                        case 1:
                            status = "Open";
                            break;
                        case 2:
                            status = "Active";
                            break;
                        case 3:
                            status = "Resolved";
                            break;
                    }
                    Console.WriteLine("|____|__________|___________________|________|_______________________________________________");
                    Console.WriteLine("| {0,-3}| {1,-8} | {2,-18}|   {3,-4} | {4} / {5}  ", task.GetID(), status, task.GetName(), task.GetWorker(), task.GetDescription(), task.GetComment());
                    break;
                }
            }
            Console.WriteLine("|____|__________|___________________|________|_______________________________________________");
        }

        public int PrintAllTasks(bool choise, int workerId = 0)
        {
            Console.Clear();
            ReadTaskFile();
            Console.WriteLine(" ____________________________________________________________________________________________ ");
            Console.WriteLine("| ID |  Status  |       Name        | Worker |           Description/Comment              ");
            foreach (var task in tasksList)
            {
                if (workerId != 0)
                {
                    if (task.GetWorkerID() == workerId)
                    {
                        string status = "";
                        switch (task.GetStatus())
                        {
                            case 1:
                                status = "Open";
                                break;
                            case 2:
                                status = "Active";
                                break;
                            case 3:
                                status = "Resolved";
                                break;
                        }

                        Console.WriteLine("|____|__________|___________________|________|_______________________________________________");
                        Console.WriteLine("| {0,-3}| {1,-8} | {2,-18}|   {3,-4} | {4} / {5}  ", task.GetID(), status, task.GetName(), task.GetWorker(), task.GetDescription(), task.GetComment());

                    }
                }
                else
                {
                    string status = "";
                    switch (task.GetStatus())
                    {
                        case 1:
                            status = "Open";
                            break;
                        case 2:
                            status = "Active";
                            break;
                        case 3:
                            status = "Resolved";
                            break;
                    }

                    Console.WriteLine("|____|__________|___________________|________|_______________________________________________");
                    Console.WriteLine("| {0,-3}| {1,-8} | {2,-18}|   {3,-4} | {4} / {5}  ", task.GetID(), status, task.GetName(), task.GetWorker(), task.GetDescription(), task.GetComment());
                }
            }
            Console.WriteLine("|____|__________|___________________|________|_______________________________________________");
            if (choise)
            {
                bool check = false;
                do
                {
                    Console.Write("Введите ID задачи: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    foreach (var task in tasksList)
                        if (task.GetID() == id) return id;
                    if (!check)
                    {
                        Console.WriteLine("Задача с таким ID не обнаружена.");
                        Console.WriteLine("Для продолжения нажмите Enter...");
                        Console.ReadKey();
                    }
                } while (!check);
            }
            return 0;
        }
        
        public void ReadTaskFile()
        {
            try
            {
                int ID; 
                string Name;
                string Description;
                int id_worker;
                int Worker;
                int id_status;
                int Status;
                int id_comment;
                string Comment;
                DateTime time;
                tasksList.Clear();
                
                string path = $"{pathToTasksDirectory}";
                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    string str = "", str2 = "";
                    using (StreamReader reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            str2 = reader.ReadLine();
                            if (str2 != "")
                            {
                                str = str2;
                            }
                        }
                    }

                    string[] fields = str.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                    string[] field;
                    if (fields.Length == 7)
                    {
                        ID = Convert.ToInt32(fields[0]);
                        Name = fields[1];
                        Description = fields[2];
                        field = fields[3].Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                        id_worker = Convert.ToInt32(field[0]);
                        Worker = Convert.ToInt32(field[1]);
                        field = fields[4].Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                        id_status = Convert.ToInt32(field[0]);
                        Status = Convert.ToInt32(field[1]);
                        field = fields[5].Split(new char[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                        id_comment = Convert.ToInt32(field[0]);
                        Comment = field[1];
                        time = Convert.ToDateTime(fields[6]);
                        Task task = new Task(ID, Name, Description, Worker, Status, Comment, time);
                        task.SetWorkerID(id_worker);
                        task.SetStatusID(id_status);
                        task.SetCommentID(id_comment);
                        tasksList.Add(task);
                        //
                        string str1 = $"{task.GetID()};{task.GetName()};{task.GetDescription()};{task.GetWorkerID()}:{task.GetWorker()};{task.GetStatusID()}:{task.GetStatus()};{task.GetCommentID()}:{task.GetComment()};{task.GetTime()};";
                        Console.WriteLine(str1);
                        //
                    }
                    else Console.WriteLine("Неверный формат записи данных.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    
    internal class Task
    {
        private int id;
        private string name; 
        private string description;
        private int id_worker;
        private int worker;
        private int id_status;
        private int status;
        private int id_comment;
        private string comment;
        private DateTime time;

        public Task(int ID, string Name, string Description, int Worker, int Status, string Comment)
        {
            id = ID;
            name = Name;
            description = Description;
            worker = Worker;
            id_worker = 0;
            status = Status;
            id_status = 0;
            comment = Comment;
            id_comment = 0;
            time = DateTime.Now;
        }
        
        public Task(int ID, string Name, string Description, int Worker, int Status, string Comment, DateTime Time)
        {
            id = ID;
            name = Name;
            description = Description;
            worker = Worker;
            id_worker = 0;
            status = Status;
            id_status = 0;
            comment = Comment;
            id_comment = 0;
            time = Time;
        }
        
        public int GetID() { return id; }
        public string GetName() { return name; }
        public string GetDescription() { return description; }
        public int GetWorker() { return worker; }
        public int GetStatus() { return status; }
        public string GetComment() { return comment; }
        public DateTime GetTime() { return time; }
        
        public int GetWorkerID() { return id_worker; }
        public int GetStatusID() { return id_status; }
        public int GetCommentID() { return id_comment; }
        
        public void UpdateWorker() { id_worker++; }
        public void UpdateStatus() { id_status++; }
        public void UpdateComment() { id_comment++; }
        public void UpdateTime() { time = DateTime.Now; }
            
        public void SetWorker(int Worker) { worker = Worker; }
        public void SetStatus(int Status) { status = Status; }
        public void SetComment(string Comment) { comment = Comment; }
        public void SetTime(DateTime Time) { time = Time; }
        
        public void SetWorkerID(int ID) { id_worker = ID; }
        public void SetStatusID(int ID) { id_status = ID; }
        public void SetCommentID(int ID) { id_comment = ID; }
    }
}