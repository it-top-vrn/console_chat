using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Server.JSON;
using Terminal.Gui;

namespace ConsoleChat
{
	class RegistrationForm
	{
		private ServerConnection _serverConnection;
		public void Initialize(ServerConnection serverConnection)
		{
			_serverConnection = serverConnection;
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
				Y = 1
			};
			var password = new Label("Password: ")
			{
				X = 10,
				Y = 4
			};
			var repeatPassword = new Label("Repeat password: ")
			{
				X = 3,
				Y = 6
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
			var button_OK = new Button(3, 10, "OK")
			{
				Width = 3,
				Height = 10
			};
			button_OK.Clicked += ClickedOk;
			void ClickedOk() => Button_OKOnClicked(loginText, passText);
			var button_Cansel = new Button(10, 10, "Cansel")
			{
				Width = 3,
				Height = 10
			};
			var button_Autorization = new Button(30, 10, "Autorization")
			{
				Width = 3,
				Height = 10
			};

			button_Autorization.Clicked += Clicked;
			void Clicked() => AutorizationForm();

			win.Add(
				login, password, loginText, passText, button_OK, button_Cansel, repeatPassword, repeatPassText, button_Autorization
			);

			Application.Run();
		}

		private void Button_OKOnClicked(TextField login, TextField password)
		{
			_serverConnection = new ServerConnection();

			string s;
			s = login.Text.ToString();
			Regex regex = new Regex(@"/[А-Яа-я<>{}()'!@#$%^&*]/");
			MatchCollection matches = regex.Matches(s);
			if (matches.Count > 0)
			{
				MessageBox.Query("Ошибка", "Найдены запрещенные символы");
			}
			else
			{
				AuthReg authReg = new AuthReg();
				authReg.Login = login.Text.ToString();
				authReg.Password = password.Text.ToString();
				authReg.TypeOfCommand = AuthRegTypeOfCommand.Registration;
				_serverConnection.SendMessage(authReg.Serialize());
			}
			
		}

		private void AutorizationForm()
        {
			AutorizationForm autoriz = new AutorizationForm();
			autoriz.Initialize(_serverConnection);
        }
	}
}
