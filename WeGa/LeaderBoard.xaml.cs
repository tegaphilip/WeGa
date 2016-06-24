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
        Boolean clicked = false;
        private ServiceClient sc;
        Dictionary<String, double> listOfScore;
        private string filterType = "Total";

        public LeaderBoard()
        {
            InitializeComponent();
            sc = new ServiceClient();
            GetAndSetContent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.Source is TabControl)
            {
                this.filterType = ((sender as TabControl).SelectedItem as TabItem).Header as string;
                GetAndSetContent();
            }
        }

        private void GetAndSetContent()
        {
            switch (filterType)
            {
                case "Total":
                    listOfScore = sc.GetLeaderBoard(Constants.TYPE_LEADERBOARD_TOTAL_SCORE);
                    setContent(listOfScore);
                    break;
                case "Average":

                    listOfScore = sc.GetLeaderBoard(Constants.TYPE_LEADERBOARD_AVERAGE_SCORE);
                    setContent(listOfScore);
                    break;
                default:
                    return;
            }
        }

        private void setContent(Dictionary<String, double> listOfScore)
        {
            if (listOfScore == null)
            {
                MessageBox.Show("Problem arises when trying to get the scores");
            }
            else
            {
                CreateTitleRow();
                if (filterType == "Average")
                {
                    var element = (Button)average.Children.Cast<UIElement>().FirstOrDefault(x => Grid.GetColumn(x) == 1 && Grid.GetRow(x) == 0);
                    element.Content = "Average Score";
                }
                foreach (KeyValuePair<String, double> p in listOfScore)
                {
                    var grid = GetChosenGrid();   
                    TextBlock nickname = new TextBlock();                  
                    nickname.Text = p.Key;
                    TextBlock score = new TextBlock();
                    score.Text = p.Value.ToString();
                
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(25);
                    grid.RowDefinitions.Add(row);

                    Grid.SetColumn(nickname, 0);
                    Grid.SetColumn(score, 1);
                    Grid.SetRow(nickname, grid.RowDefinitions.Count - 1);
                    Grid.SetRow(score, grid.RowDefinitions.Count - 1);

                    grid.Children.Add(nickname);
                    grid.Children.Add(score);
                   
                }
            }
        }

        private void CreateTitleRow()
        {
            var grid = GetChosenGrid();
            grid.RowDefinitions.Clear();
            grid.Children.Clear();

            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(25);
            grid.RowDefinitions.Add(row);

            Button nickname = new Button();
            nickname.Content = "Nickname";
            nickname.Click += btn_Clicked;

            Button totalScore = new Button();
            totalScore.Content = "Total Score";
            totalScore.Click += btn_Clicked;

            Grid.SetRow(nickname, 0);
            Grid.SetRow(totalScore, 0);
            Grid.SetColumn(nickname, 0);
            Grid.SetColumn(totalScore, 1);

            grid.Children.Add(nickname);
            grid.Children.Add(totalScore);
        }

        private void btn_Clicked(object sender, RoutedEventArgs e)
        {
            Button btn_Clicked = (Button)sender;
            var sorted = (IOrderedEnumerable<KeyValuePair<string,double>>)null;
            if (btn_Clicked.Content.ToString() == "Nickname")
            {
                sorted = listOfScore.OrderByDescending(x => x.Key);
                if (clicked)
                {
                    sorted = listOfScore.OrderBy(x => x.Key);
                }
            }
            else if (btn_Clicked.Content.ToString() == "Total Score" || btn_Clicked.Content.ToString() == "Average Score")
            {
                sorted = listOfScore.OrderByDescending(x => x.Value);
                if (clicked)
                {
                    sorted = listOfScore.OrderBy(x => x.Value);
                }
            }
            listOfScore = sorted.ToDictionary(pair => pair.Key, pair => pair.Value);
            setContent(listOfScore);
            clicked = !clicked;
        }

        private Grid GetChosenGrid()
        {
            var grid = total as Grid;
            if (this.filterType == "Total")
            {
                grid = total as Grid;
            }
            else
            {
                grid = average as Grid;
            }
            return grid;
        }

    }
}
