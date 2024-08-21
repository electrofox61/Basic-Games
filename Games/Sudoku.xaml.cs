using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Sudoku.xaml
    /// </summary>
    public partial class Sudoku : Page
    {
        int[,] Matrix = new int[9, 9];
        int jodoboz = 0;
        bool RosszDoboz = false;
        public Sudoku()
        {
            InitializeComponent();
        }

        public void Letrehozas()
        {
            RosszDoboz = false;
            Random r = new Random();
            for (int i = 0; i < SudokuGrid.RowDefinitions.Count; i++)
            {
                List<int> oszlop = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                for (int j = 0; j < SudokuGrid.ColumnDefinitions.Count; j++)
                {
                    Matrix[i, j] = 0;
                    bool vege = false;
                    while (!vege)
                    {
                        bool rossz = false;
                        int szam = r.Next(1, oszlop.Max() + 1);
                        if (oszlop.Contains(szam))
                        {
                            oszlop.Remove(szam);
                            Matrix[i, j] = szam;
                            if (i != 0)
                            {
                                for (int n = i - 1; n > -1; n--)
                                {
                                    if (Matrix[n, j] == szam)
                                    {
                                        rossz = true;
                                    }
                                }
                            }
                            if (rossz)
                            {
                                oszlop = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                                for (int m = 0; m < SudokuGrid.ColumnDefinitions.Count; m++)
                                {
                                    Matrix[i, m] = 0;
                                }
                                j = 0;
                                rossz = false;

                            }
                            else
                            {
                                vege = true;
                            }
                        }
                    }
                }
            }
        }
        public bool DobozJo(int[,] doboz)
        {
            List<int> szamok = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (szamok.Contains(doboz[i,j]))
                    {
                        szamok.Remove(doboz[i, j]);
                    }
                    else
                    {
                        jodoboz = 0;
                        return false;
                    }
                }
            }
            jodoboz++;
            return true;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            while (jodoboz < 9)
            {
                Letrehozas();
                int[,] EllenorzoMatrix = new int[3, 3];
                for (int i = 0; i < SudokuGrid.RowDefinitions.Count; i++)
                {
                    for (int j = 0; j < SudokuGrid.ColumnDefinitions.Count; j++)
                    {
                        EllenorzoMatrix[i % 3, j % 3] = Matrix[i, j];
                        if (i % 3 == 2 && j % 3 == 2)
                        {
                            if (!DobozJo(EllenorzoMatrix))
                            {
                                RosszDoboz = true;
                                break;
                            }
                            EllenorzoMatrix = new int[3, 3];
                        }
                    }
                    if (RosszDoboz)
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < SudokuGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < SudokuGrid.ColumnDefinitions.Count; j++)
                {
                    Button b = new Button();
                    b.Content = Matrix[i,j];
                    b.Width = 45;
                    b.Height = 45;
                    Grid.SetColumn(b, i);
                    Grid.SetRow(b, j);
                    SudokuGrid.Children.Add(b);
                }
            }
        }
    }
}

