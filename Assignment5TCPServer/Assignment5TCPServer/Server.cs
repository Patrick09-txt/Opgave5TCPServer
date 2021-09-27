using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ModelLib.model;

namespace Assignment5TCPServer
{
    public class Server
    {
        public static List<FootballPlayer> footballPlayers = new List<FootballPlayer>()
        {
            new FootballPlayer(1, "Christian Eriksen", 100, 23),
            new FootballPlayer(2, "Kasper Dolberg", 150, 10),
            new FootballPlayer(3, "Simon Kjær", 50, 4),
            new FootballPlayer(4, "Mikkel Damsgaard", 125, 7),
            new FootballPlayer(5, "Joakim Mæhle", 80, 5),
            new FootballPlayer(6, "Kasper Schmeichel", 95, 1)
        };

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

                sw.WriteLine("Please choose an input: \n" + 
                             "'HentAlle' \n" + 
                             "'Hent' [Enter] '{id}' \n" +
                             "'Gem' [Enter] '{Save Object as Json}'");

                String playerString1 = sr.ReadLine().ToLower();
                if (playerString1 == null)
                {
                    sw.WriteLine("Invalid input!");
                    return;
                }

                String playerString2 = sr.ReadLine();
                if (playerString1 != "hentalle" && playerString2 == null)
                {
                    sw.WriteLine("Invalid input!");
                    return;
                }

                if (playerString1 == "hentalle")
                {
                    foreach (FootballPlayer footballPlayer in footballPlayers)
                    {
                      sw.WriteLine(JsonSerializer.Serialize(footballPlayer));  
                    }
                }

                else if (playerString1 == "hent")
                {
                    int id = Convert.ToInt32(playerString2);
                    foreach (FootballPlayer footballPlayer in footballPlayers)
                    {
                        if (footballPlayer.Id == id)
                        {
                            sw.WriteLine(JsonSerializer.Serialize(footballPlayer));
                            return;
                        }
                    }

                    return;
                }

                else if (playerString1 == "gem")
                {
                    FootballPlayer newFootballPlayer = JsonSerializer.Deserialize<FootballPlayer>(playerString2);
                    if (newFootballPlayer != null)
                    {
                        footballPlayers.Add(newFootballPlayer);
                    }
                }
            }
            socket?.Close();
        }
    }
}
