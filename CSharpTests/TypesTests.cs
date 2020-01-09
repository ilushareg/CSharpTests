using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;
using System.Collections.Generic;

namespace CSharpTests
{
    internal sealed class TestClass
    {
        public TestClass() { }
        public int SomeNum() { return 10; }
    }
    internal static class TestClassExtension
    {
        public static int SomeOtherNum(this TestClass t, float multiplier)
        {
            return (int)(t.SomeNum() * multiplier);
        }
    }


    public class TypesTests
    {
        static void DisplayInExcel(IEnumerable<dynamic> entities)
        {
         }


        class MyClass<T>
            where T : new()
        {
            public MyClass()
            {
                MyProperty = new T();
                T defaultValue = default(T);

                Console.WriteLine($"(byte){defaultValue}");
            }
            T MyProperty { get; set; }
        }

        public TypesTests()
        {
            
        }

        internal enum Days : byte
        {
            one = 1,
            two,
            three
        };

        public void DoTest()
        {
            Nullable < int > testNull = null;
            testNull = 1;
            int? tn = null;
            tn = 2;


            TestClass tc = new TestClass();
            int r = tc.SomeOtherNum(5);

            new MyClass<float>();
            new MyClass<int>();
            new MyClass<TestClass>();

            Console.WriteLine($"{Days.two} = {(byte)Days.two}");
        }

    }
}
