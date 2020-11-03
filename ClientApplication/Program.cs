using System;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
 
namespace ClientApplication
{

    class Program
    {
        private static string Ping(ref Socket socket)
        {
            string message = "Host pinged you";
            DateTime pingTime = DateTime.Now;
            string pingTimeStr = " " + pingTime.ToString("hh:mm:ss:fff");
            message += pingTimeStr;
            //byte[] data = Encoding.Unicode.GetBytes(message);

            byte[] data = new byte[AMOUNT_OF_BYTES_IN_SOCKET_DATA];
            for (int i = 0; i < AMOUNT_OF_BYTES_IN_SOCKET_DATA; i++)
            {
                data[i] = 255;
            }
            
            socket.Send(data);
            
            data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
 
            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);

            return builder.ToString();
        }
        
        static int port = 1025 + 21;
        static string address = "127.0.0.1";

        private const int INTERVAL_BETWEEN_PINGS = 1000;
        private const byte AMOUNT_OF_BYTES_IN_SOCKET_DATA = 255;
        
        static void Main(string[] args)
        {
            //DateTime date1 = new DateTime();
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                string command = "";

                while (command != "stop")
                {
                    Console.Write("Введите комманду:");
                    command = Console.ReadLine();
                    switch (command)
                    {
                        case "ping":
                        {
                            Console.WriteLine("!Write \"stop\" to stop pinging!");
                            System.Threading.Thread.Sleep(1500);
                            Console.Write("STARTING PINGING");
                            System.Threading.Thread.Sleep(500);
                            Console.Write(".");
                            System.Threading.Thread.Sleep(500);
                            Console.Write(".");
                            System.Threading.Thread.Sleep(500);
                            Console.WriteLine(".");

                            string stopCommand = "";
                            while (stopCommand != "stop")
                            {
                                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                socket.Connect(ipPoint);
                            
                                DateTime MessageSendingTime = DateTime.Now;
                                //Console.WriteLine("ответ сервера: " + );
                                Ping(ref socket);
                                DateTime MessageReceivingTime = DateTime.Now;
                                
                                System.Threading.Thread.Sleep(INTERVAL_BETWEEN_PINGS);
                        
                                TimeSpan PingTime = MessageReceivingTime - MessageSendingTime;
                                Console.WriteLine("время ответа: {0} ms", PingTime.Seconds*1000 + PingTime.Milliseconds);
                                Console.WriteLine("___________________________");
                            
                                socket.Shutdown(SocketShutdown.Both);
                                socket.Close();

                                //stopCommand = Console.ReadLine();
                            }
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Unknown command");
                            break;
                        }
                    }
                }
                
            }
            catch(Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}