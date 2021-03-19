using System;
using System.Collections.Generic;
using System.Net;

namespace ConsoleChat
{
    class Program
    {
        public static string userName;
        public static List<string> userList;
        static void Main(string[] args)
        {
            ServerConnection _serverConnection = new ServerConnection();
            _serverConnection.GetMessage();

            //ChatForm chat = new ChatForm();
            //chat.Initialize(_serverConnection);

            new AutorizationForm().Initialize(_serverConnection);
            
        }
    }
}
