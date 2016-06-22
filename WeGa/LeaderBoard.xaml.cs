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

        private ServiceClient sc;
        Dictionary<String, double> listOfScore;
        private string filterType = "Average";

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
                    //listOfScore = from entry in listOfScore orderby entry.Value ascending select entry;
                    //List<Order> SortedList = objListOrder.OrderBy(o=>o.OrderDate).ToList();
                    //var sort = dicts.OrderBy(x => x.ContainsKey("Title") ? x["Title"] : string.Empty);
                    //from entry in myDict orderby entry.Value ascending select entry;
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
                foreach (KeyValuePair<String, double> p in listOfScore.OrderByDescending(x=>x.Value))
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
            nickname.Click += nickname_Click;
            TextBlock totalScore = new TextBlock();
            totalScore.Text = "Total Score";

            Grid.SetRow(nickname, 0);
            Grid.SetRow(totalScore, 0);
            Grid.SetColumn(nickname, 0);
            Grid.SetColumn(totalScore, 1);

            grid.Children.Add(nickname);
            grid.Children.Add(totalScore);
        }

        private void nickname_Click(object sender, RoutedEventArgs e)
        {
            var sorted = listOfScore.OrderBy(x => x.Key);
            listOfScore = (Dictionary<string,double>) sorted;
            setContent(listOfScore);
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
