using System;
using System.Text.Json;

namespace Server.JSON
{
    public enum MessageTypeofCommand 
    {
        TextMessage, // Отправляем сообщение, Указываем TextMessage, AmountMessages и FileName не указываем
        FileMessage, // Отправляем файл. Указываем FileName, AmountMessages и TextMessage не указываем
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
    }
    
}