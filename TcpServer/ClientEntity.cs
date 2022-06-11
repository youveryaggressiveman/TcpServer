using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class ClientEntity
    {
        public TcpClient _client;

        public string LastMessage { get; set; }

        public ClientEntity(TcpClient client)
        {
            _client = client;
        }

        public void Process()
        {
            NetworkStream networkStream = null;

            try
            {
                networkStream = _client.GetStream();
                

                byte[] data = new byte[64];

                while (true)
                {
                    StringBuilder builder = new();
                    int bytes = 0;
                    do
                    {
                        bytes = networkStream.Read(data, 0, data.Length);
                        builder.AppendLine(Encoding.Unicode.GetString(data, 0, data.Length));
                    }
                    while (networkStream.DataAvailable);

                    LastMessage = builder.ToString();
                }

                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                networkStream?.Close();
                _client?.Close();
            }
        }
    }
}
