using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WeGa
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            message.Content = "";

            if (nickname.Text.Equals(""))
            {
                message.Content += "Please enter your nickname~~ \n";
            }
            if (username.Text.Equals(""))
            {
                message.Content += "Please enter your username~~ \n";
            }
            if (password.Password.Equals(""))
            {
                message.Content += "Please enter your password~~ \n";
            }
            if (passwordRepeat.Password.Equals(""))
            {
                message.Content += "Repeat your password please~~ \n";
            }
            if (password.Password != passwordRepeat.Password)
            {
                message.Content += "Your passwords are not the same! \n";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            nickname.Clear();
            username.Clear();
            password.Clear();
            passwordRepeat.Clear();
        }
    }
}
