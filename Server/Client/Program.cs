using System;
using System.IO;
using System.Net.Sockets;

namespace ConsoleApp1
{

    class Program
    {
        //public static byte[] Serialize(string Message, int EncryptionKey)
        //{
        //    byte[] rawdata;
        //    return rawdata;
        //}
        static void Main(string[] args)
        {
            string host_name;
            int port = 0;
            //string Message;

            Console.Write("Please enter IP/host address: ");
            host_name = Console.ReadLine();
            Console.Write("Please enter port number: ");
            try
            {
                port = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Please enter a number.");
            }
            TcpClient client = new TcpClient();
            try
            {
                string message;
                Console.WriteLine("Connecting...");
                client.Connect(host_name, port);
                StreamWriter sw = new StreamWriter(client.GetStream());
                StreamReader sr = new StreamReader(client.GetStream());
                Console.WriteLine("Connected to server");
                Console.WriteLine("To stop, enter 'no' when prompted to enter message");
                string exit = "y";
                while(exit == "y")
                {
                    Console.WriteLine("Please enter message: ");
                    message = Console.ReadLine();
                    sw.WriteLine(message);
                    sw.Flush();
                    string line = sr.ReadLine();
                    Console.WriteLine("Server response: " + line);
                    if (exit == "no")
                    {
                        exit = "no";
                    }
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }

           
        }
    }
}
