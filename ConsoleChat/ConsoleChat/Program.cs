using System;

namespace ConsoleChat
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerConnection serverConnection = new ServerConnection();
            AutorizationForm autoriz = new AutorizationForm(serverConnection);
            autoriz.Initialize();
        }
    }
}
