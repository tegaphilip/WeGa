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
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String un = username.Text;
            MessageBox.Show(un);

            ServiceClient sc = new ServiceClient();
            bool login = sc.Login(un, un);

            MessageBox.Show(login.ToString());
            
            this.Close();
            RequestWindow rw = new RequestWindow();
            rw.Show();
      
        }
    }
}
