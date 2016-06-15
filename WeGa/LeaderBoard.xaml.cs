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
    /// Interaction logic for LeaderBoard.xaml
    /// </summary>
    public partial class LeaderBoard : Window
    {
        Dictionary<String, double> listOfScore;

        public LeaderBoard()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceClient sc = new ServiceClient();
            String tabItem = ((sender as TabControl).SelectedItem as TabItem).Header as string;

            label.Content = "not break until TAB";

            switch (tabItem)
            {
                case "Total":
                    //listOfScore = sc.GetLeaderBoard(Constants.TYPE_LEADERBOARD_TOTAL_SCORE);
                    //setContent(listOfScore, Constants.TYPE_LEADERBOARD_TOTAL_SCORE);

                    label.Content = "not break until TOTAl";
                    ListBoxItem lbt_nickname = new ListBoxItem();
                    lbt_nickname.Content = "really added this item";
                    total_nickname.Items.Add(lbt_nickname);
                    
                    break;
                case "Average":
                    listOfScore = sc.GetLeaderBoard(Constants.TYPE_LEADERBOARD_AVERAGE_SCORE);
                    setContent(listOfScore, Constants.TYPE_LEADERBOARD_AVERAGE_SCORE);
                    break;
                default:
                    return;
            }
        }

        private void setContent(Dictionary<String, double> listOfScore, string type)
        {
            if (listOfScore == null)
            {
                MessageBox.Show("Problem arises when trying to get the scores");
            }
            else
            {
                foreach (KeyValuePair<String, double> p in listOfScore)
                {
                    ListBoxItem lbt_nickname = new ListBoxItem();
                    lbt_nickname.Content = "really added this item";
                    ListBoxItem lbt_score = new ListBoxItem();
                    lbt_score.Content = p.Value;

                    if (type == Constants.TYPE_LEADERBOARD_TOTAL_SCORE)
                    {
                        total_nickname.Items.Add(lbt_nickname);
                        total_score.Items.Add(lbt_score);
                    }
                    else
                    {
                        avg_nickname.Items.Add(lbt_nickname);
                        avg_score.Items.Add(lbt_score);
                    }
                }

            }
        }
    }
}
