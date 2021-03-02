using System;

namespace ConsoleChat
{
    class Program
    {
        public static ServerConnection ServerConnection;
        static void Main(string[] args)
        {
            ServerConnection = new ServerConnection();
            AutorizationForm autoriz = new AutorizationForm(ServerConnection);
            autoriz.Initialize();
        }
    }
}
