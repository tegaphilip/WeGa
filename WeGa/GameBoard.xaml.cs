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
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Window
    {
        private string letters;

        public GameBoard()
        {
            InitializeComponent();
            letters = (string)Application.Current.Resources["gameLetters"];
            setLettersPanel();
        }


        //Get the letter by the letter_button's content, set to content of the first empty word_button 
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

        //Clear content of btn clicked and set the value back to letters panel
        private void btn_word_Click(object sender, RoutedEventArgs e)
        {
            Button btn_clicked = (Button)sender;
            if (btn_clicked.Content.ToString() != "")
            {
                btn_clicked.Content = "";
                setLettersPanel();
            }
        }


        //There is a method called isNullOrWhiteSpace in c#
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            message.Content = "";
            //Check if is empty and 
            if (!isEmpty() && !isSingleLetter() && !isPlayed(getNewWord()))
            {
                message.Content += "The new WORD is " + getNewWord() + "\n";
                setWord();
                setLettersPanel();
            }
        }

        //Reset all letters chosed.
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            var btns = wordPanel.Children;
            foreach (Button btn in btns)
            {
                btn.Content = "";
            }
            btns = lettersPanel.Children;
            foreach (Button btn in btns)
            {
                btn.Content = letters[i];
                i++;
            }

        }

        //Check if the word to be added is empty.
        private bool isEmpty()
        {
            Button element = wordPanel.Children.Cast<Button>().FirstOrDefault(e => Grid.GetColumn(e) == 0 && Grid.GetRow(e) == 0);
            //Need to be deleted at the end.
            if (element.Content.Equals("") || element == null)
            {
                message.Content += "From btnAdd_Click: You haven't enter any words!\n";
                return true;
            }
            return false;
        }

        //Check if the word to be added is a single letter.
        private bool isSingleLetter()
        {
            Button element = wordPanel.Children.Cast<Button>().FirstOrDefault(e => Grid.GetColumn(e) == 1 && Grid.GetRow(e) == 0);
            if (!isEmpty())
            {
                if (element.Content.Equals("") || element == null)
                {
                    message.Content += "From btnAdd_Click: A single letter dosent count!\n";
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        //Concatenate the letters to a word.
        private string getNewWord()
        {
            string newWord = "";
            var list = this.wordPanel.Children;
            foreach (Button btn in list)
            {
                if (!btn.Content.Equals(""))
                    newWord += btn.Content;
                else
                    break;
            }
            return newWord;
        }

        //Check if the word has been added
        private bool isPlayed(string wordSubmited)
        {
            var list = this.wordList.Children;
            foreach (Button btn in list)
            {
                if (btn.Content.Equals(wordSubmited))
                {
                    message.Content += "You have added this word!";
                    return true;
                }
            }
            return false;
        }

        //Set the new word to word list.
        private void setWord()
        {
            Button newWord = new Button();
            newWord.Height = 30;
            newWord.Width = 50;
            newWord.Content = getNewWord();

            RowDefinition newRow = new RowDefinition();
            newRow.Height = new GridLength(50);
            wordList.RowDefinitions.Add(newRow);
            wordList.Children.Add(newWord);

            message.Content += wordList.Children.Count.ToString() + "\n";

            Grid.SetRow(newWord, wordList.Children.Count);
            Grid.SetColumn(newWord, 0);
        }

        //Set the shuffered letters
        private void setLettersPanel()
        {
            var list = this.lettersPanel.Children;
            int i = 0;
            foreach (Button btn in list)
            {
                //MessageBox.Show(btn.ToString());         
                btn.Content = letters[i];
                i++;
            }
        }
    }
}
