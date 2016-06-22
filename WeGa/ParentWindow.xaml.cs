using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        public static ParentWindow parentWindow;
        private ServiceClient serviceClient;

        public ParentWindow()
        {
            InitializeComponent();
            serviceClient = new ServiceClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ParentWindow getParentWindow()
        {
            if (parentWindow == null)
            {
                parentWindow = new ParentWindow();
            }

            return parentWindow;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            startGame();
        }


        private void startGame()
        {
            RequestWindow rw = new RequestWindow
            {
                ShowInTaskbar = false,               // don't show the dialog on the taskbar
                Topmost = true,                      // ensure we're Always On Top
                ResizeMode = ResizeMode.NoResize,    // remove excess caption bar buttons
                Owner = ParentWindow.getParentWindow(),
            };
            rw.Left = (Utils.getScreenWidth() / 2) - (rw.Width / 2);
            rw.Top = (Utils.getScreenHeight() / 2) - (rw.Height / 2);
            rw.ShowDialog();
        }

        private void Game_Request_Click(object sender, RoutedEventArgs e)
        {
            String nickName = (string)Application.Current.Resources["nickname"];
            Dictionary<string, string>[] requests = serviceClient.GetGameRequests(nickName);

            PendingRequests pendingRequestsWindow = new PendingRequests(requests)
            {
                ShowInTaskbar = false,
                Topmost = true,
                ResizeMode = ResizeMode.NoResize,
                Owner = ParentWindow.getParentWindow(),
            };

            pendingRequestsWindow.Left = (Utils.getScreenWidth() / 2) - (pendingRequestsWindow.Width / 2);
            pendingRequestsWindow.Top = (Utils.getScreenHeight() / 2) - (pendingRequestsWindow.Height / 2);
            pendingRequestsWindow.ShowDialog();
        }

        private void QuitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Game_Results_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ResultsWindow resultsWindow = new ResultsWindow()
            {
                ShowInTaskbar = false,
                Topmost = true,
                ResizeMode = ResizeMode.NoResize,
                Owner = ParentWindow.getParentWindow(),
            };

            resultsWindow.Left = (Utils.getScreenWidth() / 2) - (resultsWindow.Width / 2);
            resultsWindow.Top = (Utils.getScreenHeight() / 2) - (resultsWindow.Height / 2);
            resultsWindow.ShowDialog();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!closeWindow())
            {
                e.Cancel = true;
            }
        }

        private bool closeWindow()
        {
            MessageBoxResult result = MessageBox.Show("Confirm Exit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }

            return false;
        }
    }
}
