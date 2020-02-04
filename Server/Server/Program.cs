using System;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Text;

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
            public byte[] string_to_ascii(string Message)  //just takes string and converts to ascii
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                Byte[] encodedBytes = ascii.GetBytes(Message);

                return encodedBytes;
            }
            public string encrypted_ascii_to_string(Byte []bytes)
            {
                string asciiString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Console.WriteLine(asciiString);
                return asciiString;
            }
            public void decrypt_client(Socket socket)
            {
                NetworkStream server_stream;
                server_stream = new NetworkStream(socket);
                Console.WriteLine("Connection to client Established.");
                while (true) {
                    try
                    {
                        //int encryption_key = 0;
                        StreamWriter sw = new StreamWriter(server_stream);
                        StreamReader sr = new StreamReader(server_stream);
                        string line;
                        line = sr.ReadLine();
                        Byte[] ascii_bytes = string_to_ascii(line);
                        //Console.WriteLine("Response: ");
                        //foreach (Byte b in ascii_bytes)
                        //{
                        //    Console.Write("[{0}]", (b + 2));
                        //}
                        sw.WriteLine("Received");
                        sw.WriteLine("Encrypted words: ");
                        foreach (Byte b in ascii_bytes)
                        {
                            sw.Write("[{0}]", (b + 2));
                        }
                        sw.Flush();
                        if(line == "decrypt")
                        {
                           sw.WriteLine("Decrypting...");
                           line =  encrypted_ascii_to_string(ascii_bytes);
                           sw.WriteLine(line);
                           sw.Flush();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unhandled exception: " + ex);
                    }
                }
               
            }
        }

        static void Main(string[] args)
        {
           
            startServer();
            
        }

          
        
    }
}
