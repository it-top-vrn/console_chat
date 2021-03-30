using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Listen();
            server.ListenServerCommands();
        }
    }
}