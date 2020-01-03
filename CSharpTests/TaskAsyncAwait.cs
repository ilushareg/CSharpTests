using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTests
{
    public class TestAsyncAwait
    {
        public TestAsyncAwait()
        {
            
        }
        public void DoTest()
        {
            Task<string> t = DownloadContent();
            Console.WriteLine("Doing some work");

            string result = t.Result;
            Console.WriteLine("result received bool = {0}", t.IsCompletedSuccessfully);
            //Console.WriteLine(result);

            ConcurrentBag<int> a;

            var numbers = Enumerable.Range(0, 10000000);
            var parallelResult = numbers.AsParallel().AsOrdered()
                .Where(i => i % 2 == 0)
                .ToArray();
            Console.WriteLine("waiting");
            foreach (int i in parallelResult)
            {
                int q = i;
            }

        }
        public static async Task<string> DownloadContent()
        {
            using (HttpClient client = new HttpClient())
            {
                Console.WriteLine("Task started");
                Thread.Sleep(500);
                string result = await client.GetStringAsync("http://www.microsoft.com");
                Console.WriteLine("result received");
                return result;
            }
        }

    }
}
