using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server.JSON;
using Terminal.Gui;

namespace ConsoleChat
{
    public class ServerConnection
    {
        public static ServerConnection instance;
        
        private const string host = "192.168.0.214";
        private const int port = 8888;
        
        public string UserName { private set; get; }
        public bool IsConnected { private set; get; }
        
        public TcpClient client;
        public NetworkStream stream;
        
 
        public ServerConnection()
        {
            instance = this;
            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                // запускаем новый поток для получения данных
                
                Console.WriteLine("Подключение к серверу прошло успешно");
                IsConnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        // отправка сообщений
        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Сообщение было успешно отправлено");
            Console.WriteLine(message);
        }
        // получение сообщений
        public async void GetMessage()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        byte[] data = new byte[64]; // буфер для получаемых данных
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        do
                        {
                            bytes = stream.Read(data, 0, data.Length);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (stream.DataAvailable);
 
                        string message = builder.ToString();
                        HandleMessage(message);
                        File.Create("test").Close();
                        StreamWriter writer = new StreamWriter("test", true);
                        writer.WriteLine(message);
                        writer.Close();
                        Console.WriteLine(message);//вывод сообщения
                    }
                    catch
                    {
                        Console.WriteLine("Подключение прервано!"); //соединение было прервано
                        Console.ReadLine();
                        Disconnect();
                    }
                }
            });
            
        }
 
        protected internal void HandleMessage(string json)
        {
            
            ISerializable command = null;
            if (AuthReg.CanDeserialize(json))
            {
                command = AuthReg.Deserialize(json);
            } else if (Message.CanDeserialize(json))
            {
                command = Message.Deserialize(json);
            }
            Console.WriteLine(json);
            
            Application.MainLoop.Invoke(() =>
            {
                command.Execute();
            });
            
            
        }
        public void Disconnect()
        {
            if(stream!=null)
                stream.Close();//отключение потока
            if(client!=null)
                client.Close();//отключение клиента
            IsConnected = false;
            Environment.Exit(0); //завершение процесса
        }
    }
}