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

namespace krestikinoliki
{
  
    public partial class MainWindow : Window
    {
        private bool isPlayerX = true;
        private string[,] board = new string[3, 3];
        private Random random = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);

            if (!string.IsNullOrEmpty(board[row, column]))
                return;

            board[row, column] = isPlayerX ? "X" : "O";
            button.Content = board[row, column];

            if (CheckForWinner() || CheckForDraw())
            {
                ResetGame();
            }

            isPlayerX = !isPlayerX;

            if (!isPlayerX)
            {
                MakeComputerMove();
            }
        }

        private void MakeComputerMove()
        {
            int row, column;

            do
            {
                row = random.Next(0, 3);
                column = random.Next(0, 3);
            } while (!string.IsNullOrEmpty(board[row, column]));

            board[row, column] = "O";
            Button computerButton = (Button)FindName($"button{row}{column}");
            computerButton.Content = "O";

            if (CheckForWinner() || CheckForDraw())
            {
                ResetGame();
            }

            isPlayerX = !isPlayerX;
        }

        private bool CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(board[i, 0]) && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                {
                    HighlightWinnerCells(i, 0, i, 1, i, 2);
                    MessageBox.Show($"Player {(isPlayerX ? "X" : "O")} wins!");
                    return true;
                }

                if (!string.IsNullOrEmpty(board[0, i]) && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    HighlightWinnerCells(0, i, 1, i, 2, i);
                    MessageBox.Show($"Player {(isPlayerX ? "X" : "O")} wins!");
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(board[0, 0]) && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                HighlightWinnerCells(0, 0, 1, 1, 2, 2);
                MessageBox.Show($"Player {(isPlayerX ? "X" : "O")} wins!");
                return true;
            }

            if (!string.IsNullOrEmpty(board[0, 2]) && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                HighlightWinnerCells(0, 2, 1, 1, 2, 0);
                MessageBox.Show($"Player {(isPlayerX ? "X" : "O")} wins!");
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (string.IsNullOrEmpty(board[i, j]))
                        return false;

            MessageBox.Show("Draw!");
            return true;
        }

        private void HighlightWinnerCells(int row1, int col1, int row2, int col2, int row3, int col3)
        {
            Button button1 = (Button)FindName($"button{row1}{col1}");
            Button button2 = (Button)FindName($"button{row2}{col2}");
            Button button3 = (Button)FindName($"button{row3}{col3}");

            button1.Background = Brushes.Yellow;
            button2.Background = Brushes.Yellow;
            button3.Background = Brushes.Yellow;
        }

        private void ResetGame()
        {
            isPlayerX = true;
            board = new string[3, 3];

            foreach (var button in ((Grid)Content).Children)
            {
                if (button is Button)
                {
                    Button b = (Button)button;
                    b.Content = "";
                    b.Background = Brushes.White;
                }
            }
        }
    }
}
