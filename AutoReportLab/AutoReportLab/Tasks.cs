using System;
using System.Collections.Generic;
using System.IO;

// TODO Функция создания задач
// TODO Функция изменения задач (комментарий, состояние, назначенный сотрудник)
// TODO Функция чтения файла с задачами и заполнение списка 
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
                    //ReadTasksFromFile();
                }
                else File.Create($"{pathToTasksDirectory}/tasks.txt").Dispose();
            }
        }
        
        private List<Task> tasksList = new List<Task>();
        private string pathToTasksDirectory = $"{Directory.GetCurrentDirectory()}/Tasks";
        
        public void AddTasks(string Name, string Description, int Worker, int Status, string Comment = "no")
        {
            //ReadTasksFromFile();
            int ID = tasksList.Count + 1;
            Task task = new Task(ID, Name, Description, Worker, Status, Comment);
            tasksList.Add(task);        
            //UpdateTasksFile();
            //ReadTasksFromFile();
            if (tasksList.Count == ID && tasksList[tasksList.Count - 1].GetName() == Name)
            {
                Console.WriteLine("Задача успешно добавлена.");
            }
            else
            {
                Console.WriteLine("Ошибка при создании задачи.");
            }
        }
    }
    
    internal class Task
    {
        private int id;
        private string name; 
        private string description;
        private int worker;
        private int status;
        private string comment;

        public Task(int ID, string Name, string Description, int Worker, int Status, string Comment)
        {
            id = ID;
            name = Name;
            description = Description;
            worker = Worker;
            status = Status;
            comment = Comment;
        }
        
        public string GetName() { return name; }
        public int GetWorker() { return worker; }
        public int GetStatus() { return status; }
        public string GetComment() { return comment; }
        
        public void SetWorker(int Worker) { worker = Worker; }
        public void SetStatus(int Status) { status = Status; }
        public void SetComment(string Comment) { comment = Comment; }
    }
}