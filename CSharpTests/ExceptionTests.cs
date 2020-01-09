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
            catch (StackDepthException e)
            {
                e.depth++;

                throw;
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine("StackOverflow caught");
                throw new StackDepthException(e);

            }
            catch (Exception e)
            {
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
            catch(StackDepthException e)
            {
                Console.WriteLine("depth = {0}", e.depth);

            }
            catch (Exception e)
            {
                Console.WriteLine("Caught Exception");
            }
        }

    }
}
