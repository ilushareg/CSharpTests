using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;
using System.Collections.Generic;
using System.Collections;

namespace CSharpTests
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class MyMethodAndParameterAttribute : Attribute
    {
        public MyMethodAndParameterAttribute(string cat, string value)
        {
            Description = cat + " " + value;
            Console.WriteLine(Description);
        }
        public string Description { get; set; }

    }

    public class CategoryAttribute : MyMethodAndParameterAttribute
    {
        public CategoryAttribute(string value)
            : base("Category", value)
        { }
    }
    public class UnitTestAttribute : CategoryAttribute
    {
        public UnitTestAttribute()
            : base("Unit Test")
        { }
    }

    public class AttributeTests
    {
        [Serializable]
        [CategoryAttribute("test")]
        class TestSer
        {
            public TestSer()
            {

            }
            public string FirstName { get; set; }
            public string LastName { get; set; }

        }
        //SerializableAttribute

        public AttributeTests()
        {

        }

        [UnitTest]
        public void MySecondUnitTest()
        { }

        public void DoTest()
        {
            int i=0;
            System.Type type = i.GetType();

            TestSer ts = new TestSer();

        }

    }
}
