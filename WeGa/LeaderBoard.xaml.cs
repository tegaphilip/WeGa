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
            
            switch (tabItem)
            {
                case "Total":
                    listOfScore = sc.GetLeaderBoard("t");
                    break;
                case "Average":
                    listOfScore = sc.GetLeaderBoard("a");
                    break;
                default:
                    return;
            }

            if (listOfScore == null)
            {
                MessageBox.Show("Problem arises when trying to get the scores");
            }
            else
            {
                foreach (KeyValuePair<String, double> p in listOfScore)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(60);
                    TotalTab.RowDefinitions.Add(row);
                    //TotalTab.Children.Add(row);
                    
                    
                }
            }
        }
    }
}
