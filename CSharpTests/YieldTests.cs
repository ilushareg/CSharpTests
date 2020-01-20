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

    public class YieldTests
    {
        class TestEntity
        {
            static int count = 0;
            int curr = 0;

            public TestEntity()
            {
                curr = count++;
            }
            public void Touch()
            {
                Console.WriteLine($"count = {curr}");
            }
        }

        class TestArray: IEnumerable<TestEntity>
        {
            TestEntity[] entityArr;

            public TestArray()
            {
                entityArr = new TestEntity[10];
                for(int i=0; i<entityArr.Length; i++)
                {
                    entityArr[i] = new TestEntity();
                }
            }

            public IEnumerator<TestEntity> GetEnumerator()
            {   
                int k = 0;
                while(k++<100)
                {
                    yield return new TestEntity();
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public YieldTests()
        {

        }

        public void DoTest()
        {
            TestArray a = new TestArray();
            foreach(TestEntity t in a)
            {
                t.Touch();
            }
        }

    }
}
