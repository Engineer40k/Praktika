using Praktika_1.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Praktika_1
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            LoginErrorBox.Text = string.Empty;
            EmailErrorBox.Text = string.Empty;
            PasswordErrorBox.Text = string.Empty;
            LoginReg.BorderBrush = new SolidColorBrush(Colors.Black);
            EmailReg.BorderBrush = new SolidColorBrush(Colors.Black);
            PasswordReg.BorderBrush = new SolidColorBrush(Colors.Black);
            PasswordRepReg.BorderBrush = new SolidColorBrush(Colors.Black);
            var Login = LoginReg.Text;
            var Email = EmailReg.Text;
            var Password = PasswordReg.Text;

            var Context = new AppDbContext();

            var user_exists = Context.Users.FirstOrDefault(x => x.Login == Login);
            if (user_exists is not null)
            {
                MessageBox.Show("Клуб кожевенников на два блока ниже");
                return;
            }
            else if (LoginCheck() == false) 
            {
                LoginReg.BorderBrush = new SolidColorBrush(Colors.Red);
                LoginErrorBox.Text="Введите логин";
            }
            else if(EmailCheck() == false)
            {
                EmailReg.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            else if(PasswordCheck() == false)
            {
                PasswordReg.BorderBrush = new SolidColorBrush(Colors.Red);
                PasswordRepReg.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            else 
            {
                var User = new User { Login = Login, Password = Password, Email = Email };
                Context.Users.Add(User);
                Context.SaveChanges();
                MessageBox.Show("Welcome to the club, buddy");
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.Show();
            }

        }

        private void AlreadyReg_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private bool LoginCheck()
        {
            if (LoginReg.Text.Length == 0)
            {
                return false;
            }
            return true;
        }
        private bool EmailCheck()
        {
            if (EmailReg.Text.Length == 0)
            {
                EmailErrorBox.Text="Введите электронную почту";
                return false;
            }

            if (EmailReg.Text.IndexOf("@") > -1)
            {
                if (EmailReg.Text.IndexOf(".", EmailReg.Text.IndexOf("@")) > EmailReg.Text.IndexOf("@"))
                {
                    return true;
                }
            }
            EmailErrorBox.Text="Формат введенной почты некорректен";
            return false;
        }
        private bool PasswordCheck()
        {
            bool hasUpperCase = false;
            bool hasDigit = false;
            bool hasSpecialCharacter = false;
            if (PasswordReg.Text.Length <= 8)
            {
                PasswordErrorBox.Text = "Пароль менее 8 символов";
                return false;
            }
            foreach (char c in PasswordReg.Text)
            {
                if (char.IsUpper(c))
                {
                    hasUpperCase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialCharacter = true;
                }
            }
            if (hasUpperCase == false)
            {
                PasswordErrorBox.Text = "Пароль не содержит букв верхнего регистра";
                return false;
            }
            if (hasDigit == false)
            {
                PasswordErrorBox.Text = "Пароль не содержит цифр";
                return false;
            }
            if(hasSpecialCharacter == false)
            {
                PasswordErrorBox.Text = "Пароль не содержит спецсимволов";
                return false;
            }
            if (PasswordReg.Text != PasswordRepReg.Text)
            {
                PasswordErrorBox.Text = "Введенные пароли не совпадают";
                return false;
            }
            return true;
        }
    }
}


