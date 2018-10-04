using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Miny
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int difficulty = 1;
        int cols = 10;
        int rows = 10;
        int[,] playGround = new int[10, 10];
        public MainWindow()
        {
            InitializeComponent();
            generateGrid(cols, rows);

            generatePlayGround();

            renderGrid();


            Board.ShowGridLines = true;
        }
        void generatePlayGround()
        {
            int bombs = difficulty * 10;

            playGround[0, 0] = 10;
            playGround[0, 5] = 10;
            playGround[0, 4] = 10;
            playGround[0, 3] = 10;
            playGround[9, 2] = 10;
            /*while (bombs>0)
            {

            }*/
        }
        void renderGrid()
        {
            Board.Children.Clear();
            for (int _row = 0; _row < rows; _row++)
            {
                for (int _col = 0; _col < cols; _col++)
                {
                    Button button = new Button();
                    button.FontSize = 14;
                    button.VerticalAlignment = VerticalAlignment.Stretch;
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.FontWeight = FontWeights.Bold;

                    //BOMB
                    if (playGround[_col, _row] == 9)
                    {
                        button.Content = "X";
                    }
                    //EMPTY
                    if (playGround[_col, _row] == 10)
                    {
                        button.Content = "";
                    }
                    //BOMB NEAR
                    else if (playGround[_col, _row] > 0 && playGround[_col, _row] < 9)
                    {
                        button.Content = playGround[_col, _row].ToString();
                    }
                    //THIS IS NEODHALENO
                    else
                    {
                        button.Content = "Nevim";
                    }
                    button.Click += clickButton;
                    Grid.SetRow(button, _row);
                    Grid.SetColumn(button, _col);
                    Board.Children.Add(button);
                }
            }
        }

        int countBombs(int[,] playGround, int _col, int _row)
        {
            int bombCounter = 0;

            // LEFT
            if (_col > 1)
            {
                if (playGround[_col - 1, _row] == 9) bombCounter++;
            }
            // LEFT DIAGONAL UP
            if (_col > 1 && _row > 1)
            {
                if (playGround[_col - 1, _row - 1] == 9) bombCounter++;
            }
            // UP
            if (_row > 1)
            {
                if (playGround[_col, _row - 1] == 9) bombCounter++;
            }
            // RIGHT
            if (_col < cols - 1)
            {
                if (playGround[_col + 1, _row] == 9) bombCounter++;
            }
            // RIGHT DIAGONAL UP
            if (_col < cols - 1 && _row > 1)
            {
                if (playGround[_col + 1, _row - 1] == 9) bombCounter++;
            }
            // DOWN
            if (_row < rows - 1)
            {
                if (playGround[_col, _row + 1] == 9) bombCounter++;
            }
            // LEFT DIAGONAL DOWN
            if (_col > 1 && _row < rows - 1)
            {
                if (playGround[_col - 1, _row + 1] == 9) bombCounter++;
            }
            // RIGHT DIAGONAL DOWN
            if (_col < cols - 1 && _row < rows - 1)
            {
                if (playGround[_col + 1, _row + 1] == 9) bombCounter++;
            }

            return bombCounter;

        }

        void checkClick(int row, int col)
        {
            // BOMB CLICK
            if (playGround[col, row] == 9)
            {
                // LOST GAME
            };
            // NEODHALENO FIELD CLICK
            if (playGround[col, row] == 0)
            {
                int _col = col;
                int _row = row;

                while (true)
                {
                    // countBombs and reveal empty fields
                }
            };
            renderGrid();
        }

        void clickButton(object sender, RoutedEventArgs e)
        {
            checkClick(Grid.GetRow((Button)sender), Grid.GetColumn((Button)sender));
        }

        void generateGrid(int cols, int rows)
        {
            Board.RowDefinitions.Clear();
            Board.ColumnDefinitions.Clear();
            for (int _cols = 0; _cols < cols; _cols++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int _rows = 0; _rows < rows; _rows++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }
        }

    }
}
