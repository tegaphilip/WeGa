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

        int i = 1;
        public GameBoard()
        {
            //DockPanel dp = new DockPanel();
            //StackPanel sp = new StackPanel();

            //Button btn = new Button();
            //Button btn2 = new Button();
            //btn.Content = "Test adding stackpanel in to dockpanel";
            //sp.Children.Add(btn);
            //sp.Children.Add(btn2);

            //DockPanel.SetDock(sp, Dock.Left);
            InitializeComponent();
        }


        //Get the letter by the letter_button's content, set to content of the first empty word_button 
        private void btn_Letter_Click(object sender, RoutedEventArgs e)
        { 
            Button btn_clicked = (Button)sender;
            char letter =  char.Parse(btn_clicked.Content.ToString());
            
            //set letter to word_button
            var list = this.wordPanel.Children;
            foreach(UIElement elm in list) {
                Button btn_to_change = (Button)elm;
                if (btn_to_change.Content.Equals(""))
                {
                    btn_to_change.Content = letter;
                    break;
                }
            }
        }

        private void btn_word_Click(object sender, RoutedEventArgs e)
        {
            Button btn_clicked = (Button)sender;
            btn_clicked.Content = "";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Commented to see results after click;

            //message.Content = "";
            //Check if is empty
            if (isEmpty())
            {
                message.Content += "From btnAdd_Click: You haven't enter any words!\n";
            }
            else
            {
                //Check if is a singel letter
                if (isSingleLetter())
                {
                    message.Content += "From btnAdd_Click: Word with a single letter dosn't count! Buddy! \n";
                }
                else
                {
                    //Need to delete at the end
                    message.Content += "Not empty \n";
                    message.Content += "The new WORD is " + getNewWord() + i++ +"\n";

                    Button newWord = new Button();
                    newWord.Height = 30;
                    newWord.Width = 40;
                    newWord.Content = getNewWord();

                    RowDefinition newRow = new RowDefinition();
                    newRow.Height = new GridLength(50);
                    wordList.RowDefinitions.Add(newRow);
                    wordList.Children.Add(newWord);

                     message.Content += wordList.Children.Count.ToString() + "\n";

                     Grid.SetRow(newWord, wordList.Children.Count);
                     Grid.SetColumn(newWord, 0);
                    
                }
 
            }
        }

        //Reset all letters chosed.
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            var btns = wordPanel.Children;
            foreach (Button btn in btns)
            {
                btn.Content = "";
            }
        }

        //Check if the word to be added is empty.
        private bool isEmpty()
        {
            Button element = wordPanel.Children.Cast<Button>().FirstOrDefault(e => Grid.GetColumn(e) == 0 && Grid.GetRow(e) == 0);    
            //Need to be deleted at the end.
            if (element.Content.Equals("") || element == null)
            {
                return true;
            }
            return false;
        }

        //Check if the word to be added is a single letter.
        private bool isSingleLetter()
        {
            Button element = wordPanel.Children.Cast<Button>().FirstOrDefault(e => Grid.GetColumn(e) == 1 && Grid.GetRow(e) == 0);
            if (!isEmpty()) {
                if (element.Content.Equals("") ||element == null)
                    return true;
                else
                    return false;
            }        
            return false;
        }

        //Concatenate the letters to a word.
        private string getNewWord()
        {
            string newWord ="New+";
            var list = this.wordPanel.Children;
            foreach (UIElement elm in list)
            {
                Button btn_to_change = (Button) elm;
                if (!btn_to_change.Content.Equals(""))
                    newWord += btn_to_change.Content;
                else
                    break;
            }
            return newWord;
        }

        private bool isPlayed(string wordSubmited)
        {
            var list = this.wordList.Children;
            foreach (UIElement elm in list)
            {
                Button btn = (Button) elm;
                if (!btn.Content.Equals(wordList))
                    return true;
                else
                    break;
            } 
            return false;
        }
    }
}
