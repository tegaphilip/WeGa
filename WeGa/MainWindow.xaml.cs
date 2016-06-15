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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeGa.library;

namespace WeGa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceClient sc;

        public MainWindow()
        {
            InitializeComponent();
            VerifyDatabaseConnection();
            CenterWindowOnScreen();
        }

        private void CenterWindowOnScreen()
        {
            this.Left = (Utils.getScreenWidth() / 2) - (this.Width / 2);
            this.Top = (Utils.getScreenHeight() / 2) - (this.Height / 2);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow()
            {
                ShowInTaskbar = false,               // don't show the dialog on the taskbar
                Topmost = true,                      // ensure we're Always On Top
                ResizeMode = ResizeMode.NoResize,    // remove excess caption bar buttons
                Owner = Application.Current.MainWindow,
            };
            loginWindow.Left = (Utils.getScreenWidth() / 2) - (loginWindow.Width / 2);
            loginWindow.Top = (Utils.getScreenHeight() / 2) - (loginWindow.Height / 2);
            loginWindow.ShowDialog();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow()
            {
                ShowInTaskbar = false,               // don't show the dialog on the taskbar
                Topmost = true,                      // ensure we're Always On Top
                ResizeMode = ResizeMode.NoResize,    // remove excess caption bar buttons
                Owner = Application.Current.MainWindow
            };
            registerWindow.Left = (Utils.getScreenWidth() / 2) - (registerWindow.Width / 2);
            registerWindow.Top = (Utils.getScreenHeight() / 2) - (registerWindow.Height / 2);
            registerWindow.ShowDialog();
        }

        private void VerifyDatabaseConnection()
        {
            sc = new ServiceClient();
            if (!sc.Ping())
            {
                MessageBox.Show("Your database server is not running or cannot be reached");
                this.Close();
            }
        }
    }
}
