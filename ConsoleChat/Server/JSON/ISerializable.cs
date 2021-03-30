namespace Server
{
    public interface ISerializable
    {
        public string Serialize();
        public void Execute(Client client);
    }
}