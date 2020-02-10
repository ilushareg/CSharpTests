using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
        public delegate bool checkfilter(IEnumerable<string> si);
        Func<string, bool> chrckfunc = (s) => { return (s.Length > 2); };

        static WeakReference wr = null;

        public ThreadingTests()
        {
            wr = new WeakReference(new int[100]);
            int[] a = (int[])wr.Target;
            

        }
        public void DoTest()
        {
            Console.WriteLine("starting");

            //checkfilter a = Func<IEnumerable<string>, bool>((s) => { return (s.Length > 2); });


            List<string> ls = new List<string>();
            ls.Add("aaa");
            ls.Add("aa");
            ls.Add("VVVa");

            IEnumerable<string> filtered = ls.Where(chrckfunc);
            foreach(string s in filtered)
            {
                Console.WriteLine(s);

            }

            return;


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

            //TaskTests tt = new TaskTests();
            //tt.DoTest();

            //ExceptionTests tt = new ExceptionTests();
            //tt.DoTest();

            FileTests tt = new FileTests();
            tt.DoTest();

            //StringTests tt = new StringTests();
            //tt.DoTest();

        }
    }
}
