using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
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
				Y = 6,
				Width = Dim.Width(passText)
			};
			var button_OK = new Button(3, 10, "OK")
			{
				Width = 3,
				Height = 10
			};
			button_OK.Clicked += ClickedOk;
			void ClickedOk() => Button_OKOnClicked(loginText, passText, repeatPassText);
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

		private void Button_OKOnClicked(TextField login, TextField password, TextField passwordConfirm)
		{
			if (!Program.authorizated)
			{
				string s = login.Text.ToString();
				string m = password.Text.ToString();
				string passwordConf = passwordConfirm.Text.ToString();
				Regex regex1 = new Regex(@"/[А-Яа-я<>{}()'!@#$%^&*]/");
				MatchCollection matches1 = regex1.Matches(s);
				if ((matches1.Count > 0) & (s.Length < 5) & (s.Length > 20))
				{

					MessageBox.Query("Ошибка", "Логин не соответствует требованиям", "OK");
				}
				else if (m.Length < 5)
				{
					MessageBox.Query("Ошибка", "Пароль слишком слабый, как и ты", "OK");
				} 
				else if (m != passwordConf)
				{
					MessageBox.Query("Ошибка", "Пароли не совпадают", "OK");
				}
				else
				{
					AuthReg authReg = new AuthReg();
					authReg.Login = login.Text.ToString();
					authReg.Password = password.Text.ToString();
					authReg.TypeOfCommand = AuthRegTypeOfCommand.Registration;
					Program.userName = authReg.Login;
					_serverConnection.SendMessage(authReg.Serialize());
				}
			}
			
		}

		private void AutorizationForm()
        {
	        if (!Program.authorizated)
	        {
		        AutorizationForm autoriz = new AutorizationForm();
		        autoriz.Initialize(_serverConnection);
	        }
        }
	}
}
