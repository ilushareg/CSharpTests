using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Security.AccessControl;
using System.Net;
using System.Net.Http;

namespace CSharpTests
{

    public class FileTests
    {
        public FileTests()
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

        public async Task<string> ReadAsyncHttpRequest()
        {
            HttpClient client = new HttpClient();
            Thread.Sleep(2000);
            string result = await client.GetStringAsync("http://www.microsoft.com");
            Thread.Sleep(2000);
            return result;
        }

        public void DoTest()
        {
            Task<string> t = ReadAsyncHttpRequest();
            while (TaskStatus.Running == t.Status)
            {
                Console.WriteLine("...");
                Thread.Sleep(0);
            }
            Console.WriteLine("Done");//t.Result

            //WebRequest request = WebRequest.Create("http://www.microsoft.com");
            //using (WebResponse response = request.GetResponse())
            //{ 
            //    StreamReader responseStream = new StreamReader(response.GetResponseStream());
            //    string responseText = responseStream.ReadToEnd();
            //    Console.WriteLine(responseText); // Displays the HTML of the website
            //    response.Close();
            //}
        }
    }
}
