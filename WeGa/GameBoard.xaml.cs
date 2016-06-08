using System;
using System.Collections;
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
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Window
    {
        private string letters;
        private String currentPlayer = (String)Application.Current.Resources["nickname"];
        private String opponentName;
        private int gameId;
        private ServiceClient serviceClient;
        private int playerScore = 0;
        ArrayList wordsAlreadyPlayedArrayList = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opponentName"></param>
        /// <param name="gameId"></param>
        public GameBoard(String opponentName, int gameId)
        {
            InitializeComponent();
            this.opponentName = opponentName;
            this.gameId = gameId;
            letters = (string)Application.Current.Resources["gameLetters"];
            this.Title = currentPlayer.ToUpper() + " VS " + opponentName.ToUpper();
            playerButton.Content = currentPlayer.ToUpper();
            opponentButton.Content = opponentName.ToUpper();
            serviceClient = new ServiceClient();
            setLettersPanel();
        }


         
        /// <summary>
        /// Get the letter by the letter_button's content, set to content of the first empty word_button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Letter_Click(object sender, RoutedEventArgs e)
        {
            Button btn_clicked = (Button)sender;
            if (btn_clicked.Content.ToString() != "")
            {
                char letter = char.Parse(btn_clicked.Content.ToString());
                btn_clicked.Content = "";
                //set letter to word_button
                var list = this.wordPanel.Children;
                foreach (Button btn in list)
                {
                    if (btn.Content.Equals(""))
                    {
                        btn.Content = letter;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Clear content of btn clicked and set the value back to letters panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_word_Click(object sender, RoutedEventArgs e)
        {
            Button btn_clicked = (Button)sender;
            if (btn_clicked.Content.ToString() != "")
            {
                resetLetter(char.Parse(btn_clicked.Content.ToString()));
                btn_clicked.Content = "";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            String wordPlayed = getNewWord();
            if (wordPlayed.Distinct().Count() <= 1)
            {
                MessageBox.Show("You must play at least two letters");
                return;
            }

            if (isPlayed(wordPlayed))
            {
                MessageBox.Show("Word previously played");
                return;
            }

            Dictionary<string, string> response = serviceClient.SendWord(wordPlayed, this.gameId, currentPlayer);
            if (response == null || response["status"] == Constants.ERROR)
            {
                String errorMessage = response.ContainsKey("message") ? response["message"] : "an unknown error occurred";
                MessageBox.Show(errorMessage);
                return;
            }

            setWord(wordPlayed);
            int score = int.Parse(response["value"]);
            playerScore += score;
            playerScoreButton.Content = playerScore;
            effectReset();
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        private void effectReset()
        {
            var btns = wordPanel.Children;
            foreach (Button btn in btns)
            {
                btn.Content = "";
            }
            setLettersPanel();
        }

        //Reset all letters chosen.
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            effectReset();
        }

        //Concatenate the letters to a word.
        private string getNewWord()
        {
            string newWord = "";
            var list = this.wordPanel.Children;
            foreach (Button btn in list)
            {
                if (btn.Content.Equals(""))
                {
                    break;
                }
                newWord += btn.Content.ToString();
            }
            return newWord;
        }

        //Check if the word has been added
        private bool isPlayed(string wordSubmited)
        {
            return wordsAlreadyPlayedArrayList.Contains(wordSubmited);
        }

        //Set the new word to word list.
        private void setWord(String word)
        {
            wordsAlreadyPlayedArrayList.Add(word);
            ListBoxItem item = new ListBoxItem();
            item.Content = word;
            wordsAlreadyPlayed.Items.Add(item);
        }

        //Set the shuffered letters
        private void setLettersPanel()
        {
            var list = this.lettersPanel.Children;
            int i = 0;
            foreach (Button btn in list)
            {        
                btn.Content = letters[i];
                i++;
            }
        }

        //Reset a clicked letter
        private void resetLetter(char letter)
        {
            var list = this.lettersPanel.Children;
            int i = 0;
            foreach (Button btn in list)
            {
                if (letters[i] == letter)
                {
                    btn.Content = letters[i];
                }
                i++;
            }
        }
    }
}
