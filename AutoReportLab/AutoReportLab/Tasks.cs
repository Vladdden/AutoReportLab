using System;
using System.Collections.Generic;
using System.IO;
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
                File.Create($"{pathToTasksDirectory}/tasks.txt").Dispose();
            }
            else
            {
                if (File.Exists($"{pathToTasksDirectory}/tasks.txt"))
                {
                    ReadTaskFile();
                }
                else File.Create($"{pathToTasksDirectory}/tasks.txt").Dispose();
            }
        }
        
        private List<Task> tasksList = new List<Task>();
        private string pathToTasksDirectory = $"{Directory.GetCurrentDirectory()}/Tasks";
        
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
            string path = $"{pathToTasksDirectory}/{task.GetName()}.txt";
            File.Create(path).Dispose();
            using (StreamWriter writer = new StreamWriter(path))
            {
                string str = $"{task.GetID()};{task.GetName()};{task.GetDescription()};{task.GetWorkerID()}:{task.GetWorker()};{task.GetStatusID()}:{task.GetStatus()};{task.GetCommentID()}:{task.GetComment()}";
                writer.WriteLine(str);
            }
        }
        
        public void ReadTaskFile()
        {
            try
            {
                int id_id;
                int ID; 
                int id_name;
                string Name;
                int id_description;
                string Description;
                int id_worker;
                int Worker;
                int id_status;
                int Status;
                int id_comment;
                string Comment;
                tasksList.Clear();
                
                string path = $"{pathToTasksDirectory}";
                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    string str = "";
                    using (StreamReader reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        { str = reader.ReadLine(); }
                    }

                    string[] fields = str.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                    string[] field;
                    if (fields.Length == 6)
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
                        Task task = new Task(ID, Name, Description, Worker, Status, Comment);
                        task.SetWorkerID(id_worker);
                        task.SetStatusID(id_status);
                        task.SetCommentID(id_comment);
                        tasksList.Add(task);
                        //
                        string str1 = $"{task.GetID()};{task.GetName()};{task.GetDescription()};{task.GetWorkerID()}:{task.GetWorker()};{task.GetStatusID()}:{task.GetStatus()};{task.GetCommentID()}:{task.GetComment()}";
                        Console.WriteLine(str1);
                        //
                    }
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
        
        public void SetWorkerID(int ID) { id_worker = ID; }
        public void SetStatusID(int ID) { id_status = ID; }
        public void SetCommentID(int ID) { id_comment = ID; }
    }
}