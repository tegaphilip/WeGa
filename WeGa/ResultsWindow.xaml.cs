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
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        private Dictionary<string, string>[] results;
        private ServiceClient serviceClient;
        private string filterType = "All";
        private Dictionary<int, int> gameListMaps;
        private ListBox listBox;
        public ResultsWindow()
        {
            InitializeComponent();
            serviceClient = new ServiceClient();
            GetAndSetResults();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                this.filterType = ((sender as TabControl).SelectedItem as TabItem).Header as string;
                BuildResult();
            }
        }

        private void GetAndSetResults()
        {
            String nickName = (string)Application.Current.Resources["nickname"];
            results = serviceClient.GetResults(nickName);
        }

        private void BuildResult()
        {
            String id = (string)Application.Current.Resources["id"];

            listBox = allResults as ListBox;
            bool all = false, wins = false, pending = false, losses = false;

            if (this.filterType == "Wins")
            {
                listBox = winResults as ListBox;
                wins = true;
            }
            else if (this.filterType == "Losses")
            {
                //lossesResults = new ListBox();
                listBox = lossesResults as ListBox;
                losses = true;
            }
            else if (this.filterType == "Pending")
            {
                listBox = pendingResults as ListBox;
                pending = true;
            }
            else
            {
                all = true;
            }

            listBox.Items.Clear();

            gameListMaps = new Dictionary<int, int>();

            if (results != null)
            {
                int i = 0;
                foreach (Dictionary<string, string> result in results)
                {
                    String nickname = result["nickname"];
                    ListBoxItem item = new ListBoxItem();
                    if (all || (wins && result["winner"] == id)
                        || (losses && result["winner"] != id && result["winner"] != "")
                        || (pending && result["winner"] == "") && result["game_neglected"] == "False")
                    {
                        item.Content = GetContent(result, id);
                    }
                    else
                    {
                        continue;
                    }

                    listBox.Items.Add(item);
                    gameListMaps.Add(i, int.Parse(result["game_id"]));
                    i++;
                }
            }
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = listBox.SelectedIndex;
            if (selectedIndex < 0)
            {
                MessageBox.Show("Select a game whose results you want to view");
            }
            else
            {
                int gameId = gameListMaps[selectedIndex];
                Dictionary<string, string[]> details = serviceClient.GetGameInfo(gameId);
                string gameLetters = serviceClient.GetGameLetters(gameId);
                if (details == null)
                {
                    MessageBox.Show("The game details could not be retrieved", "Error");
                }
                else
                {
                    GameInformationWindow gameInformationWindow = new GameInformationWindow(details, gameLetters)
                    {
                        ShowInTaskbar = false,               // don't show the dialog on the taskbar
                        Topmost = true,                      // ensure we're Always On Top
                        ResizeMode = ResizeMode.NoResize,    // remove excess caption bar buttons
                        Owner = this,
                    };
                    gameInformationWindow.Left = (Utils.getScreenWidth() / 2) - (gameInformationWindow.Width / 2);
                    gameInformationWindow.Top = (Utils.getScreenHeight() / 2) - (gameInformationWindow.Height / 2);
                    gameInformationWindow.ShowDialog();
                }
            }
        }

        private Grid GetContent(Dictionary<string, string> result, String id)
        {
            Grid DynamicGrid = CreateGrid();
            TextBlock txtBlock0 = new TextBlock();
            TextBlock txtBlock1 = new TextBlock();
            TextBlock txtBlock2 = new TextBlock();
            TextBlock txtBlock3 = new TextBlock();
            TextBlock txtBlock4 = new TextBlock();

            txtBlock0.Text = result["nickname"].ToUpper();
            txtBlock1.Text = result["player1_score"];
            txtBlock2.Text = result["nickname2"].ToUpper();
            txtBlock3.Text = result["player2_score"] ;

            if (result["winner"] != "" && result["player2_score"] == "")
            {
                txtBlock4.Text = "GAME RESIGNED";
            }
            if (result["winner"] != "" && result["player1_score"] == "")
            {
                txtBlock4.Text = "GAME RESIGNED";
            }

            if (result["game_neglected"] == "True")
            {
                txtBlock3.Text = "GAME DECLINED";
                txtBlock3.Foreground = new SolidColorBrush(Colors.Red);
                txtBlock3.FontWeight = FontWeights.Bold;
            }

            if (id != result["player1_id"])
            {
                txtBlock0.FontWeight = FontWeights.Bold;
                txtBlock0.Text = "ME";
                txtBlock1.FontWeight = FontWeights.Bold;
            }
            else
            {
                txtBlock2.FontWeight = FontWeights.Bold;
                txtBlock2.Text = "ME";
                txtBlock3.FontWeight = FontWeights.Bold;
            }

            Grid.SetColumn(txtBlock0, 0);
            Grid.SetColumn(txtBlock1, 1);
            Grid.SetColumn(txtBlock2, 2);
            Grid.SetColumn(txtBlock3, 3);
            Grid.SetColumn(txtBlock4, 4);

            DynamicGrid.Children.Add(txtBlock0);
            DynamicGrid.Children.Add(txtBlock1);
            DynamicGrid.Children.Add(txtBlock2);
            DynamicGrid.Children.Add(txtBlock3);
            DynamicGrid.Children.Add(txtBlock4);
            return DynamicGrid;
        }

        private Grid CreateGrid()
        {
            Grid DynamicGrid = new Grid();
            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(100);
                DynamicGrid.ColumnDefinitions.Add(columnDefinition);
            }

            return DynamicGrid;
        }
    }
}
