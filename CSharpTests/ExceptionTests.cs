using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;

namespace CSharpTests
{
    public class StackDepthException : Exception
    {
        public int depth = 0;

        System.Runtime.ExceptionServices.ExceptionDispatchInfo ex = null;

        public StackDepthException(StackOverflowException e)
        {
            ex = ExceptionDispatchInfo.Capture(e);
        }

    }
    public class ExceptionTests
    {
        public ExceptionTests()
        {
            
        }

        public static void Recursive(int n)
        {
            try {
                if (n == 10)
                {
                    throw (new StackOverflowException("asd"));
                }
                Recursive(++n);
            }
            catch (Exception e)
            {
                //throw;
                Console.WriteLine("Caught general exception");
                System.Runtime.ExceptionServices.ExceptionDispatchInfo ex = ExceptionDispatchInfo.Capture(e);
                ex.Throw();
            }

        }
        public void DoTest()
        {

            try
            {
                Recursive(0);
                //throw (new Exception("ksjdhksjdfh"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                Exception curr = e;

                while (curr != null)
                {
                    Console.WriteLine("depth ");

                    curr = e.InnerException;
                }
            }
        }

    }
}
