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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String un = username.Text;
            String pass = password.Password;

            if (un.Trim() == "" || pass.Trim() == "")
            {
                message.Content = "please fill in all fields";
                return;
            }

<<<<<<< HEAD
            MessageBox.Show(login.ToString());
            
            this.Close();
            RequestWindow rw = new RequestWindow();
            rw.Show();
      
=======
            ServiceClient sc = new ServiceClient();
            Dictionary<String, String> login = sc.Login(un, pass);
            if (login["status"] == Constants.ERROR)
            {
                message.Content = login["message"];
            }
            else
            {
                message.Content = "Welcome boss!!!!!!! >> " + login["nickname"];
            }
>>>>>>> 37c928788076ee788202bf4fc71d081314249bd5
        }
    }
}
