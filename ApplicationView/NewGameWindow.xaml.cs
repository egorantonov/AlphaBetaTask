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

namespace ApplicationView
{
    /// <summary>
    /// Логика взаимодействия для NewGameWindow.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        public int Depth
        {
            get { return (int)DepthSlider.Value; }
        }

        public int WinLength
        {
            get { return (int)WinSlider.Value; }
        }

        public int Size
        {
            get { return (int)FieldSizeSlider.Value; }
        }

        public NewGameWindow()
        {
            InitializeComponent();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }


    }
}
