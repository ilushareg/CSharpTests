using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTests
{
    public class TaskTests
    {
        public TaskTests()
        {
        }
        public void DoTest()
        {


            Task<Int32[]> t = Task.Run(() =>
            {

                Int32 []res = new Int32[100];
                int l = res.Length;

                Task[] childTasks = new Task[l];
                //int l = 5;
                for (int z = 0; z < l; z++)
                {
                    int nn = z;
                    childTasks[z] = new Task(() => {
                        res[nn] = nn;
                        for (int q = 0; q < 15; q++)
                        {
                            Console.WriteLine("Thread {0} step {1}", nn, q);
                            //Console.Write("T");
                            Thread.Sleep(10);
                        }
                    }, TaskCreationOptions.AttachedToParent);

                    childTasks[z].Start();
                    Thread.Sleep(10);


                }

                for (int x = 0; x < 10; x++)
                {
                    Console.Write("*");
                    Thread.Sleep(10);
                }

                Console.WriteLine("ThreadDone");
                Task.WaitAll(childTasks);

                return res;
            });


            t.ContinueWith<string>((f) =>
            {
                foreach (int i in f.Result)
                    Console.WriteLine(i);

                return "aaa";
            }, TaskContinuationOptions.OnlyOnRanToCompletion);


            t.Wait();
 
        }
    }
}
