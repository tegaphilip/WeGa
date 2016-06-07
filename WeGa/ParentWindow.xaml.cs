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
    /// Interaction logic for ParentWindow.xaml
    /// </summary>
    public partial class ParentWindow : Window
    {
        public static ParentWindow parentWindow;

        public ParentWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ParentWindow getParentWindow()
        {
            if (parentWindow == null)
            {
                parentWindow = new ParentWindow();
            }
            
            return parentWindow;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            startGame();
        }


        private void startGame()
        {
            RequestWindow rw = new RequestWindow
            {
                ShowInTaskbar = false,               // don't show the dialog on the taskbar
                Topmost = true,                      // ensure we're Always On Top
                ResizeMode = ResizeMode.NoResize,    // remove excess caption bar buttons
                Owner = ParentWindow.getParentWindow(),
            };
            rw.Left = (Utils.getScreenWidth() / 2) - (this.Width / 2);
            rw.Top = (Utils.getScreenHeight() / 2) - (this.Height / 2);
            rw.ShowDialog();
        }
    }
}
