using System;
using System.Collections.Generic;
using System.Net;
using Server;

namespace ConsoleChat
{
    class Program
    {
        public static string userName;
        public static List<string> userList;
        public static bool authorizated = false;
        static void Main(string[] args)
        {
            /*ServerConnection _serverConnection = new ServerConnection();
            _serverConnection.GetMessage();
            new AutorizationForm().Initialize(_serverConnection);*/
            var server = new FTPserver();
           var test = server.FTPUploadFile(@"C:\Renata Safarova\часы.txt", "test2", "test3");
            Console.WriteLine(test);
            //server.FTPDownloadFile("ftp://ftp60.hostland.ru/mnement/23.03.2021_20_48_07_test2_test3.txt");
        }
    }
}
