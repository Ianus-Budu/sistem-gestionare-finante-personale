using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static int PORT = 5000;
    static string BROADCAST_IP = "255.255.255.255";

    static UdpClient client;

    static void Main()
    {
        Console.Write("Introdu numele tău: ");
        string username = Console.ReadLine();

        try
        {
            // Creare socket cu ReuseAddress pentru a permite mai multe instanțe
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Bind(new IPEndPoint(IPAddress.Any, PORT));

            client = new UdpClient();
            client.Client = socket;
            client.EnableBroadcast = true;

            // Thread separat pentru primirea mesajelor
            Thread receiveThread = new Thread(() => ReceiveMessages());
            receiveThread.Start();

            Console.WriteLine("Chat pornit!");
            Console.WriteLine("Comenzi:");
            Console.WriteLine("/all mesaj       -> trimite mesaj la toți");
            Console.WriteLine("/pm ip mesaj     -> mesaj privat");

            while (true)
            {
                string input = Console.ReadLine();

                if (input.StartsWith("/all "))
                {
                    string message = username + ": " + input.Substring(5);
                    SendBroadcast(message);
                }
                else if (input.StartsWith("/pm "))
                {
                    var parts = input.Split(' ', 3);
                    if (parts.Length < 3)
                    {
                        Console.WriteLine("Format: /pm IP mesaj");
                        continue;
                    }

                    string ip = parts[1];
                    string message = "(Privat) " + username + ": " + parts[2];
                    SendPrivate(ip, message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare: " + ex.Message);
        }
    }

    static void SendBroadcast(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(BROADCAST_IP), PORT);
            client.Send(data, data.Length, endPoint);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare trimitere broadcast: " + ex.Message);
        }
    }

    static void SendPrivate(string ip, string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), PORT);
            client.Send(data, data.Length, endPoint);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare trimitere mesaj privat: " + ex.Message);
        }
    }

    static void ReceiveMessages()
    {
        try
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, PORT);

            while (true)
            {
                byte[] data = client.Receive(ref remoteEP);
                string message = Encoding.UTF8.GetString(data);

                // afișează IP-ul expeditorului + mesajul
                Console.WriteLine($"\n[{remoteEP.Address}] {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare primire mesaj: " + ex.Message);
        }
    }
}