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
            choice = Console.ReadLine();

            while (choice.Contains("Exit") == false)
            {
                //----------------------------------------TALKBACKHELLO-----------------------------------------------------------//
                if (choice.Contains("Hello") == true && choice.Contains("TalkBack") == true)
                {
                    Console.WriteLine("Please wait....");
                    //choice = Console.ReadLine();
                    Class.Tasks.TalkbackHello().Wait();
                    choice = Console.ReadLine();
                }
                //--------------------------------------TALKBACKSORT------------------------------------------------------------//
                else if (choice.Contains("Sort") == true)
                {
                    Console.WriteLine("Please wait....");
                    string manipulate = "";
                    string[] tokens = choice.Split('[');
                    int remover = tokens[1].LastIndexOf(']');
                    manipulate = tokens[1].Remove(remover, 1);
                    string tokens1 = manipulate.Replace(",", "&num=");
                    Class.Tasks.TalkbackSort(tokens1).Wait();
                    choice = Console.ReadLine();
                }
               
                //-------------------------------------GETUSER-----------------------------------------------------------------//
                else if (choice.Contains("Get") == true && choice.Contains("User") == true)
                {
                    string[] tokens = choice.Split(' ');
                    if (tokens.Length < 3)
                    {
                        Console.WriteLine("Please enter your name and try again");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Please wait....");

                        Class.Tasks.TalkbackGetUsr(tokens[2]).Wait();
                        user.user_name = tokens[2];
                        choice = Console.ReadLine();
                    }
                   
                }
                //-------------------------------------POSTUSER---------------------------------------------------------------//
                else if (choice.Contains("Post") == true && choice.Contains("User") == true)
                {
                    Console.WriteLine("Please wait....");
                    string json = user.user_name;
                    Class.Tasks.TalkbackPostGetUsr(json).Wait();
                    user.api_key = Class.Tasks.return_api();
                    choice = Console.ReadLine();
                }
                //------------------------------------DELETEUSER-------------------------------------------------------------//
                else if (choice.Contains("Delete") == true && choice.Contains("User") == true)
                {
                    if (Class.Tasks.api_key == null || Class.Tasks.api_key == "")
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Please wait....");
                        user.user_name = "amples";
                        string json = user.user_name;
                        Class.Tasks.TalkbackDeleteUser(json).Wait();
                        choice = Console.ReadLine();
                    }
                   
                }
                //-----------------------------------PROTECTED HELLO--------------------------------------------------------//
                else if (choice.Contains("Protected") == true && choice.Contains("Hello") == true)
                {
                    if (Class.Tasks.api_key == null || Class.Tasks.api_key == "")
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Please wait....");
                        Class.Tasks.TalkbackProtectedHello().Wait();
                        choice = Console.ReadLine();
                    }
                   
                }
                //-----------------------------------PROTECTEDSHA1----------------------------------------------------------//
                else if (choice.Contains("Protected") == true && choice.Contains("SHA1") == true)
                {
                    if (Class.Tasks.api_key == null || Class.Tasks.api_key == "")
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Please wait....");
                        string[] tokens = choice.Split(' ');
                        Class.Tasks.TalkbackProtectedSHA1(tokens[2]).Wait();
                        choice = Console.ReadLine();
                    }
                }
                //------------------------------protected256---------------------------------------------------------------//
                else if (choice.Contains("Protected") == true && choice.Contains("SHA256") == true)
                {
                    if (Class.Tasks.api_key == null || Class.Tasks.api_key == "")
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Please wait....");
                        string[] tokens = choice.Split(' ');
                        Class.Tasks.TalkbackProtectedSHA256(tokens[2]).Wait();
                        choice = Console.ReadLine();
                    }
                    
                }
                //------------------------------PUBLIC KEY-----------------------------------------------------------------//
                else if (choice.Contains("Protected") == true && choice.Contains("Get") == true)
                {
                    if (Class.Tasks.api_key == null || Class.Tasks.api_key == "")
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Please wait....");
                        Class.Tasks.TalkbackProtectedPublic_key().Wait();
                        choice = Console.ReadLine();
                    }
                   
                }
                //--------------------------------PRIVATE SIGNED--------------------------------------//
                else if(choice.Contains("Protected")==true && choice.Contains("Sign") == true)
                {
                    if (Class.Tasks.api_key == null || Class.Tasks.api_key == "")
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else if ((Class.Tasks.pKey == null || Class.Tasks.pKey == ""))
                    {
                        Console.WriteLine("Client doesn’t yet have the public key");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        //REMEMBER TO GET PUBLIC KEY FIRST
                        Console.WriteLine("Please wait....");
                        string[] tokens = choice.Split(' ');
                        Class.Verify verify = new Class.Verify();
                        Class.Tasks.TalkbackProtectedPrivate_key(tokens[2]).Wait();
                        byte[] data = verify.string_to_ascii(tokens[2]); //original data
                        string data2 = Class.Tasks.Data_verify; //Signed data from server
                        byte[] data2_1 = verify.StringToByteArray(data2);//Signed data from server in bytes
                        string data3 = Class.Tasks.pKey; //server public key
                        string confirm = verify.VerifySignedHash(data, data2_1, data3);
                        Console.WriteLine("Confirmation: " + confirm);
                        choice = Console.ReadLine();
                    }
                   
                }
                //-----------------------------ADD FIFTEA------------------------------------------------//
                else if(choice.Contains("Add")==true && choice.Contains("Fifty") == true)
                {
                    if((Class.Tasks.api_key == null || Class.Tasks.api_key == ""))
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else if((Class.Tasks.pKey == null || Class.Tasks.pKey == ""))
                    {
                        Console.WriteLine("Client doesn’t yet have the public key" );
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Class.Verify verify = new Class.Verify();
                        string[] tokens = choice.Split(' ');
                        string server_public = Class.Tasks.pKey;//server_public public key
                                                                //convert original into into a byte array first
                        byte[] data = verify.string_to_ascii(tokens[2]); //original data
                        //--------------------------------------AES IV AND KEY---------------------------------------------------------------------
                        AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
                        aesProvider.GenerateKey();
                        aesProvider.GenerateIV();
                        byte[] aes_key = aesProvider.Key;
                        byte[] aes_initVector = aesProvider.IV;
                        //--------------------------------ENCRYPT using key-------------------------------------------------------------------
                        byte[] encrpted_data = verify.EncryptWithPublicKey(data, server_public);
                        byte[] encrypted_aes_key = verify.EncryptWithPublicKey(aes_key, server_public);
                        byte[] encrypt_aes_iv = verify.EncryptWithPublicKey(aes_initVector, server_public);
                        //----------------------------------------Convert to hex-------------------------------------------------------------------
                        string hex_data = verify.ByteArrayToHexString(encrpted_data);
                        string hex_aes_key = verify.ByteArrayToHexString(encrypted_aes_key);
                        string hex_aes_iv = verify.ByteArrayToHexString(encrypt_aes_iv);
                        //--------------------------------------putting it 2getha---------------------------------------------------------------------
                        string encrpted_sum = hex_data + "&encrpted_message=" + hex_aes_key + "&encrpted_message=" + hex_aes_iv;
                        //-------------------------------------sending it over to server----------------------------------------------------------------
                        Class.Tasks.TalkbackSendEncryptedData(encrpted_sum, aes_key, aes_initVector).Wait();
                        //Console.WriteLine(encrpted_sum);
                        choice = Console.ReadLine();
                    }
                   
                }
                //---------------------------CHANGE ROLE------------------------------------------------------//
                else if(choice.Contains("User")==true && choice.Contains("Role") == true)
                {
                    if(Class.Tasks.api_key == null || Class.Tasks.api_key == "")
                    {
                        Console.WriteLine("You need to do a User Post or User Set first");
                        choice = Console.ReadLine();
                    }
                    else
                    {
                        Class.Tasks.TalkbackChangeRole("pyro", "user").Wait();
                        choice = Console.ReadLine();
                    }
                   
                }
                //----------------------------------USER SET CLIENT FUNCTIONALITY------------------------------------//
                else if(choice.Contains("User")==true && choice.Contains("Set") == true)
                {

                }
                else
                {
                    Console.WriteLine("BAD COMMAND");
                    choice = Console.ReadLine();
                }
            }
        }
    }
}
