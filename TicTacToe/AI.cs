namespace TicTacToe
{
    class AI
    {
        private Board board;
        private Random rng;

        public AI(Board board)
        {
            this.board = board;
            rng = new Random();
        }

        public void MakeMove()
        {
            List<(int, int)> openCells = board.GetOpenCells();

            // Check if AI can win in the next move
            foreach (var (row, column) in openCells)
            {
                board.SetCell(row, column, 'O');
                if (board.CheckVictory('O'))
                {
                    // If it does, keep the move and return
                    return;
                }
                board.SetCell(row, column, ' ');
            }

            // Check if player can win in the next move and block them
            foreach (var (row, column) in openCells)
            {
                board.SetCell(row, column, 'X');
                if (board.CheckVictory('X'))
                {
                    // If it does, place 'O' in this cell to block 'X' and return
                    board.SetCell(row, column, 'O');
                    return;
                }
                board.SetCell(row, column, ' ');
            }

            // If no immediate win or block, make a random move
            if (openCells.Count > 0)
            {
                var (selectedRow, selectedColumn) = openCells[rng.Next(openCells.Count)];
                board.SetCell(selectedRow, selectedColumn, 'O');
            }
        }

        public void MakeMinimaxMove()
        {
            int bestScore = int.MinValue; // Initialize the best score to the minimum possible value.
            (int, int) bestMove = (-1, -1); // Initialize bestMove to (-1, -1) indicating no valid move has been selected yet.
            List<(int, int)> openCells = board.GetOpenCells();

            foreach (var (row, column) in openCells)
            {
                board.SetCell(row, column, 'O'); // Make a temporary move for 'O' (AI).
                int score = Minimax(board, 0, false, int.MinValue, int.MaxValue); // Evaluate the move using Minimax.
                board.SetCell(row, column, ' '); // Undo the temporary move.
                if (score > bestScore) // If the evaluated score is better than the best score.
                {
                    bestScore = score; // Update the best score.
                    bestMove = (row, column); // Update the best move.
                }
            }

            board.SetCell(bestMove.Item1, bestMove.Item2, 'O'); // Make the best move for 'O'.
        }

        private int Minimax(Board board, int depth, bool isMaximizing, int alpha, int beta)
        {
            if (board.CheckVictory('O')) return 10 - depth; // If 'O' wins, return a positive score (favoring shallower victories).
            if (board.CheckVictory('X')) return depth - 10; // If 'X' wins, return a negative score (favoring deeper victories).
            if (board.GetOpenCells().Count == 0) return 0;  // If the board is full (tie), return 0.

            if (isMaximizing)
            {
                int bestScore = int.MinValue; // Initialize the best score to the minimum possible value.
                foreach (var (row, column) in board.GetOpenCells())
                {
                    board.SetCell(row, column, 'O'); // Make a temporary move for 'O'.
                    int score = Minimax(board, depth + 1, false, alpha, beta); // Evaluate the move using Minimax for the minimizing player.
                    board.SetCell(row, column, ' '); // Undo the temporary move.
                    bestScore = Math.Max(score, bestScore); // Update the best score.
                    alpha = Math.Max(alpha, score); // Update alpha with the best score.
                    if (beta <= alpha) // Alpha-Beta Pruning condition.
                        break; // Prune the search tree by breaking out of the loop.
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue; // Initialize the best score to the maximum possible value.
                foreach (var (row, column) in board.GetOpenCells())
                {
                    board.SetCell(row, column, 'X'); // Make a temporary move for 'X'.
                    int score = Minimax(board, depth + 1, true, alpha, beta); // Evaluate the move using Minimax for the maximizing player.
                    board.SetCell(row, column, ' '); // Undo the temporary move.
                    bestScore = Math.Min(score, bestScore); // Update the best score.
                    beta = Math.Min(beta, score); // Update beta with the best score.
                    if (beta <= alpha) // Alpha-Beta Pruning condition.
                        break; // Prune the search tree by breaking out of the loop.
                }
                return bestScore;
            }
            //Alpha and beta are two parameters used in the Minimax algorithm to implement alpha-beta pruning, which helps to reduce the number of nodes evaluated in the search tree.
        }
    }
}
