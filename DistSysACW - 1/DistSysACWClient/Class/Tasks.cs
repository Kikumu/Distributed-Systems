using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
namespace DistSysACWClient.Class
{
    class Tasks
    {
        static HttpClient client = new HttpClient();
        // UserClass user = new UserClass();
        public static string api_key = "";
        //-----------------------------------------------------------------------TALKBACK HELLO---------------------------------------------------------------------------//
        public static async Task TalkbackHello()
        {
            client.BaseAddress = new Uri("https://localhost:44307/");
            try
            {
                
                Task<string> task = GetStringAsync("api/talkback/hello"); // GET COMMAND
                if (await Task.WhenAny(task, Task.Delay(3000)) == task) //DELAY
                    Console.WriteLine(task.Result); //Carry out task??...
                else
                    Console.WriteLine("Request timed out");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }
        
        //-----------------------------TALKBACKSORT METHOD-------------------------------------------------------------//
        public static async Task TalkbackSort(string tst)
        {
            client.BaseAddress = new Uri("https://localhost:44307/");
            try
            {

                string combine = "api/talkback/sort?num=" + tst;
                Task<string> task = GetStringAsync(combine); // GET COMMAND
                if (await Task.WhenAny(task, Task.Delay(3000)) == task) //DELAY
                    Console.WriteLine(task.Result); //Carry out task??...
                else
                    Console.WriteLine("Request timed out");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }
        //-----------------------------GETUSR METHOD-------------------------------------------------------------//
        public static async Task TalkbackGetUsr(string tst)
        {
            client.BaseAddress = new Uri("https://localhost:44307/");
            try
            {
                
                string combine = "api/user/new?name=" + tst;
                Task<string> task = GetStringAsync(combine); // GET COMMAND
                if (await Task.WhenAny(task, Task.Delay(4000)) == task) //DELAY
                {
                    //UserClass UserClass = new UserClass();
                    //UserClass.user_name = task.Result;
                    Console.WriteLine(task.Result); //Carry out task??...
                }
                    
                else
                    Console.WriteLine("Request timed out");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }
        //--------------------------POSTUSR------------------------------------------------------------------//
        public static async Task TalkbackPostGetUsr(string tst)
        {
            client.BaseAddress = new Uri("https://localhost:44307/");
            try
            {
                string combine = "api/user/new";
                Task<string> task = PostStringAsync(combine, tst); // GET COMMAND
                if (await Task.WhenAny(task, Task.Delay(4000)) == task) //DELAY
                {
                    api_key = task.Result;
                    Console.WriteLine(task.Result); //Carry out task??...
                }
                else
                    Console.WriteLine("Request timed out");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }
        //----------------------------------------------FOR SENDING GET REQUESTS----------------------------------------------------------//
        static async Task<string> GetStringAsync(string path)
        {
            string response_str = "";
            HttpResponseMessage response = await client.GetAsync(path);
            response_str = await response.Content.ReadAsStringAsync();
            return response_str; //GRAB STRING and put in body of client
        }
        //---------------------------------------------FOR SENDING POST REQUESTS-----------------------------------------------------------//
        static async Task<string> PostStringAsync(string path, string data)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(path, stringContent);
            string response_str = await response.Content.ReadAsStringAsync();
            return response_str; //GRAB STRING and put in body of client
        }
        //-------------------------------------------FOR SENDING DELETE REQUESTS---------------------------------------------------------//
      
        
        
        
        
        
        
        
        
        
        //----------------------------------------JUST RETURNS API--------------------------------------------------------------------//
        public static string return_api()
        {
            return api_key;
        }
    }
}
