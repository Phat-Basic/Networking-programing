using System.Net;
using System.Net.Sockets;
using System.Text;


namespace TCPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            TcpClient server;
            try{
                server = new TcpClient("127.0.0.1", 9000);
            }
            catch (SocketException) {
                Console.WriteLine("Unable to connecting sever");
                return;
            }
            //
            NetworkStream ns=server.GetStream();
            //
            int recv = ns.Read(data, 0, data.Length);
            stringData=Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);
            while (true) { 
                input =Console.ReadLine();
                if(input=="exit")
                { break; }
                ns.Write(Encoding.ASCII.GetBytes(input),0, input.Length);
                ns.Flush();
                data = new byte[1024];
                recv= ns.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            Console.WriteLine("Disconnecting from server...");
            ns.Close();
            server.Close();
        }
    }
}
