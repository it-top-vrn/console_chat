using System.Text.Json;

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
    }
}