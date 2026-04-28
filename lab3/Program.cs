using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

class Program
{
    static string customDnsServer = null;

    static void Main()
    {
        Console.WriteLine("DNS Client App");
        Console.WriteLine("Comenzi:");
        Console.WriteLine("resolve <domain/ip>");
        Console.WriteLine("use dns <ip>");
        Console.WriteLine("exit");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input.ToLower() == "exit")
                break;

            var parts = input.Split(' ');

            if (parts.Length < 2)
            {
                Console.WriteLine("Comandă invalidă.");
                continue;
            }

            if (parts[0].ToLower() == "resolve")
            {
                Resolve(parts[1]);
            }
            else if (parts[0].ToLower() == "use" && parts.Length == 3 && parts[1].ToLower() == "dns")
            {
                SetDns(parts[2]);
            }
            else
            {
                Console.WriteLine("Comandă necunoscută.");
            }
        }
    }

    static void SetDns(string ip)
    {
        if (!IPAddress.TryParse(ip, out _))
        {
            Console.WriteLine("DNS invalid.");
            return;
        }

        customDnsServer = ip;
        Console.WriteLine($"DNS setat la: {ip}");
    }

    static void Resolve(string input)
    {
        if (IPAddress.TryParse(input, out IPAddress ip))
        {
            ReverseLookup(ip);
        }
        else
        {
            ForwardLookup(input);
        }
    }

    static void ForwardLookup(string domain)
    {
        try
        {
            if (customDnsServer == null)
            {
                var addresses = Dns.GetHostAddresses(domain);
                Console.WriteLine("IP-uri găsite:");
                foreach (var addr in addresses)
                {
                    Console.WriteLine(addr);
                }
            }
            else
            {
                var result = QueryDns(domain, customDnsServer);
                if (result.Count == 0)
                {
                    Console.WriteLine("Nu s-au găsit IP-uri.");
                }
                else
                {
                    Console.WriteLine("IP-uri găsite:");
                    foreach (var ip in result)
                    {
                        Console.WriteLine(ip);
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Nu s-a putut rezolva domeniul.");
        }
    }

    static void ReverseLookup(IPAddress ip)
    {
        try
        {
            var entry = Dns.GetHostEntry(ip);
            Console.WriteLine("Domenii găsite:");
            Console.WriteLine(entry.HostName);
        }
        catch
        {
            Console.WriteLine("Nu s-a putut rezolva IP-ul.");
        }
    }

    // DNS query manual (simplificat)
    static List<string> QueryDns(string domain, string dnsServer)
    {
        List<string> result = new List<string>();

        try
        {
            UdpClient client = new UdpClient();
            client.Connect(dnsServer, 53);

            byte[] request = BuildDnsRequest(domain);
            client.Send(request, request.Length);

            var remoteEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] response = client.Receive(ref remoteEP);

            result = ParseDnsResponse(response);
        }
        catch
        {
            Console.WriteLine("Eroare la conectarea cu DNS server.");
        }

        return result;
    }

    static byte[] BuildDnsRequest(string domain)
    {
        Random rand = new Random();
        ushort id = (ushort)rand.Next(ushort.MaxValue);

        List<byte> request = new List<byte>();

        // Header
        request.AddRange(BitConverter.GetBytes((ushort)IPAddress.HostToNetworkOrder((short)id)));
        request.AddRange(new byte[] { 0x01, 0x00 }); // flags
        request.AddRange(new byte[] { 0x00, 0x01 }); // questions
        request.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

        // Question
        foreach (var part in domain.Split('.'))
        {
            request.Add((byte)part.Length);
            request.AddRange(Encoding.ASCII.GetBytes(part));
        }

        request.Add(0x00); // end
        request.AddRange(new byte[] { 0x00, 0x01 }); // type A
        request.AddRange(new byte[] { 0x00, 0x01 }); // class IN

        return request.ToArray();
    }

    static List<string> ParseDnsResponse(byte[] response)
    {
        List<string> ips = new List<string>();

        int answerCount = response[7];
        int index = 12;

        // skip question
        while (response[index] != 0)
        {
            index += response[index] + 1;
        }

        index += 5;

        for (int i = 0; i < answerCount; i++)
        {
            index += 2; // name
            index += 2; // type
            index += 2; // class
            index += 4; // ttl

            int dataLength = (response[index] << 8) | response[index + 1];
            index += 2;

            if (dataLength == 4)
            {
                string ip = $"{response[index]}.{response[index + 1]}.{response[index + 2]}.{response[index + 3]}";
                ips.Add(ip);
            }

            index += dataLength;
        }

        return ips;
    }
}