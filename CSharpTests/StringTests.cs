using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CSharpTests
{
    public class TestToString
    {
        public TestToString()
        {

        }
        //public override string ToString() { return "TestToString qweasd"; }

        public string ToString(string format)
        {
            return format + " qweasd";
        }

    }
    public static class ToStringExtension
    {
        public static string ToString(TestToString t)
        {
            return "asdasd";
        }
    }

    public class StringTests
    {
        public StringTests()
        {
        }
        public void DoTest()
        {
            double cost = 1234;
            TestToString tts = new TestToString();
            cost.ToString("C", new System.Globalization.CultureInfo("en - US"));

            Console.WriteLine(tts);


        }
    }
}
