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

            //close main window once login is successful
            //this.Owner.Close();
            string un = username.Text.Trim();
            Application.Current.Resources["username"] = un;
            List<string> nickNameList = sc.GetPlayerNicknames();
            foreach (string nm in nickNameList)
            {
                if (nm == un)
                    Application.Current.Resources["nickname"] = nm;
            }

            this.Close();
            ParentWindow parentWindow = ParentWindow.getParentWindow();
            parentWindow.Left = (Utils.getScreenWidth() / 2) - (parentWindow.Width / 2);
            parentWindow.Top = (Utils.getScreenHeight() / 2) - (parentWindow.Height / 2);
            parentWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
