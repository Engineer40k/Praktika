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

            var Login = LoginReg.Text;

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
                MessageBox.Show("Введите логин");
            }
            else if(EmailCheck() == false)
            {
                MessageBox.Show("Введите/Исправте электронную почту");
            }
            else 
            {
                var User = new User { Login = Login, Password = Password };
                Context.Users.Add(User);
                Context.SaveChanges();
                MessageBox.Show("Welcome to the club, buddy");
                this.Close();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }

        }

        private void AlreadyReg_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
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
                return false;
            }

            if (EmailReg.Text.IndexOf("@") > -1)
            {
                if (EmailReg.Text.IndexOf(".", EmailReg.Text.IndexOf("@")) > EmailReg.Text.IndexOf("@"))
                {
                    return true;
                }
            }
            return false;
        }
        private bool PasswordCheck()
        {
            if (PasswordReg.Text != PasswordRepReg.Text)
            {
                MessageBox.Show("Введенные пароли не совпадают");
                return false;
            } 
            if(PasswordReg.Text.Length == 0)
            {
                MessageBox.Show("Введите пароль");
                return false;
            }
            if (PasswordReg.Text.IndexOf("-",'@','#') > -1)
            {

                return true;
            }
            return false;
        }
    }
}


