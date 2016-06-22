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
    /// Interaction logic for PendingRequests.xaml
    /// </summary>
    public partial class PendingRequests : Window
    {
        private Dictionary<string, string>[] requests;
        private Dictionary<int, Dictionary<string, string>> requestsMap;
        private ServiceClient serviceClient;

        public PendingRequests(Dictionary<string, string>[] requests)
        {
            InitializeComponent();
            this.requests = requests;
            createScreen();
            serviceClient = new ServiceClient();
        }

        private void createScreen()
        {
            if (requests == null || requests.Count() == 0)
            {
                MessageBox.Show("You have no pending requests");
                return;
            }

            requestsMap = new Dictionary<int, Dictionary<string, string>>();
            int i = 0;
            foreach (Dictionary<string, string> dic in requests)
            {
                String nickname = dic["nickname"];

                ListBoxItem item = new ListBoxItem();
                item.Content = "VERSUS " + nickname.ToUpper() + " AT " + dic["date_started"];
                requestList.Items.Add(item);

                requestsMap.Add(i, dic);
                i++;
            }
        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> selectedRequest = getSelectedRequest();
            if (selectedRequest == null)
            {
                MessageBox.Show("No request was selected");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Start Game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int gameId = int.Parse(selectedRequest["game_id"]);
                //remove from list
                int selectedIndex = requestList.SelectedIndex;
                requestList.Items.RemoveAt(selectedIndex);
                requestsMap.Remove(selectedIndex);
                this.Close();

                Window gb = new GameBoard(selectedRequest["nickname"], gameId, selectedRequest["letters"]);
                gb.Left = (Utils.getScreenWidth() / 2) - (gb.Width / 2);
                gb.Top = (Utils.getScreenHeight() / 2) - (gb.Height / 2);
                gb.ShowDialog();
            }
        }

        private void btnNeglect_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> selectedRequest = getSelectedRequest();
            if (selectedRequest == null)
            {
                MessageBox.Show("No request was selected");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to decline this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int gameId = int.Parse(selectedRequest["game_id"]);
                Dictionary<string, string> response = serviceClient.NeglectGame(gameId, (string)Application.Current.Resources["nickname"]);
                if (response == null || response["status"] == Constants.ERROR)
                {
                    String errorMessage = response.ContainsKey("message") ? response["message"] : "an unknown error occurred";
                    MessageBox.Show(errorMessage);
                }
                else
                {
                    //remove from list
                    int selectedIndex = requestList.SelectedIndex;
                    requestList.Items.RemoveAt(selectedIndex);
                    requestsMap.Remove(selectedIndex);
                    MessageBox.Show("Game has been declined");
                }
            }
        }

        private Dictionary<string, string> getSelectedRequest()
        {
            int selectedIndex = requestList.SelectedIndex;
            if (selectedIndex < 0)
            {
                return null;
            }

            return requestsMap[selectedIndex];
        }
    }
}
