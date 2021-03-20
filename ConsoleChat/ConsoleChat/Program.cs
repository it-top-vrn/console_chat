using System;
using System.Collections.Generic;
using System.Net;

namespace ConsoleChat
{
    class Program
    {
        public static string userName;
        public static List<string> userList;
        public static bool authorizated = false;
        static void Main(string[] args)
        {
            ServerConnection _serverConnection = new ServerConnection();
            _serverConnection.GetMessage();
            new AutorizationForm().Initialize(_serverConnection);
            
        }
    }
}
