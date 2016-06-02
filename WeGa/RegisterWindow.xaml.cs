﻿using System;
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
using WeGa.library;

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

            if (nickname.Text.Trim().Equals(""))
            {
                message.Content += "Please enter your nickname~~ \n";
            }
            else if (username.Text.Trim().Equals(""))
            {
                message.Content += "Please enter your username~~ \n";
            }
            else if (password.Password.Trim().Equals(""))
            {
                message.Content += "Please enter your password~~ \n";
            }      
            else if (password.Password != passwordRepeat.Password)
            {
                message.Content += "Your passwords are not the same! \n";
            }
            else
            {
                ServiceClient sc = new ServiceClient();
                bool res = sc.Register(username.Text.Trim(), password.Password.Trim(), nickname.Text.Trim());

                MessageBoxButton mbb = new MessageBoxButton();
                MessageBox.Show(this, res.ToString(), "You've registered sucessfully!~\n"+"Click OK to login!~", mbb);
                this.Close();

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
