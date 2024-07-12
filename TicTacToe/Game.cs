namespace TicTacToe
{
    class Game
    {
        private Board board;
        private Player player;
        private AI ai;
        private bool playerTurn;
        private bool exit;
        private int playerScore;
        private int aiScore;

        public Game()
        {
            board = new Board();
            player = new Player(board);
            ai = new AI(board);
            playerTurn = true;
            exit = false;
            playerScore = 0;
            aiScore = 0;
        }

        public void Start()
        {
            while (!exit)
            {
                PlayGame();
                Console.WriteLine($"\nScores: Player: {playerScore} - AI: {aiScore}");
                Console.WriteLine("Press any key to play again, or Esc to exit.");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
                else
                {
                    board.Reset();
                    playerTurn = true;
                    exit = false;
                }
            }
        }

        public void PlayGame()
        {
            while(!exit)
            {
                if (playerTurn)
                {
                    if (!board.CheckDraw())
                    {
                        player.MakeMove();
                        //player.AIMakeMove();
                        if (board.CheckVictory('X'))
                        {
                            EndGame(@" █     █░    ██▓    ███▄    █ 
▓█░ █ ░█░   ▓██▒    ██ ▀█   █ 
▒█░ █ ░█    ▒██▒   ▓██  ▀█ ██▒
░█░ █ ░█    ░██░   ▓██▒  ▐▌██▒
░░██▒██▓    ░██░   ▒██░   ▓██░
░ ▓░▒ ▒     ░▓     ░ ▒░   ▒ ▒ 
  ▒ ░ ░      ▒ ░   ░ ░░   ░ ▒░
  ░   ░      ▒ ░      ░   ░ ░ 
    ░        ░              ░ ");
                            playerScore++;
                            break;
                        }
                    }
                    else
                    {
                        EndGame(@"▄▄▄█████▓    ██▓   ▓█████ 
▓  ██▒ ▓▒   ▓██▒   ▓█   ▀ 
▒ ▓██░ ▒░   ▒██▒   ▒███   
░ ▓██▓ ░    ░██░   ▒▓█  ▄ 
  ▒██▒ ░    ░██░   ░▒████▒
  ▒ ░░      ░▓     ░░ ▒░ ░
    ░        ▒ ░    ░ ░  ░
  ░          ▒ ░      ░   
             ░        ░  ░");
                        break;
                    }
                }
                else
                {
                    if (!board.CheckDraw())
                    {
                        //ai.MakeMinimaxMove(); // DarkSoulsMode
                        ai.MakeMove();
                        if (board.CheckVictory('O'))
                        {
                            EndGame(@"       ▄▄▄          ██▓       
      ▒████▄       ▓██▒       
      ▒██  ▀█▄     ▒██▒       
      ░██▄▄▄▄██    ░██░       
       ▓█   ▓██▒   ░██░       
       ▒▒   ▓▒█░   ░▓         
        ▒   ▒▒ ░    ▒ ░       
        ░   ▒       ▒ ░       
            ░  ░    ░         
                              
 █     █░    ██▓    ███▄    █ 
▓█░ █ ░█░   ▓██▒    ██ ▀█   █ 
▒█░ █ ░█    ▒██▒   ▓██  ▀█ ██▒
░█░ █ ░█    ░██░   ▓██▒  ▐▌██▒
░░██▒██▓    ░██░   ▒██░   ▓██░
░ ▓░▒ ▒     ░▓     ░ ▒░   ▒ ▒ 
  ▒ ░ ░      ▒ ░   ░ ░░   ░ ▒░
  ░   ░      ▒ ░      ░   ░ ░ 
    ░        ░              ░ ");
                            aiScore++;
                            break;
                        }
                    }
                    else
                    {
                        EndGame(@"▄▄▄█████▓    ██▓   ▓█████ 
▓  ██▒ ▓▒   ▓██▒   ▓█   ▀ 
▒ ▓██░ ▒░   ▒██▒   ▒███   
░ ▓██▓ ░    ░██░   ▒▓█  ▄ 
  ▒██▒ ░    ░██░   ░▒████▒
  ▒ ░░      ░▓     ░░ ▒░ ░
    ░        ▒ ░    ░ ░  ░
  ░          ▒ ░      ░   
             ░        ░  ░");
                        break;
                    }
                }
                playerTurn = !playerTurn;
            }
        }

        private void EndGame(string gameOverDrawMessage)
        {
            Console.Clear();
            board.Render();
            Console.WriteLine();
            Console.WriteLine(gameOverDrawMessage);
            Console.ReadKey(true);
        }
    }
}