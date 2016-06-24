using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
            SystemSounds.Asterisk.Play();
            System.Threading.Thread.Sleep(2000);
            SystemSounds.Beep.Play();
            System.Threading.Thread.Sleep(2000);
            SystemSounds.Exclamation.Play();
            System.Threading.Thread.Sleep(2000);
            SystemSounds.Hand.Play();
            System.Threading.Thread.Sleep(2000);
            SystemSounds.Question.Play();
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
            Dictionary<String, String> response = sc.Login(username.Text, password.Password);
            if (response["status"] == Constants.ERROR)
            {
                message.Content = response["message"];
                return;
            }
            else
            {
                message.Content = "Welcome boss!!!!!!! >> " + response["nickname"];
            }

            Application.Current.Resources["username"] = response["username"];
            Application.Current.Resources["nickname"] = response["nickname"];
            Application.Current.Resources["id"] = response["id"];
            
            this.Close();

            ParentWindow parentWindow = ParentWindow.getParentWindow();
            parentWindow.Left = (Utils.getScreenWidth() / 2) - (parentWindow.Width / 2);
            parentWindow.Top = (Utils.getScreenHeight() / 2) - (parentWindow.Height / 2);
            parentWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
