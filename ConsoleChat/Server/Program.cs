using System;
using System.Globalization;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Server.JSON;
using Newtonsoft.Json;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            string json = "{\"TypeofCommand\":2,\"Sender\":\"test\",\"Recepient\":\"Alexis\",\"DateTime\":null,\"TextMessage\":null,\"FileName\":null,\"AmountMessages\":10}";
            Message message = JsonConvert.DeserializeObject<Message>(json);
            Console.WriteLine("все хорошо");
            Console.ReadKey();

             DateTime time = DateTime.Now;
            var hui = JsonConvert.SerializeObject(time);
            Console.WriteLine(hui);
            DateTime time2 = JsonConvert.DeserializeObject<DateTime>(hui);

            Console.WriteLine(time2.ToString());
            Console.ReadKey();
            */


            Server server = new Server();
            server.Listen();
            server.ListenServerCommands();
        }
    }
}