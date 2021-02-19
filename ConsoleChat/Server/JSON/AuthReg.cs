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
        
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public static bool CanDeserialize(string json)
        {
            if (json.Contains("\"Login\":"))
            {
                return true;
            }
            return false;
        }
        public static AuthReg Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<AuthReg>(json);
        }
    }
}