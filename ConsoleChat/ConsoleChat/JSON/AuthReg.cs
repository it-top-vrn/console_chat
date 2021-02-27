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

        public void Execute()
        {
            if (TypeOfCommand == AuthRegTypeOfCommand.Authorization)
            {
                
            } else if (TypeOfCommand == AuthRegTypeOfCommand.Registration)
            {
                
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