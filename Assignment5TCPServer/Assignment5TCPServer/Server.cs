using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5TCPServer
{
    public class Server
    {
        private const int PORT = 2121;
        public Server()
        {

        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();
            {
                while (true)
                {
                    TcpClient socket = listener.AcceptTcpClient();

                    Task.Run(
                        () =>
                        {
                            TcpClient tmpSocket = socket;
                            DoClient(tmpSocket);
                        }
                    );
                }
            }
        }

        private void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.AutoFlush = true;

                String playerString = sr.ReadLine();
                String playerString1 = sr.ReadLine();

                Console.WriteLine("Server have received : " + playerString);
                Console.WriteLine(playerString1);
            }
            socket?.Close();
        }
    }
}
