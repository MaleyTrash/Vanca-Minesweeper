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

namespace Miny
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int difficulty = 1;
        int rows = 10;
        int cols = 10;
        

        public MainWindow()
        {
            InitializeComponent();
            generateGrid(cols, rows);

            TextBlock txtBlock1 = new TextBlock();
            txtBlock1.Text = "B";
            txtBlock1.FontSize = 14;
            txtBlock1.FontWeight = FontWeights.Bold;
            
            txtBlock1.VerticalAlignment = VerticalAlignment.Center;
            txtBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(txtBlock1, 1);
            Grid.SetColumn(txtBlock1, 1);
            Board.Children.Add(txtBlock1);
            Board.ShowGridLines = true;
        }
        void generatePlayGround()
        {
            int bombs = difficulty * 10;

            while (bombs>0)
            {

            }
        }
        void generateGrid(int cols, int rows)
        {
            for (int _rows = 0; _rows < rows; _rows++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }
            for (int _cols = 0; _cols < cols; _cols++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        
    }
}
