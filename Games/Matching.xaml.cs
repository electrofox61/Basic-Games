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

namespace Games
{
    /// <summary>
    /// Interaction logic for Matching.xaml
    /// </summary>
    public partial class Matching : Page
    {
        public Matching()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            List<char> icons = new List<char>();
            while (icons.Count != 30)
            {
                int karakter = (int)r.Next(0, 24);
                if (!icons.Contains((char)karakter))
                { 
                icons.Add((char)karakter);
                icons.Add((char)karakter);
                }
            }
            for (int i = 0; i < MatchingGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < MatchingGrid.RowDefinitions.Count; j++)
                {
                    char icon = icons[r.Next(0,icons.Count)];
                    Button b = new Button();
                    b.Content = icons[icon];
                    icons.RemoveAt(icon);
                    b.Width = 80;
                    b.Height = 80;
                    Grid.SetColumn(b, i);
                    Grid.SetRow(b, j);
                    MatchingGrid.Children.Add(b);
                }
            }
        }
    }
}
