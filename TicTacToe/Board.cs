namespace TicTacToe
{
    class Board
    {
        private char[,] board = new char[3, 3]
        {
            {' ', ' ', ' '},
            {' ', ' ', ' '},
            {' ', ' ', ' '}
        };

        public char GetCell(int row, int column)
        {
            return board[row, column];
        }

        public void SetCell(int row, int column, char c)
        {
            board[row, column] = c;
        }

        public List<(int, int)> GetOpenCells()
        {
            List<(int, int)> openCells = new List<(int, int)>();

            int rows = board.GetLength(0);
            int columns = board.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        openCells.Add((i, j));
                    }
                }
            }

            return openCells;
        }

        public bool CheckVictory(char c) =>
            (board[0, 0] == c && board[0, 1] == c && board[0, 2] == c) ||
            (board[1, 0] == c && board[1, 1] == c && board[1, 2] == c) ||
            (board[2, 0] == c && board[2, 1] == c && board[2, 2] == c) ||

            (board[0, 0] == c && board[1, 0] == c && board[2, 0] == c) ||
            (board[0, 1] == c && board[1, 1] == c && board[2, 1] == c) ||
            (board[0, 2] == c && board[1, 2] == c && board[2, 2] == c) ||

            (board[0, 0] == c && board[1, 1] == c && board[2, 2] == c) ||
            (board[0, 2] == c && board[1, 1] == c && board[2, 0] == c);

        public bool CheckDraw() =>
            board[0, 0] != ' ' && board[0, 1] != ' ' && board[0, 2] != ' ' &&
            board[1, 0] != ' ' && board[1, 1] != ' ' && board[1, 2] != ' ' &&
            board[2, 0] != ' ' && board[2, 1] != ' ' && board[2, 2] != ' ';

        public void Render()
        {
            Console.WriteLine(@"
  Tic Tac Toe
  
  ╔═══╦═══╦═══╗
  ║ {0} ║ {1} ║ {2} ║
  ╠═══╬═══╬═══╣
  ║ {3} ║ {4} ║ {5} ║
  ╠═══╬═══╬═══╣
  ║ {6} ║ {7} ║ {8} ║
  ╚═══╩═══╩═══╝
  ", board[0, 0], board[0, 1], board[0, 2], board[1, 0], board[1, 1], board[1, 2], board[2, 0], board[2, 1], board[2, 2]);
        }

        public void Reset()
        {
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }
    }
}