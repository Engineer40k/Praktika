using Praktika_1.Classes;
using Praktika_1.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktika_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.Show();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var Login = LoginMain.Text;

            var Password = PasswordMain.Text;

            var Context = new AppDbContext();

            var user = Context.Users.SingleOrDefault(x => x.Login == Login && x.Password == Password);
            if (user is null) 
            {
                MessageBox.Show("Неправильный логин или пароль!");
                return;
            }
            else
            {
                this.Close();
                MainCabinet mainCabinet = new MainCabinet();
                mainCabinet.Show();
            }
        }

        private void HidePassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
