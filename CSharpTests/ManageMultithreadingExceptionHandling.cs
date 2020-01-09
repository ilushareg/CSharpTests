using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTests
{
    public class ManageMultithreadingExceptionHandling
    {
        public ManageMultithreadingExceptionHandling()
        {
            
        }
        public void DoTest()
        {
            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;
            Task task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                //throw (new Exception("sdjhsjdhsjdh"));
                token.ThrowIfCancellationRequested();

            }, token)
                .ContinueWith((t) =>
            {
                Thread.Sleep(100);

                Console.WriteLine("exception handled");
                //Console.WriteLine(t.Exception.Message);
                //t.Exception.Handle((e) => { return true; }); 
                //Console.WriteLine("exception handled");
                //Console.WriteLine($"Continuation {emessage}");
            })
;


            try
            {
                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();
                cancellationTokenSource.Cancel();
                task.Wait();
                Console.WriteLine("exception handled outside #1");
                Console.WriteLine(task.Exception.InnerExceptions[0].Message);

            }
            catch (AggregateException e)
            {
                Console.WriteLine("exception handled outside #2");
                Console.WriteLine(e.InnerExceptions[0].Message);
            }
        }

    }
}
