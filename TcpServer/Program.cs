using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpServer
{
    class Program
    {
        private static TcpListener s_tcpListener;
        private const int PORT = 6666;

        static void Main(string[] args)
        {
            try
            {
                s_tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), PORT);
                s_tcpListener.Start();

                Console.WriteLine("Я вас слушаю");
                while (true)
                {
                    TcpClient client = s_tcpListener.AcceptTcpClient();
                    ClientEntity clientEntity = new(client);
                    Thread clientThread = new(new ThreadStart(clientEntity.Process));
                    clientThread.Start();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
