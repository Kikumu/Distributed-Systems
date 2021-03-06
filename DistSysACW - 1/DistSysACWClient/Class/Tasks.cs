﻿using System;
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
        public static string Data_verify;
        public static string pKey;
        public static string api_key = "";
        //-----------------------------------------------------------------------TALKBACK HELLO---------------------------------------------------------------------------//
        public static async Task<string> TalkbackHello()
        {
           // client.BaseAddress = new Uri("https://localhost:44307/");
                HttpRequestMessage httpRequest = new HttpRequestMessage();
                httpRequest.RequestUri =new Uri("https://localhost:44307/api/talkback/hello");
                httpRequest.Method = HttpMethod.Get;
                httpRequest.Headers.Add("apikey", return_api());       //for authorization
                HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
                string resp = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine(resp);
                return resp;
        }
        
        //-----------------------------TALKBACKSORT METHOD-------------------------------------------------------------//
        public static async Task<string> TalkbackSort(string tst)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/talkback/sort?num=" + tst);
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());        //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
            return resp;

        }
        //-----------------------------GETUSR METHOD-------------------------------------------------------------//
        public static async Task<string> TalkbackGetUsr(string tst)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/user/new?username=" + tst);
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());   //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
            return resp;
        }
        //--------------------------POSTUSR------------------------------------------------------------------//
        public static async Task<string> TalkbackPostGetUsr(string tst)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/user/new");
            httpRequest.Method = HttpMethod.Post;
            var stringContent = new StringContent(JsonConvert.SerializeObject(tst), Encoding.UTF8, "application/json");   //conversion to json string
            httpRequest.Content = stringContent;
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            if (resp == "Oops. Make sure your body contains a string with your username and your Content-Type is Content-Type:application/json" || resp== "Oops. This username is already in use. Please try again with a new username.")
            {
                Console.WriteLine(resp);
            }
            else
            {
                api_key = resp;
                resp = "Got API Key";
                Console.WriteLine(resp);
            }
            return resp;

        }
        //-------------------------DELETEUSER-------------------------------------------------------------------//
        public static async Task<string>TalkbackDeleteUser(string tst)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/user/removeuser?username="+tst);
            httpRequest.Method = HttpMethod.Delete;
            httpRequest.Headers.Add("apikey", return_api());      //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
            return resp;
        }
        //------------------------------------------PROTECTEDHELLO-------------------------------------------------------------------//
        public static async Task<string> TalkbackProtectedHello()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/protected/hello");
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());      //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
            return resp;
        }
        //------------------------------------protectedsha1-----------------------------------------------------------------------------------//
        public static async Task<string> TalkbackProtectedSHA1(string tst)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/protected/sha1?message="+tst);
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());      //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
            return resp;
        }
        //----------------------------------------PROTECTED256------------------------------------------------------------------------------------//
        public static async Task<string> TalkbackProtectedSHA256(string tst)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/protected/sha256?message=" + tst);
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());      //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
            return resp;
        }
        //-----------------------------------------RETURNPUBLICKEY------------------------------------------------------------------------------//
        public static async Task<string> TalkbackProtectedPublic_key()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/protected/GetPublickey");
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());      //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Got Public Key");
            pKey = resp;
            return resp;
        }
        //------------------------------------------SIGHWITHPRIVATEKEY------------------------------------------//
        public static async Task<string> TalkbackProtectedPrivate_key(string tst)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/protected/sign?message="+tst);
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());      //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            if (resp != "Bad Request")
            {
                Console.WriteLine("Message successfully signed");
            }  
            else
                Console.WriteLine("Message was not successfully signed");
            Data_verify = resp;
            return resp;
        }
        //----------------------------------------ROLE CHANGE--------------------------------------------------------------------//
        public static async Task<string>TalkbackChangeRole(string name, string role)
        {
            string usrnme = name;
            string usrole = role;
            Class.ObjectJsonSerialiser objectJson = new Class.ObjectJsonSerialiser();
            objectJson.name = usrnme;
            objectJson.role = usrole;
            string z = JsonConvert.SerializeObject(objectJson);
            z = JsonConvert.SerializeObject(z);
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/user/ChangeRole");
            httpRequest.Method = HttpMethod.Post;
            httpRequest.Headers.Add("apikey", return_api());
            var stringContent = new StringContent(z, Encoding.UTF8, "application/json");   //conversion to json string
            httpRequest.Content = stringContent;
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            Console.WriteLine(resp);
            return resp;
        }
        //----------------------------------------------------SEND ENCRYPTED MESSAGE TO SERVER----------------------------------------------------
        public static async Task<string> TalkbackSendEncryptedData(string tst,byte[]decrypt_aes,byte[]decrypt_iv)
        {
            AES_functions.AES_E_D aES_Functions = new AES_functions.AES_E_D();
            Class.Verify verify = new Class.Verify();
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri("https://localhost:44307/api/protected/AddFifty?encrpted_message=" + tst);
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("apikey", return_api());        //for authorization
            HttpResponseMessage httpResponse = await client.SendAsync(httpRequest);
            string resp = await httpResponse.Content.ReadAsStringAsync();
            if (resp != "Bad Request")
            {
                //Console.WriteLine("Encrypted string:" + resp);
                byte[] encrypted_data = verify.StringToByteArray(resp);
                string aes_decrypted_data = aES_Functions.DecryptStringFromBytes_Aes(encrypted_data, decrypt_aes, decrypt_iv);
                Console.WriteLine("Decrypted integer which should be " + aes_decrypted_data+ " in this example");
            }
            else
                Console.WriteLine("An error occurred!");
            return resp;
        }
        //------------------------------------------------JUST RETURNS APPI KEY---------------------------------------------------------------//
        public static string return_api()
        {
            return api_key;
        }
    }
}
