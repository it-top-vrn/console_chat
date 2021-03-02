using System;
using System.Net.Sockets;
using System.Text;
using Server.JSON;

namespace Server
{
    public class Client
    {
        protected internal string Id { get; }
        protected internal NetworkStream Stream {get; private set;}
        public string userName;
        public TcpClient client;
        public Server server; // объект сервера
 
        public Client(TcpClient tcpClient, Server server)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            this.server = server;
            server.AddConnection(this);
        }
 
        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                // в бесконечном цикле получаем сообщения от клиента
                while (true)
                {
                    string message;
                    message = GetMessage();
                    HandleMessage(message);
                    //server.BroadcastMessage(message, Id);

                }
            }
            catch(Exception e)
            {
                string user = "";
                if (userName == null) user = "NOT_AUTHORIZATED";
                else user = userName;
                Console.WriteLine($"Клиент {user} - {client.Client.RemoteEndPoint}");
            }
            finally
            {
                // в случае выхода из цикла закрываем ресурсы
                server.RemoveConnection(Id);
                Close();
            }
        }
 
        // чтение входящего сообщения и преобразование в строку
        private string GetMessage()
        {
            byte[] data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);
 
            return builder.ToString();
        }
 
        // закрытие подключения
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
        protected internal void HandleMessage(string json)
        {
            string user = "";
            if (userName == null) user = "NOT_AUTHORIZATED";
            else user = userName;
            
            ISerializable command = null;
            if (AuthReg.CanDeserialize(json))
            {
                command = AuthReg.Deserialize(json);
                Console.WriteLine($"Пришла команда от {user} - {client.Client.RemoteEndPoint} типа AuthReg");
            } else if (Message.CanDeserialize(json))
            {
                command = Message.Deserialize(json);
                Console.WriteLine($"Пришла команда от {user} - {client.Client.RemoteEndPoint} типа Message");
            }
            Console.WriteLine(json);
            command.Execute(this);
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            Stream.Write(data, 0, data.Length);
        }
      
        // отправка файла
        public void SendFile()
        {
            Console.WriteLine("Укажите полностью путь к файлу (включая расширение):");
            string pathToFile = Console.ReadLine();
            Console.WriteLine("Укажите кому хотите отправить сообщение:");
            string recepient = Console.ReadLine();
            FTPserver ftp = new FTPserver();
            ftp.FTPUploadFile(pathToFile, userName, recepient);
            //pathToFile = ftp.RenameFile(pathToFile, userName, recepient);
            Message jsonFile = new Message { TypeofCommand = MessageTypeofCommand.FileMessage, Sender = userName, Recepient = recepient, FileName = pathToFile };
            byte[] data2 = Encoding.Unicode.GetBytes(jsonFile.Serialize());
            Stream.Write(data2, 0, data2.Length);
        }

        public void FTPServerContents()
        {
            FTPserver ftp = new FTPserver();
            ftp.ServerContents();
        }

        public void DownLoadFile(string filename)
        {
            FTPserver ftp = new FTPserver();
            ftp.FTPDownloadFile(filename);
        }
    }
}