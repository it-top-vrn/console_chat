using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using ConsoleChat;
using Newtonsoft.Json;
using Terminal.Gui;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Server.JSON
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

        public void Execute()
        {
            if (TypeofCommand == MessageTypeofCommand.TextMessage || TypeofCommand == MessageTypeofCommand.FileMessage)
            {
                ChatForm.instance.AddMessage($"[{DateTime}] [{Sender}]: {TextMessage}\n");
            } else if (TypeofCommand == MessageTypeofCommand.GetMessages)
            {
                Dictionary<DateTime, List<string>> dictionary = JsonConvert.DeserializeObject<Dictionary<DateTime, List<string>>>(TextMessage);
                foreach (var element in dictionary)
                {
                    if (element.Value[1] == "file")
                    {
                        ChatForm.instance.AddMessage($"[{element.Key}] [{element.Value[2]}]", true);
                        string file = "Скачать файл: ";
                        while (file.Length != 150)
                        {
                            file += " ";
                        }
                        ChatForm.instance.AddMessage(file, true, element.Value[0]);
                    }
                    else
                    {
                        ChatForm.instance.AddMessage($"[{element.Key}] [{element.Value[2]}]: {element.Value[0]}\n");
                    }
                    
                }
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
            return JsonConvert.DeserializeObject<Message>(json);
        }
    }
    
}