namespace TicTacToe
{
    class Player
    {
        private Board board;

        public Player(Board board)
        {
            this.board = board;
        }

        public void MakeMove()
        {
            int row = 1;
            int column = 1;
            int initialMargin = 4;
            int rowSpaces = 2;
            int columnSpaces = 4;

            bool moved = false;
            while (!moved)
            {
                Console.Clear();
                board.Render();
                Console.SetCursorPosition((column * columnSpaces + initialMargin), (row * rowSpaces + initialMargin));

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        row = row <= 0 ? 2 : row - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        row = row >= 2 ? 0 : row + 1;
                        break;

                    case ConsoleKey.LeftArrow:
                        column = column <= 0 ? 2 : column - 1;
                        break;

                    case ConsoleKey.RightArrow:
                        column = column >= 2 ? 0 : column + 1;
                        break;

                    case ConsoleKey.Enter:
                        if (board.GetCell(row, column) == ' ')
                        {
                            board.SetCell(row, column, 'X');
                            moved = true;
                        }
                        break;

                    case ConsoleKey.Escape:
                        Console.Clear();
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }

        public void AIMakeMove()
        {
            int bestScore = int.MinValue;
            (int, int) bestMove = (-1, -1);
            List<(int, int)> openCells = board.GetOpenCells();

            foreach (var (row, column) in openCells)
            {
                board.SetCell(row, column, 'X');
                int score = Minimax(board, 0, false, int.MinValue, int.MaxValue);
                board.SetCell(row, column, ' ');
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = (row, column);
                }
            }

            board.SetCell(bestMove.Item1, bestMove.Item2, 'X');
        }

        private int Minimax(Board board, int depth, bool isMaximizing, int alpha, int beta)
        {
            if (board.CheckVictory('X'))
                return 10 - depth;
            if (board.CheckVictory('O'))
                return depth - 10;
            if (board.GetOpenCells().Count == 0)
                return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                foreach (var (row, column) in board.GetOpenCells())
                {
                    board.SetCell(row, column, 'X');
                    int score = Minimax(board, depth + 1, false, alpha, beta);
                    board.SetCell(row, column, ' ');
                    bestScore = Math.Max(score, bestScore);
                    alpha = Math.Max(alpha, score);
                    if (beta <= alpha)
                        break;
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                foreach (var (row, column) in board.GetOpenCells())
                {
                    board.SetCell(row, column, 'O');
                    int score = Minimax(board, depth + 1, true, alpha, beta);
                    board.SetCell(row, column, ' ');
                    bestScore = Math.Min(score, bestScore);
                    beta = Math.Min(beta, score);
                    if (beta <= alpha)
                        break;
                }
                return bestScore;
            }
        }
    }
}