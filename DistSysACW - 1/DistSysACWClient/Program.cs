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

namespace DistSysACWClient
{
    class Client
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            List<int> SortValues = new List<int>();

            string choice = "";
            Console.WriteLine("Hello. What would you like to do?");
            Console.WriteLine();
            choice = Console.ReadLine();
            //----------------------------------------TALKBACKHELLO-----------------------------------------------------------//
            if(choice.Contains("Hello")== true && choice.Contains("TalkBack") == true)
            {
                TalkbackHello().Wait();
            }
            //--------------------------------------TALKBACKSORT------------------------------------------------------------//
            else if(choice.Contains("Sort") == true)
            {
                string manipulate = "";
                string[]tokens = choice.Split('[');
                int remover = tokens[1].LastIndexOf(']');
                manipulate = tokens[1].Remove(remover,1);
                string tokens1 = manipulate.Replace("," ,"&num=");
                TalkbackSort(tokens1).Wait();
            }
            //-------------------------------------GETUSER-----------------------------------------------------//
            else if (choice.Contains("Get") == true)
            {

            }

            
            //Console.ReadKey();
        }
        //-------------------------------------TALKBACK HELLO METHOD----------------------------------------------------------//
        static async Task TalkbackHello()
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
        static async Task TalkbackSort(string tst)
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
            catch(Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }
        //-----------------------------GETUSR METHOD-------------------------------------------------------------//
        static async Task TalkbackGetUsr(string tst)
        {
            client.BaseAddress = new Uri("https://localhost:44307/");
            try
            {
                string combine = "api/user/new?"+tst;
                Task<string> task = GetStringAsync(combine); // GET COMMAND
                if (await Task.WhenAny(task, Task.Delay(2000)) == task) //DELAY
                    Console.WriteLine(task.Result); //Carry out task??...
                else
                    Console.WriteLine("Request timed out");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error found: " + ex);
            }
        }

        static async Task<string> GetStringAsync(string path)
        {
            string response_str = "";
            HttpResponseMessage response = await client.GetAsync(path);
            response_str = await response.Content.ReadAsStringAsync();
            return response_str; //GRAB STRING and put in body of client
        }
    }
}
