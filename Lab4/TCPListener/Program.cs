﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPListener
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int recv;
            byte[] data= new byte[1024];
            TcpListener newsock = new TcpListener(9000);
            newsock.Start();
            Console.WriteLine("Waiing for customer");
            //
            TcpClient client= newsock.AcceptTcpClient();
            //
            NetworkStream ns = client.GetStream();
            Console.WriteLine(client.Client.RemoteEndPoint.ToString());
            string welcome = "welcome to the sever";
            data= Encoding.ASCII.GetBytes(welcome);
            ns.Write(data, 0, data.Length);
            while (true)
            {
                data = new byte[1024];
                recv = ns.Read(data, 0, data.Length);
                if (recv == 0)
                    break;
                Console.WriteLine(Encoding.ASCII.GetString(data,0,recv));
                ns.Write(data, 0, recv);
            }
            ns.Close();
            client.Close();
            newsock.Stop();
        }
    }
}
