using CollectionsAndLinq.Common.Models;
using System;
using System.Diagnostics;
using System.Timers;

namespace CoolParking
{
    internal class Program
    {
        private static Random randomTask = new Random();
        private static Stopwatch sw;
        private static List<int> ids = new List<int>();

        private static List<InfoForTask> tasks = new List<InfoForTask>()
            {
               new InfoForTask() {Name = "Count of tasks in projects", IsRunning = false },
               new InfoForTask() {Name = "List of tasks", IsRunning = false },
               new InfoForTask() {Name = "Completed task's", IsRunning = false },
               new InfoForTask() {Name = "Sort team members", IsRunning = false },
               new InfoForTask() {Name = "Sort tasks", IsRunning = false },
               new InfoForTask() {Name = "User with information", IsRunning = false },
               new InfoForTask() {Name = "Project with information", IsRunning = false },
               new InfoForTask() {Name = "Sorted filtered page of projects", IsRunning=false },
            };

        private readonly static CollectionsAndLinq.Timer queries;
        static Program()
        {
            queries = new CollectionsAndLinq.Timer();
            queries.Elapsed += GetId;
            queries.Ids = ids;
        }

        static async Task Main(string[] args)
        {
            
            do
            {
                var markedTaskId = await queries.MarkRandomTaskWithDelay(1000);
                await Menus.FirstMenu();
            } while (Menus.Work);
            Console.ReadKey();
        }

        public static void GetId(Object source, ElapsedEventArgs e)
        {
            TaskCompletionSource<int> tcs1 = new TaskCompletionSource<int>();
            Task<int> t1 = tcs1.Task;
            int randomTasksId = randomTask.Next(0, tasks.Count);

            if (!tasks[randomTasksId].IsRunning)
            {
                Task.Factory.StartNew(() =>
                {
                    tcs1.SetResult(randomTasksId);
                });

                tasks[randomTasksId].IsRunning = true;
            }
            else
            {
                throw new InvalidOperationException("Task is complete");
            }

            sw = Stopwatch.StartNew();

            try
            {
                ids.Add(t1.Result);
                sw.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n-------------------------------------------------\n{0}", ex.ToString());
            }
        }
    }
}
