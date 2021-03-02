using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        public static TcpListener tcpListener; // сервер для прослушивания
        private List<Client> clients = new List<Client>(); // все подключения
        
        protected internal void AddConnection(Client client)
        {
            clients.Add(client);
            Console.WriteLine($"Добавлен новый клинет {client.client.Client.RemoteEndPoint}");
        }
        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            Client client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
                clients.Remove(client);
        }

        public void ListenServerCommands()
        {
            while (true)
            {
                var command = Console.ReadLine();
                if (command == "clients")
                {
                    Console.WriteLine($"Кол-во подключенных клиентов: {clients.Count}");
                    foreach (var client in clients)
                    {
                        string user;
                        if (client.userName == null) user = "NOT_AUTHORIZATED";
                        else user = client.userName;
                        Console.WriteLine(user + " - " + client.client.Client.RemoteEndPoint);
                    }
                }
                else
                {
                    Console.WriteLine("Неизвестная команда");
                }
            }
        }
        // прослушивание входящих подключений
        protected internal void Listen()
        {
            Task.Run(() =>
            {
                try
                {
                    tcpListener = new TcpListener(IPAddress.Any, 8888);
                    tcpListener.Start();
                    Console.WriteLine("Сервер запущен. Ожидание подключений...");
 
                    while (true)
                    {
                        TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    
                        Client clientObject = new Client(tcpClient, this);
                        Thread clientThread = new Thread(clientObject.Process);
                        clientThread.Start();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Тут было отключение всех клиентов");
                    //Disconnect();
                }
            });
        }
 
        // трансляция сообщения подключенным клиентам
        protected internal void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id!= id) // если id клиента не равно id отправляющего
                {
                    clients[i].Stream.Write(data, 0, data.Length); //передача данных
                }
            }
        }
        // отключение всех клиентов
        protected internal void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера
 
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
            Environment.Exit(0); //завершение процесса
        }

        public List<Client> GetClients()
        {
            return new List<Client>(clients);
        }
    }
}