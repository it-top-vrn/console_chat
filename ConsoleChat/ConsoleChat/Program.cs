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
            new AutorizationForm().Initialize();
            
        }
    }
}
