using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace CSharpTests
{

    class Program
    {
        public static int tstatic = 0;

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            //ThreadingTests t = new ThreadingTests();
            //t.DoTest();

            DelegateTests tt = new DelegateTests();
            tt.DoTest();

            //ExceptionTests tt = new ExceptionTests();
            //tt.DoTest();

            //ConsumeDataTests tt = new ConsumeDataTests();
            //tt.DoTest();

            RPSRules.testRules();

            RPSgame g = new RPSgame();
            g.run();

            //StringTests tt = new StringTests();
            //tt.DoTest();

        }
    }
}
