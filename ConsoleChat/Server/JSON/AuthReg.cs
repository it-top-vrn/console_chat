using System;
using System.Data.SqlTypes;
using Newtonsoft.Json;
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
        
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Execute(Client client)
        {
            if (TypeOfCommand == AuthRegTypeOfCommand.Authorization)
            {
                // должна быть авторизация
                Console.WriteLine("Авторизация не реализована. Нет бд");
                //throw new SqlNullValueException("Авторизация не реализована. Нет бд");
            } else if (TypeOfCommand == AuthRegTypeOfCommand.Registration)
            {
                // должна быть регистрация
                Console.WriteLine("Регистрация не реализована. Нет бд");
                //throw new SqlNullValueException("Регистрация не реализована. Нет бд");
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