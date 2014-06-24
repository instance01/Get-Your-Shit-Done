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

namespace GetYourShitDone
{
    /// <summary>
    /// Interaktionslogik für Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        MainWindow w;
        bool hidden = false;
        public Start()
        {
            InitializeComponent();
            w = new MainWindow();
            w.Hide();
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - 65;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight - 110;
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            hidden = !hidden;
            if (hidden)
            {
                w.Show();
            }
            else
            {
                w.Hide();
            }
        }
    }
}
