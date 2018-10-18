using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        bool lost = false;
        bool won = false;
        bool firstClick = false;
        int flags;
        int[,] playGround = new int[10, 10];
        public MainWindow()
        {
            InitializeComponent();
            Announce.Foreground = Brushes.Black;
            Announce.Content = "Game has started!";
            Board.Children.Clear();
            generateGrid(cols, rows);
            renderGrid();
            Board.ShowGridLines = true;
        }
        void startGame(int _cols, int _rows)
        {
            Announce.Foreground = Brushes.Black;
            Announce.Content = "Game has started!";
            playGround = new int[cols, rows];
            lost = false;
            won = false;
            firstClick = false;
            Board.Children.Clear();
            generateGrid(cols, rows);
            renderGrid();
            Board.ShowGridLines = true;
        }
        void generatePlayGround(int col = 0, int row = 0)
        {
            int bombs = difficulty * 10;
            flags = difficulty * 10;
            Random rand = new Random();
            while (bombs > 0)
            {
                for (int _row = 0; _row < rows; _row++)
                {
                    for (int _col = 0; _col < cols; _col++)
                    {
                        if (rand.Next(0, 80) == 10 && playGround[_col, _row] != 9)
                        {
                            if (col != _col && col - 1 != col && col - 1 != col && row != _row && row - 1 != _row && row + 1 != _row)
                            {
                                if (bombs > 0)
                                {
                                    playGround[_col, _row] = 9;
                                    bombs--;
                                }
                            }

                        }
                    }
                }
            }
        }
        void renderGrid()
        {
            Board.Children.Clear();
            int empty = 0;
            int flaggedBombs = 0;
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
                        button.Content = "";
                        button.Background = Brushes.DarkGray;
                    }
                    //EXPLODED BOMB
                    if (playGround[_col, _row] == 31)
                    {
                        button.Content = "X";
                        button.Background = Brushes.Red;
                    }
                    //FLAG NUMBER
                    else if (playGround[_col, _row] >= 11 && playGround[_col, _row] <= 18)
                    {
                        button.Content = "F";
                        button.Foreground = Brushes.Red;
                        button.Background = Brushes.DarkGray;
                    }
                    //QUESTIONMARK NUMBER
                    else if (playGround[_col, _row] >= 21 && playGround[_col, _row] <= 28)
                    {
                        button.Content = "?";
                        button.Foreground = Brushes.Red;
                        button.Background = Brushes.DarkGray;
                    }
                    //QUESTIONMARK BOMB
                    else if (playGround[_col, _row] == 29)
                    {
                        button.Content = "?";
                        button.Foreground = Brushes.Red;
                        button.Background = Brushes.DarkGray;
                    }
                    //FLAG BOMB
                    else if (playGround[_col, _row] == 19)
                    {
                        button.Content = "F";
                        button.Foreground = Brushes.Red;
                        button.Background = Brushes.DarkGray;
                        flaggedBombs++;
                    }
                    //FLAG EMPTY
                    else if (playGround[_col, _row] == 20)
                    {
                        button.Content = "F";
                        button.Foreground = Brushes.Red;
                        button.Background = Brushes.DarkGray;
                    }
                    //QUESTIONMARK EMPTY
                    else if (playGround[_col, _row] == 30)
                    {
                        button.Content = "?";
                        button.Foreground = Brushes.Red;
                        button.Background = Brushes.DarkGray;
                    }
                    //EMPTY
                    else if (playGround[_col, _row] == 10)
                    {
                        button.Content = "";
                        button.Background = Brushes.LightGray;
                    }
                    //BOMB NEAR
                    else if (playGround[_col, _row] > 0 && playGround[_col, _row] < 9)
                    {
                        button.Content = playGround[_col, _row].ToString();
                        button.Background = Brushes.LightGray;
                    }
                    //THIS IS NEODHALENO = 0
                    else if (playGround[_col, _row] == 0)
                    {
                        button.Content = "";
                        button.Background = Brushes.DarkGray;
                        empty++;
                    }
                    button.Click += clickButtonLeft;
                    button.MouseRightButtonUp += clickButtonRight;
                    Grid.SetRow(button, _row);
                    Grid.SetColumn(button, _col);
                    Board.Children.Add(button);
                }
            }

            if (empty == 0 && flaggedBombs == difficulty * 10)
            {
                won = true;
                Announce.Content = "You Won!";
                Announce.Foreground = Brushes.Green;
            }
            Flags.Content = "Flags: " + flags;
        }

        int countBombs(int[,] playGround, int _col, int _row)
        {
            int bombCounter = 0;

            // LEFT
            if (_col > 0)
            {
                if (playGround[_col - 1, _row] % 10 == 9) bombCounter++;
            }
            // LEFT DIAGONAL UP
            if (_col > 0 && _row > 0)
            {
                if (playGround[_col - 1, _row - 1] % 10 == 9) bombCounter++;
            }
            // UP
            if (_row > 0)
            {
                if (playGround[_col, _row - 1] % 10 == 9) bombCounter++;
            }
            // RIGHT
            if (_col < cols - 1)
            {
                if (playGround[_col + 1, _row] % 10 == 9) bombCounter++;
            }
            // RIGHT DIAGONAL UP
            if (_col < cols - 1 && _row > 0)
            {
                if (playGround[_col + 1, _row - 1] % 10 == 9) bombCounter++;
            }
            // DOWN
            if (_row < rows - 1)
            {
                if (playGround[_col, _row + 1] % 10 == 9) bombCounter++;
            }
            // LEFT DIAGONAL DOWN
            if (_col > 0 && _row < rows - 1)
            {
                if (playGround[_col - 1, _row + 1] % 10 == 9) bombCounter++;
            }
            // RIGHT DIAGONAL DOWN
            if (_col < cols - 1 && _row < rows - 1)
            {
                if (playGround[_col + 1, _row + 1] % 10 == 9) bombCounter++;
            }

            return bombCounter;

        }

        void showNotShown(int col, int row)
        {
            if (col > 0)
            {
                int nextCol = col - 1;
                int nextRow = row;
                int count = countBombs(playGround, nextCol, nextRow);
                if (count == 0)
                {
                    if (playGround[nextCol, nextRow] == 0)
                    {
                        playGround[nextCol, nextRow] = 10;
                        showNotShown(nextCol, nextRow);
                    }
                }
                else if (playGround[nextCol, nextRow] != 9)
                {
                    playGround[nextCol, nextRow] = count;
                }
            }
            if (col < cols-1)
            {
                int nextCol = col + 1;
                int nextRow = row;
                int count = countBombs(playGround, nextCol, nextRow);
                if (count == 0)
                {
                    if (playGround[nextCol, nextRow] == 0)
                    {
                        playGround[nextCol, nextRow] = 10;
                        showNotShown(nextCol, nextRow);
                    }

                }
                else if (playGround[nextCol, nextRow] != 9)
                {
                    playGround[nextCol, nextRow] = count;
                }
            }
            if (row > 0)
            {
                int nextCol = col;
                int nextRow = row - 1;
                int count = countBombs(playGround, nextCol, nextRow);
                if (count == 0)
                {
                    if (playGround[nextCol, nextRow] == 0)
                    {
                        playGround[nextCol, nextRow] = 10;
                        showNotShown(nextCol, nextRow);
                    }
                }
                else if (playGround[nextCol, nextRow] != 9)
                {
                    playGround[nextCol, nextRow] = count;
                }
            }
            if (row < rows-1)
            {
                int nextCol = col;
                int nextRow = row + 1;
                int count = countBombs(playGround, col, row + 1);
                if (count == 0)
                {
                    if (playGround[nextCol, nextRow] == 0)
                    {
                        playGround[nextCol, nextRow] = 10;
                        showNotShown(nextCol, nextRow);
                    }
                }
                else if (playGround[nextCol, nextRow] != 9)
                {
                    playGround[nextCol, nextRow] = count;
                }
            }
        }

        void checkClick(int col, int row)
        {
            int value = playGround[col, row];

            // CHECK FLAGS
            if (value >= 11 && value <= 20)
            {
                playGround[col, row] = value % 10;
                value = value % 10;
                flags++;
            }

            // BOMB CLICK
            if (value == 9)
            {
                Announce.Foreground = Brushes.Red;
                Announce.Content = "You lost!";
                playGround[col, row] = 31;
            }

            // NEODHALENO FIELD CLICK
            else if (value == 0)
            {
                int count = countBombs(playGround, col, row);
                if (count == 0)
                {
                    showNotShown(col, row);
                    playGround[col, row] = 10;
                }
                else playGround[col, row] = count;
            }
            // FLAG CLICK
            renderGrid();
        }

        void placeFlag(int col, int row)
        {
            if (flags > 0)
            {
                if (playGround[col, row] == 9)
                {
                    playGround[col, row] = 19;
                    flags--;
                }
                else if (playGround[col, row] == 0)
                {
                    playGround[col, row] = 20;
                    flags--;
                }
                else if (countBombs(playGround, col, row) > 0 && playGround[col, row] == 0)
                {
                    playGround[col, row] = countBombs(playGround, col, row) + 10;
                    flags--;
                }
            }
            renderGrid();
        }

        void placeQuestionMark(int col, int row)
        {
            if (playGround[col, row] % 10 == 9)
            {
                playGround[col, row] = 29;
            }
            else if (playGround[col, row] % 10 == 0)
            {
                playGround[col, row] = 30;
            }
            else if (countBombs(playGround, col, row) > 0 && playGround[col, row] == 0)
            {
                playGround[col, row] = countBombs(playGround, col, row) + 20;
            }

        }

        void clickButtonLeft(object sender, RoutedEventArgs e)
        {
            if (!lost && !won)
            {
                if (!firstClick)
                {
                    generatePlayGround(Grid.GetColumn((Button)sender), Grid.GetRow((Button)sender));
                    firstClick = true;
                }
                checkClick(Grid.GetColumn((Button)sender), Grid.GetRow((Button)sender));
            }
            
        }

        void clickButtonRight(object sender, MouseEventArgs e)
        {
            if (!lost && !won)
            {
                int _col = Grid.GetColumn((Button)sender);
                int _row = Grid.GetRow((Button)sender);
                int value = playGround[_col, _row];
                if (value >= 11 && value <= 20)
                {
                    placeQuestionMark(_col, _row);
                    flags++;
                }
                else if (value <= 10)
                {
                    placeFlag(_col, _row);
                }
                else
                {
                    playGround[_col, _row] = value % 10;
                }
                renderGrid();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            startGame(cols, rows);
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

        private void Ez_Click(object sender, RoutedEventArgs e)
        {
            cols = 10;
            rows = 10;
            
            difficulty = 1;
            startGame(cols, rows);
        }
        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            cols = 15;
            rows = 15;
            difficulty = 3;
            startGame(cols, rows);
        }
        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            cols = 25;
            rows = 25;
            difficulty = 8;
            startGame(cols, rows);
        }
    }
}
