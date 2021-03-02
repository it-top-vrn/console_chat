using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleChat
{
    public class ServerConnection
    {
        private const string host = "localhost";
        private const int port = 8888;
        
        public string UserName { private set; get; }
        public bool IsConnected { private set; get; }
        
        public TcpClient client;
        public NetworkStream stream;
        
 
        public ServerConnection()
        {
            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(); //старт потока
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
        public void ReceiveMessage()
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
                    Console.WriteLine(message);//вывод сообщения
                }
                catch
                {
                    Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    Console.ReadLine();
                    Disconnect();
                }
            }
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