using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CSharpTests
{

    public class ConditionalTests
    {
        public ConditionalTests()
        {
        }

        [Conditional("ASD")]
        private static void Log(string message)
        {
            Console.WriteLine(message);
        }

        string PrepareString()
        {
            Console.WriteLine("preparing string");
            return "aaaaa";
        }

        public void DoTest()
        {
            Log(PrepareString());

            double cost = 1234;
            TestToString tts = new TestToString();
            cost.ToString("C", new System.Globalization.CultureInfo("en - US"));

            Console.WriteLine(tts);


        }
    }
}
