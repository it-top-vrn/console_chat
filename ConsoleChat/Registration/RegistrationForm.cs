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

			var win = new Window("MyApp")
			{
				X = 0,
				Y = 1,

				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

			top.Add(win);

			var login = new Label("Login: ")
			{
				X = 11,
				Y = 2
			};
			var password = new Label("Password: ")
			{
				X = 8,
				Y = 5
			};
			var forgotPassword = new Label("Forgot password: ")
			{
				X = 1,
				Y = 7
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
				Y = Pos.Top(forgotPassword),
				Width = Dim.Width(loginText)
			};
			var forgotPassText = new TextField("")
			{
				Secret = true,
				X = Pos.Left(passText),
				Y = Pos.Top(password),
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
				login, password, loginText, passText, button_OK, forgotPassword, button_Cansel, forgotPassText 
			);

			Application.Run();
		}
	}
}
