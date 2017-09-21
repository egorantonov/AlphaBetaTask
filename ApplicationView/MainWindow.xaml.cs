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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlphaBetaPruning;

namespace ApplicationView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int size = 15;
        private int winLength = 5;
        private int depth = 5;


        public MainWindow()
        {
            InitializeComponent();
            NewGameStart(size,winLength,depth);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            var newGameWindow = new NewGameWindow();
            if (newGameWindow.ShowDialog() == true)
            {
                this.size = newGameWindow.Size;
                this.winLength = newGameWindow.WinLength;
                this.depth = newGameWindow.Depth;
            }

            NewGameStart(size, winLength, depth);

        }

        private void NewGameStart(int size, int winLength, int depth)
        {

            

            BattleField.Children.Clear();
            BattleField.Rows = BattleField.Columns = size;
            for (int i = 0; i < (size * size); i++)
            {
                BattleField.Children.Add(new Rectangle() { Fill = Brushes.LightGray, Margin = new Thickness(0.5) });
            }
        }

        private void NewGameStart(int[,] field, int winLength)
        {

        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
