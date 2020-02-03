using System;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Program
    {
        public static TcpListener listener; //server(needs to be assigned port and ip)
        public static void startServer()
        {
            Decryptor decryptor;
            Console.Write("Initializing...");
            int port = 43;
            IPAddress local = IPAddress.Parse("127.0.0.1"); //my ip
            Socket server_socket; //socket is linked with ip
            listener = new TcpListener(local, port);
            listener.Start();
            Console.WriteLine("Server Active.");

            while (true)
            {
                try
                {
                    server_socket = listener.AcceptSocket(); //allows server to connect
                    decryptor = new Decryptor();
                    decryptor.decrypt_client(server_socket);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Unhandled Exception: " + ex);
                }
            }
        }
        class Decryptor
        {
            public void decrypt_client(Socket socket)
            {
                NetworkStream server_stream;
                server_stream = new NetworkStream(socket);
                try
                {
                    Console.WriteLine("Connection to client Established.");
                    StreamWriter sw = new StreamWriter(server_stream);
                    StreamReader sr = new StreamReader(server_stream);
                    string line;
                    line = sr.ReadLine(); 
                    Console.WriteLine("Response: " + line);
                    sw.WriteLine("Received");
                    sw.Flush();
                        }
                catch (Exception ex)
                {
                    Console.WriteLine("Unhandled exception: " + ex);
                }
            }
        }

        static void Main(string[] args)
        {
           
            startServer();
            
        }

          
        
    }
}
