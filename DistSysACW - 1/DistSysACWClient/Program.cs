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

namespace DistSysACWClient
{
    class Client
    {
        static void Main(string[] args)
        {
            List<int> SortValues = new List<int>();
            Class.UserClass user = new Class.UserClass();
            string choice = "";
            Console.WriteLine("Hello. What would you like to do?");
            Console.WriteLine();
            choice = Console.ReadLine();
            //----------------------------------------TALKBACKHELLO-----------------------------------------------------------//
            if(choice.Contains("Hello")== true && choice.Contains("TalkBack") == true)
            {
                Class.Tasks.TalkbackHello().Wait();
            }
            //--------------------------------------TALKBACKSORT------------------------------------------------------------//
            else if(choice.Contains("Sort") == true)
            {
                string manipulate = "";
                string[]tokens = choice.Split('[');
                int remover = tokens[1].LastIndexOf(']');
                manipulate = tokens[1].Remove(remover,1);
                string tokens1 = manipulate.Replace("," ,"&num=");
                Class.Tasks.TalkbackSort(tokens1).Wait();
            }
            //-------------------------------------GETUSER-----------------------------------------------------//
            else if (choice.Contains("Get") == true)
            {
                string[] tokens = choice.Split(' ');
                Class.Tasks.TalkbackGetUsr(tokens[2]).Wait();
                user.user_name = tokens[2];
            }
            //-------------------------------------POSTUSER----------------------------------------------------//
            else if(choice.Contains("Post")==true && choice.Contains("User"))
            {
                string json = "king";
                Class.Tasks.TalkbackPostGetUsr(json).Wait();
                user.api_key = Class.Tasks.return_api();
            }
            //------------------------------------DELETEUSER-----------------------------------------------------//
            else if(choice.Contains("Delete")==true && choice.Contains("User"))
            {
                user.user_name = "amples";
                string json = user.user_name;
                Class.Tasks.TalkbackDeleteUser(json).Wait();
               
            }
            //-----------------------------------PROTECTED HELLO-----------------------------------------------//
            else if (choice.Contains("Protected") == true && choice.Contains("Hello"))
            {
                Class.Tasks.TalkbackProtectedHello().Wait();
            }
            //-----------------------------------PROTECTEDSHA1---------------------------------------------------//
            else if (choice.Contains("Protected") == true && choice.Contains("SHA1"))
            {
                string[] tokens = choice.Split(' ');
                Class.Tasks.TalkbackProtectedSHA1(tokens[2]).Wait();

            }
            //------------------------------protected256-----------------------------------------------------------//
            else if (choice.Contains("Protected") == true && choice.Contains("SHA256"))
            {
                string[] tokens = choice.Split(' ');
                Class.Tasks.TalkbackProtectedSHA256(tokens[2]).Wait();
            }
        }

    }
}
