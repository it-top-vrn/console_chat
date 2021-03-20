﻿using System;
using System.Collections.Generic;
using System.Text;
using Server.JSON;
using Terminal.Gui;

namespace ConsoleChat
{
    class ChatForm
    {
	    public static ChatForm instance;
	    private List<string> messages;
	    private string selectedUser;
	    private ServerConnection _serverConnection;
		private ListView list_Message;
		public ListView list_Dialogs;
	    
	    public void Initialize(ServerConnection serverConnection)
	    {
		    instance = this;
		    _serverConnection = serverConnection;
		    _serverConnection = ServerConnection.instance;
			Application.Init();
			var top = Application.Top;

			var win_Dialogs = new Window("NameDialogs")
			{
				X = 0,
				Y = 0,

				Width = 50,
				Height = 17
			};
			var win_InptMessage = new Window("Message")
			{
				X = 0,
				Y = Pos.Bottom(win_Dialogs),

				Width = 50,
				Height = Dim.Fill()
			};
			var win_ShowDialogs = new Window("Dialogs")
			{
				X = Pos.Right(win_Dialogs),
				Y = 0,

				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

			messages = new List<string>();
			list_Message = new ListView(messages)
			{
				X = 0,
				Y = 0,
				Height = Dim.Fill(),
				Width = Dim.Fill()
			};
			list_Message.KeyUp += List_MessageOnKeyUp;
			var MessageText = new TextField()
			{
				X = 0,
				Y = 0,

				Width = 38,
				Height = Dim.Fill(),

			};
			var but_Enter = new Button("Enter")
			{
				X = Pos.Right(MessageText),
				Y = 0,
				Width = 10,
				Height = 1
			};
			
			list_Dialogs = new ListView(Program.userList)
			{
				X = 1,
				Y = 1,
				Width = Dim.Fill(),
				Height = Dim.Fill(),
			};
			list_Dialogs.OpenSelectedItem += (a) =>
			{
				selectedUser = Program.userList[a.Item];
				Message message = new Message();
				message.TypeofCommand = MessageTypeofCommand.GetMessages;
				message.Sender = selectedUser;
				message.Recepient = Program.userName;
				message.AmountMessages = 10;
				ClearChat();
				_serverConnection.SendMessage(message.Serialize());
			};

			but_Enter.Clicked += () => {
				Message message = new Message
				{
					TypeofCommand = MessageTypeofCommand.TextMessage,
					Sender = Program.userName,
					Recepient = selectedUser,
					DateTime = DateTime.Now,
					TextMessage = MessageText.Text.ToString(),
				};
				_serverConnection.SendMessage(message.Serialize());
				MessageText.Text = "";
			};

			top.Add(win_Dialogs);
			top.Add(win_InptMessage);
			top.Add(win_ShowDialogs);

			win_Dialogs.Add(
				list_Message);

			win_InptMessage.Add(
				MessageText, but_Enter);

			win_ShowDialogs.Add(
				list_Dialogs);
			
			Application.Run();
		}

	    private void List_MessageOnKeyUp(View.KeyEventEventArgs obj)
	    {
		    // МОЖЕТ БЫТЬ ПРИГОДИТЬСЯ
	    }

	    public void ClearChat()
	    {
		    messages.Clear();
	    }

	    public void AddMessage(string message)
	    {
			string word = "";
			List<string> words = new List<string>();
			for(var i = 0; i < message.Length; i++)
			{
				var c = message[i];
				if (c != ' ')
				{
					word += c;
				} else
				{
					words.Add(word);
					word = "";
				}
			}
			if(word != "" && !words.Contains(word.Trim()))
			{
				words.Add(word.Trim());
			}
			int size = 0;
			string str = "";
			int j = 0;
			bool shit = false;
			foreach(var item in words)
			{
				if (size + item.Length <= 40)
				{
					str += item + " ";
					size += item.Length;
				} else
				{
					messages.Add(str);
					if (j == words.Count - 1)
					{
						messages.Add(item);
						shit = true;
					}
					str = "";
					size = 0;
				}
				j++;
			}
			if(!shit) messages.Add(str);
			if (list_Message.Source.ToList().Count > 14)
			{
				list_Message.MovePageDown();
			}
			list_Message.SelectedItem = messages.Count -1;
			for (int i = 0; i < 14; i++)
			{
				list_Message.MoveUp();
			}
			messages.Add("");
	    }
	}
}
