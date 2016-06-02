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
            if (username.Text.Trim() == "" || password.Password.Trim() == "")
            {
                message.Content = "please fill in all fields";
                return;
            }

            ServiceClient sc = new ServiceClient();
            Dictionary<String, String> login = sc.Login(username.Text, password.Password);
            if (login["status"] == Constants.ERROR)
            {
                message.Content = login["message"];
                return;
            }
            else
            {
                message.Content = "Welcome boss!!!!!!! >> " + login["nickname"];
            }

            this.Close();
            RequestWindow rw = new RequestWindow();
            rw.Left = (Utils.getScreenWidth() / 2) - (this.Width / 2);
            rw.Top = (Utils.getScreenHeight() / 2) - (this.Height / 2);
            rw.Show();
        }
    }
}
