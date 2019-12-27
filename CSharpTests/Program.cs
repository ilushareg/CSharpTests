using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTests
{
    public class Act
    {
        public Act()
        {
            Do(delegate (string s) { Console.WriteLine(s); return 5; });
            DoVoid(delegate { Console.WriteLine("DoVoid"); return; });

            //Func<int> a = new Func<int>{return 1; };

        }

    void DoVoid(Action dv)
        {
            dv();
        }

        void Do(Func<string, int> myMethodName)
        {
            int i = myMethodName("My String");

        }

    }
    public class ThreadingTests
    {
        public ThreadingTests()
        {

        }
        public void DoTest()
        {
            Thread[] tList = new Thread[5];
            {
                for (int i = 0; i < tList.Length; i++)
                {
                    Thread thr = new Thread(new ParameterizedThreadStart(delegate (Object o)
                    {
                        try
                        {
                            while (true)
                            {
                                Console.WriteLine("Hello Thread {0}", (int)o);
                                Thread.Sleep(100);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception caught {0}", e.Message);
                        }

                    }));
                    //Thread thr = new Thread(new ThreadStart(delegate { tstatic = Thread.CurrentThread.ManagedThreadId; Console.WriteLine(tstatic); tstatic = 99; Thread.Sleep(100); Console.WriteLine(tstatic); }));
                    tList[i] = thr;
                }
            }

            for (int i = 0; i < tList.Length; i++)
            {
                tList[i].Start(i);
            }
            for (int i = 0; i < tList.Length; i++)
            {

                tList[i].Abort();
            }

        }
    }

    class Program
    {
        public static int tstatic = 0;

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            //ThreadingTests t = new ThreadingTests();
            //t.DoTest();

            TaskTests tt = new TaskTests();
            tt.DoTest();

        }
    }
}
