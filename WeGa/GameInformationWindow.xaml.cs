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

namespace WeGa
{
    /// <summary>
    /// Interaction logic for GameInformationWindow.xaml
    /// </summary>
    public partial class GameInformationWindow : Window
    {
        private Dictionary<string, string[]> details;
        private String player1Nickname = "", player2Nickname = "";
        int player1Total = 0, player2Total = 0;

        public GameInformationWindow(Dictionary<string, string[]> details, string letters)
        {
            InitializeComponent();
            this.details = details;
            GameLetters.Content = "LETTERS:  " + letters.ToUpper();
            BuildGrid();
        }

        private void BuildGrid()
        {
            if (details == null)
            {
                MessageBox.Show("This game currently contains no information");
                return;
            }

            List<string> keyList = new List<string>(this.details.Keys);
            int i = 0;
            foreach (string key in keyList)
            {
                if (i == 0)
                {
                    player1.Content = key.ToUpper();
                    player1Nickname = key;
                }
                else
                {
                    player2.Content = key.ToUpper();
                    player2Nickname = key;
                }

                this.Title += key;
                if (i == 0)
                {
                    this.Title += " VERSUS ";
                }
                i++;
            }

            string[] player1Words = this.details[player1Nickname];
            string[] player2Words = this.details[player2Nickname];

            for (int count = 0; count < Math.Max(player1Words.Length, player2Words.Length); count++)
            {
                Grid DynamicGrid = CreateGrid();

                TextBlock txtBlock0 = new TextBlock();
                TextBlock txtBlock1 = new TextBlock();
                TextBlock txtBlock2 = new TextBlock();
                TextBlock txtBlock3 = new TextBlock();

                txtBlock0.Text = count < player1Words.Length ? player1Words[count] : "";
                txtBlock1.Text = count < player1Words.Length ? ComputeScore(player1Words[count]).ToString() : "";
                player1Total += count < player1Words.Length ? ComputeScore(player1Words[count]) : 0;
                txtBlock2.Text = count < player2Words.Length ? player2Words[count] : "";
                txtBlock3.Text = count < player2Words.Length ? ComputeScore(player2Words[count]).ToString() : "";
                player2Total += count < player2Words.Length ? ComputeScore(player2Words[count]) : 0;

                Grid.SetColumn(txtBlock0, 0);
                Grid.SetColumn(txtBlock1, 1);
                Grid.SetColumn(txtBlock2, 2);
                Grid.SetColumn(txtBlock3, 3);

                DynamicGrid.Children.Add(txtBlock0);
                DynamicGrid.Children.Add(txtBlock1);
                DynamicGrid.Children.Add(txtBlock2);
                DynamicGrid.Children.Add(txtBlock3);
                GameInfoStack.Children.Add(DynamicGrid);

                //add empty grid
                DynamicGrid = CreateGrid();
                txtBlock0 = new TextBlock();
                txtBlock0.Text = "---------------------------------------------------------------------------------------------------------------------------";
                Grid.SetColumn(txtBlock0, 0);
                Grid.SetColumnSpan(txtBlock0, 4);
                DynamicGrid.Children.Add(txtBlock0);
                GameInfoStack.Children.Add(DynamicGrid);
            }

            Grid lastGrid = CreateGrid();

            TextBlock txtBlock = new TextBlock();
            TextBlock txtBlock4 = new TextBlock();

            txtBlock.Text = player1Total.ToString();
            txtBlock4.Text = player2Total.ToString();

            txtBlock.FontWeight = FontWeights.Bold;
            txtBlock4.FontWeight = FontWeights.Bold;

            Grid.SetColumn(txtBlock, 1);
            Grid.SetColumn(txtBlock4, 3);

            lastGrid.Children.Add(txtBlock);
            lastGrid.Children.Add(txtBlock4);
            GameInfoStack.Children.Add(lastGrid);
        }

        private Grid CreateGrid()
        {
            Grid DynamicGrid = new Grid();
            for (int i = 0; i < 4; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(100);
                DynamicGrid.ColumnDefinitions.Add(columnDefinition);
            }

            return DynamicGrid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static int ComputeScore(String word)
        {
            int score = word.Length * 10;
            if (word.Length == 7)
            {
                score += 50;
            }
            else if (word.Length == 6)
            {
                score += 25;
            }

            return score;
        }
    }
}
