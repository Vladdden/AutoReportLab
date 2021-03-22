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
        
        private List<Worker> tasksList = new List<Worker>();
        private string pathToTasksDirectory = $"{Directory.GetCurrentDirectory()}/Tasks";
        
    }
    
    internal class Task
    {
        protected int id;
        protected string name; 
        protected string description;
        protected int worker;
        protected int status;
        protected string comment;
        
    }
}