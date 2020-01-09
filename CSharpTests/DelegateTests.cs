using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTests
{
    public class DelegateTests
    {
        public DelegateTests()
        {
            
        }
        public class MyArgs : EventArgs
        {
            public MyArgs(int value)
            {
                Value = value;
            }
            public int Value { get; set; }
        }
        public class Pub
        {
            public event EventHandler<MyArgs> OnChange = delegate { };
            public void Raise()
            {
                OnChange(this, new MyArgs(42));
            }
        }
        public void CreateAndRaise()
        {
            Pub p = new Pub();
            int a = 0;

            EventHandler<MyArgs> act = (sender, e)
                =>
            {
                int z = a;
                Console.WriteLine("Event raised: {0} {1}", e.Value, z);
            };

            p.OnChange += act;

            a = 1;
            p.OnChange += act;
            p.OnChange += act;

            //p.OnChange -= (sender, e)
            //    => Console.WriteLine("Event raised: {0}", e.Value);

            p.Raise();
        }

        public void DoTest()
        {
            try
            {
                Console.WriteLine("try");
            }
            catch (Exception e)
            {

            }
            finally
            {
                Console.WriteLine("finally");

            }
            try
            {
                Console.WriteLine("try");
            }
            catch (Exception e)
            {

            }
            finally
            {
                Console.WriteLine("finally");

            }
            //CreateAndRaise();
        }
    }
}
