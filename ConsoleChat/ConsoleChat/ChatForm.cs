using System;
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
	    
	    public void Initialize()
	    {
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
			messages = new List<string>();
			var list_Message = new ListView(messages)
			{
				X = 0,
				Y = 0,
				Height = Dim.Fill(),
				Width = Dim.Fill()
			};
			var win_ShowDialogs = new Window("Dialogs")
			{
				X = Pos.Right(win_Dialogs),
				Y = 0,

				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

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
			
			var list_Dialogs = new ListView(Program.userList)
			{
				X = 1,
				Y = 1,
				Width = Dim.Fill(),
				Height = Dim.Fill(),
			};
			list_Dialogs.MouseClick += (a) =>
			{
				selectedUser = Program.userList[list_Dialogs.SelectedItem];
				Message message = new Message();
				message.TypeofCommand = MessageTypeofCommand.GetMessages;
				message.Sender = Program.userName;
				message.Recepient = selectedUser;
				message.AmountMessages = 10;
				ClearChat();
				_serverConnection.SendMessage(message.Serialize());
				
				
			};

			but_Enter.Clicked += () => {
				messages.Add($"me: {MessageText.Text.ToString()}");
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

	    public void ClearChat()
	    {
		    messages.Clear();
	    }

	    public void AddMessage(string message)
	    {
		    messages.Add(message);
	    }
	}
}
