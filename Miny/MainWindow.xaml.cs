﻿using System;
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
        int[,] playGround = new int[10, 10];
        public MainWindow()
        {
            InitializeComponent();
            Announce.Foreground = Brushes.Black;
            Announce.Content = "Game has started!";
            Board.Children.Clear();
            generateGrid(cols, rows);
            generatePlayGround();
            renderGrid();
            Board.ShowGridLines = true;
        }
        void startGame(int _cols, int _rows)
        {
            Announce.Foreground = Brushes.Black;
            Announce.Content = "Game has started!";
            playGround = new int[10, 10];
            Board.Children.Clear();
            generateGrid(cols, rows);
            generatePlayGround();
            renderGrid();
            Board.ShowGridLines = true;
        }
        void generatePlayGround()
        {
            int bombs = difficulty * 20;
            Random rand = new Random();
            while (bombs > 0)
            {                
                for (int _row = 0; _row < rows; _row++)
                {
                    for (int _col = 0; _col < cols; _col++)
                    {
                        if(rand.Next(0, 40) == 10)
                        {
                            playGround[_col, _row] = 9;
                            bombs--;
                        }
                    }
                }
            }
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
                    //FLAG
                    if (playGround[_col, _row] == 11)
                    {
                        button.Content = "F";
                        button.Background = Brushes.Red;
                    }
                    //EMPTY
                    if (playGround[_col, _row] == 10)
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
                    }
                    button.Click += clickButtonLeft;
                    button.MouseRightButtonUp += clickButtonRight;
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
            if (_col > 0)
            {
                if (playGround[_col - 1, _row] == 9) bombCounter++;
            }
            // LEFT DIAGONAL UP
            if (_col > 0 && _row > 0)
            {
                if (playGround[_col - 1, _row - 1] == 9) bombCounter++;
            }
            // UP
            if (_row > 0)
            {
                if (playGround[_col, _row - 1] == 9) bombCounter++;
            }
            // RIGHT
            if (_col < cols - 1)
            {
                if (playGround[_col + 1, _row] == 9) bombCounter++;
            }
            // RIGHT DIAGONAL UP
            if (_col < cols - 1 && _row > 0)
            {
                if (playGround[_col + 1, _row - 1] == 9) bombCounter++;
            }
            // DOWN
            if (_row < rows - 1)
            {
                if (playGround[_col, _row + 1] == 9) bombCounter++;
            }
            // LEFT DIAGONAL DOWN
            if (_col > 0 && _row < rows - 1)
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
            if (col < 9)
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
            if (row < 9)
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
            // BOMB CLICK
            if (playGround[col, row] == 9)
            {
                Announce.Foreground = Brushes.Red;
                Announce.Content = "You lost!";
                //startGame(cols, rows);
            }
            // 
            // Debug.WriteLine(countBombs(playGround, col, row));
            // NEODHALENO FIELD CLICK
            else if (playGround[col, row] == 0)
            {
                int count = countBombs(playGround, col, row);
                if (count == 0) playGround[col, row] = 10;
                else playGround[col, row] = count;
                showNotShown(col, row);
            };
            renderGrid();
        }

        void clickButtonLeft(object sender, RoutedEventArgs e)
        {
            checkClick(Grid.GetColumn((Button)sender), Grid.GetRow((Button)sender));
            renderGrid();
        }
        void clickButtonRight(object sender, MouseEventArgs e)
        {
            // playGround[Grid.GetColumn((Button)sender), Grid.GetRow((Button)sender)] = 11;
            // renderGrid();
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
