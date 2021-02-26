namespace Server.JSON
{
    public interface ISerializable
    {
        public string Serialize();
        public void Execute(Client client);
    }
}