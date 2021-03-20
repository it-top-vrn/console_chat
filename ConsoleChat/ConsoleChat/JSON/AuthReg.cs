using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using ConsoleChat;
using Newtonsoft.Json;
using Terminal.Gui;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Server.JSON
{
    public enum AuthRegTypeOfCommand
    {
        Registration, Authorization 
    }

    public class AuthReg : ISerializable
    {
        public AuthRegTypeOfCommand TypeOfCommand { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> UserList { get; set; }
        
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Execute()
        {
            Console.WriteLine(TypeOfCommand.ToString());
            if (TypeOfCommand == AuthRegTypeOfCommand.Authorization || TypeOfCommand == AuthRegTypeOfCommand.Registration)
            {
                if (Success)
                {
                    if (TypeOfCommand == AuthRegTypeOfCommand.Registration && Program.userName != Login)
                    {
                        foreach(var item in UserList)
                        {
                            if (item != Program.userName && !Program.userList.Contains(item))
                            {
                                Program.userList.Add(item);
                            }
                        }
                    } else
                    {
                        Program.userName = Login;
                        Program.userList = UserList;
                        Application.Shutdown();
                        new ChatForm().Initialize(ServerConnection.instance);
                    }
                }
                else
                {
                    MessageBox.Query("Ошибка", Message, "OK");
                }
            }
        }
        public static bool CanDeserialize(string json)
        {
            if (json.Contains("\"Login\":"))
            {
                return JsonConvert.DeserializeObject<AuthReg>(json) != null;
            }
            return false;
        }
        public static AuthReg Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<AuthReg>(json);
        }
    }
}