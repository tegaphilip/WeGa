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
    /// Interaction logic for RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        ServiceClient sc;
        List<string> nickNameList;
        String receivedNickName;
        String gameLetters;
        
        //hack into listbox
        Dictionary<int, string> nickNamesDictionary;

        public RequestWindow()
        {
            sc = new ServiceClient();
            InitializeComponent();
            setPlayerList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = playerList.SelectedIndex;
            if (selectedIndex == 0)
            {
                MessageBox.Show("An invalid item was selected");
                return;
            }

            receivedNickName = nickNamesDictionary[selectedIndex-1];
            gameLetters = Utils.getLetters();
            Application.Current.Resources["gameLetters"] = gameLetters;
            Dictionary<String, String> response = sc.CreateGame((String)Application.Current.Resources["nickname"], receivedNickName, gameLetters);

            if (response == null || response["status"] == Constants.ERROR)
            {
                String errorMessage = response.ContainsKey("message") ? response["message"] : "an unknown error occurred";
                MessageBox.Show(errorMessage);
                return;
            }

            this.Close();
            Window gb = new GameBoard(receivedNickName, int.Parse(response["game_id"]));
            gb.ShowDialog();

        }

        private void setPlayerList()
        {
            nickNameList = sc.GetPlayerNicknames();
            if (nickNameList == null)
            {
                MessageBox.Show("There are no available players at the moment");
            }
            else
            {
                nickNameList.Remove((string)Application.Current.Resources["nickname"]);
                int i = 0;
                nickNamesDictionary = new Dictionary<int, string>();
                foreach (string nm in nickNameList)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = nm;
                    playerList.Items.Add(item);
                    nickNamesDictionary.Add(i, nm);
                    i++;
                }
            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

    }
}
