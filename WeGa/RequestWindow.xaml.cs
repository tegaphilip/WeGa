﻿using System;
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

        public RequestWindow()
        {
            sc = new ServiceClient();
            InitializeComponent();
            setPlayerList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gameLetters = Utils.getLetters();
            Application.Current.Resources["gameLetters"] = gameLetters;
            sc.CreateGame((String)Application.Current.Resources["nickname"], receivedNickName, gameLetters);

            this.Close();
            Window gb = new GameBoard();
            gb.Show();

        }

        private void setPlayerList()
        {
            nickNameList = sc.GetPlayerNicknames();
            nickNameList.Remove((string)Application.Current.Resources["nickname"]);
            foreach (string nm in nickNameList)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = nm;
                
                playerList.Items.Add(item);
            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

    }
}
