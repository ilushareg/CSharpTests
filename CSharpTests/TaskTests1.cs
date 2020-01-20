using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTests
{
    public class TaskTests1
    {
        public TaskTests1()
        {
            
        }
        public Task runTask()
        {
            Task t = Task<int>.Run(() => { Thread.Sleep(2000); return 1; });
            return t;
        }

        public void DoTest()
        {

            Task<int>[] tasks = new Task<int>[3];
            tasks[0] = Task.Run(() => { Thread.Sleep(2000); return 1; });
            tasks[1] = Task.Run(() => { Thread.Sleep(1000); return 2; });
            tasks[2] = Task.Run(() => { Thread.Sleep(3000); return 3; });

            Task tt = runTask();

            while (tasks.Length > 0)
            {
                int i = Task.WaitAny(tasks);
                Task<int> completedTask = tasks[i];
                Console.WriteLine(completedTask.Result);
                var temp = tasks.ToList();
                temp.RemoveAt(i);
                tasks = temp.ToArray();
            }
        }
    }
}
