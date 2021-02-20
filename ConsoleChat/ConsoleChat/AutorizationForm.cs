using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace ConsoleChat
{
    class AutorizationForm
    {
		public void Initialize()
		{

			Application.Init();
			var top = Application.Top;

			var win = new Window("AUTORIZATION")
			{
				X = 0,
				Y = 1,

				Width = Dim.Fill(),
				Height = Dim.Fill()
			};

			top.Add(win);

			var login = new Label("Login: ")
			{
				X = 6,
				Y = 2
			};
			var password = new Label("Password: ")
			{
				X = 3,
				Y = 5
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

			var button_OK = new Button(3, 10, "OK")
			{
				Width = 3,
				Height = 10
			};
			var button_Cansel = new Button(10, 10, "Cansel")
			{
				Width = 3,
				Height = 10
			};
			var button_Registration = new Button(30, 10, "Registration")
			{
				Width = 3,
				Height = 10
			};
			var btn = new Button(30, 10, "")
			{
				Width = 3,
				Height = 10
			};

			button_Registration.Clicked += Clicked;
			void Clicked() => Registration_Form();

			win.Add(
				login, password, loginText, passText, button_OK, button_Cansel, button_Registration
			);

			Application.Run();
		}

		private void Registration_Form()
        {
			RegistrationForm reg = new RegistrationForm();
			reg.Initialize();
        }
	}
}
