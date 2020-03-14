using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            TaskAsync().Wait();
            Console.ReadKey();
        }

        static async Task TaskAsync()
        {
            client.BaseAddress = new Uri("http://localhost:53164/");
            try
            {
                Task<string> task = GetStringAsync("/api/Translator/GetInt/7"); // GET COMMAND
                if (await Task.WhenAny(task, Task.Delay(2000)) == task) //DELAY
                    Console.WriteLine(task.Result); //Carry out task??...
                else
                    Console.WriteLine("Request timed out");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        //localhost: 53164 /
        }
        static async Task<string>GetStringAsync(string path)
        {
            string response_str = "";
            HttpResponseMessage response = await client.GetAsync(path);
            response_str = await response.Content.ReadAsStringAsync();
            return response_str; //GRAB STRING FROM SERVER
        }
    }
}
