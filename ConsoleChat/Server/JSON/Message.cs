using System;
using System.Data.SqlTypes;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Collections.Generic;
using api_database;

namespace Server
{
    public enum MessageTypeofCommand 
    {
        TextMessage, // Отправляем сообщение. Указываем TextMessage. AmountMessages и FileName не указываем
        FileMessage, // Отправляем файл. Указываем FileName. AmountMessages и TextMessage не указываем
        GetMessages // Получаем последние сообщеня. Указываем AmountMessages, Sender, Recepient обязательно, остальное не указываем
    }
    public class Message : ISerializable
    {

        public MessageTypeofCommand TypeofCommand { get; set; }
        public string Sender { get; set; }
        public string Recepient { get; set; }
        public DateTime DateTime { get; set; }
        public string TextMessage { get; set; }
        public string FileName { get; set; }
        public int AmountMessages { get; set; }
        
        public string Serialize()
        {
            return JsonSerializer.Serialize(this);
        }

        public void Execute(Client client)
        {
            var server = client.server;
            if (TypeofCommand == MessageTypeofCommand.TextMessage || TypeofCommand == MessageTypeofCommand.FileMessage)
            {
                if(TypeofCommand == MessageTypeofCommand.FileMessage)
                {
                    DBconnection.PostMessage(Sender, Recepient, FileName, "file");
                } else
                {
                    DBconnection.PostMessage(Sender, Recepient, TextMessage);
                }
                foreach (var recepient in server.GetClients())
                {
                    if (recepient.userName == Recepient)
                    {
                        if (TypeofCommand == MessageTypeofCommand.FileMessage)
                        {
                            Console.WriteLine($"Отправка файла от {Sender} к {Recepient}");
                        }
                        else
                        {
                            Console.WriteLine($"Отправка сообщения от {Sender} к {Recepient}");
                        }
                        recepient.SendMessage(Serialize());
                        break;
                    }
                }
                client.SendMessage(Serialize());

            } else if (TypeofCommand == MessageTypeofCommand.GetMessages)
            {
                var bufferMessage= DBconnection.GetMessage(Sender, Recepient, AmountMessages);
                TextMessage = JsonConvert.SerializeObject(bufferMessage);
                client.SendMessage(Serialize());
            }
        }

        public static bool CanDeserialize(string json)
        {
            if (json.Contains("\"TextMessage\":"))
            {
                return JsonConvert.DeserializeObject<Message>(json) != null;
            }
            return false;
        }
        public static Message Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Message>(json); ;
        }
    }
}