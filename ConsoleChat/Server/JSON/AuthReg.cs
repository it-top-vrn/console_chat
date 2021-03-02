using System;
using System.Data.SqlTypes;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using api_database;

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
        
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Execute(Client client)
        {
            if (TypeOfCommand == AuthRegTypeOfCommand.Authorization)
            {
                Message = DBconnection.Authorization(Login, Password);
                if (Message == "Authorization success")
                {
                    Success = true;
                }
                else if (Message == "Authorization fail")
                {
                    Success = false;
                }
            } else if (TypeOfCommand == AuthRegTypeOfCommand.Registration)
            {
                Message = DBconnection.Registration(Login, Password);
                if (Message == "Registration success")
                {
                    Success = true;
                }
                else if (Message == "Registration fail")
                {
                    Success = false;
                }
            }
            client.SendMessage(Serialize());
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