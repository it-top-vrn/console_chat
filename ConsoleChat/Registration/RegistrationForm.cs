using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Registration
{
	class RegistrationForm
	{
		public void Initialize()
		{

			Application.Init();
			var top = Application.Top;

			var win = new Window("REGISTRATION")
			{
				X = 0,
				Y = 1,

				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

			top.Add(win);

			var login = new Label("Login: ")
			{
				X = 13,
				Y = 3
			};
			var password = new Label("Password: ")
			{
				X = 10,
				Y = 6
			};
			var repeatPassword = new Label("Repeat password: ")
			{
				X = 3,
				Y = 8
			};
			var loginText = new TextField("")
			{
				X = Pos.Right(password),
				Y = Pos.Top(login),
				Width = 40
			};
			var passText = new TextField("")
			{
				Secret = true,
				X = Pos.Left(loginText),
				Y = Pos.Top(password),
				Width = Dim.Width(loginText)
			};
			var repeatPassText = new TextField("")
			{
				Secret = true,
				X = Pos.Left(passText),
				Y = Pos.Top(repeatPassword),
				Width = Dim.Width(passText)
			};
			var button_OK = new Button(3, 14, "OK")
			{
				Width = 3,
				Height = 10
			};
			var button_Cansel = new Button(10, 14, "Cansel")
			{
				Width = 3,
				Height = 10
			};

			win.Add(
				login, password, loginText, passText, button_OK, button_Cansel, repeatPassword, repeatPassText
			);

			Application.Run();
		}
	}
}
