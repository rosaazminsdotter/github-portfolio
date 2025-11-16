
namespace PacManGame
{
    public class Program
    {
        static void Main(string[] args)
        {

            bool playAgain = true;
            IFruitFactory factory;

            while (playAgain)
            {
                Console.Clear();
                Console.CursorVisible = false;

                Board board = new Board();
                Game game = new Game(board);
                game.CreateChars();

                Console.WriteLine("Welcome! Choose Level: 1. Easy or 2. Hard");
                string level = Console.ReadLine();

                if (level == "1")
                {
                    factory = new FruitFactoryEasy(board);
                }
                else if (level == "2")
                {
                    factory = new FruitFactoryHard(board);
                }
                else
                {
                    Console.WriteLine("Invalid input, making it hard");
                    factory = new FruitFactoryHard(board);

                }


                while (!game.IsGameOver)
                {
                    game.Play(factory);
                    Thread.Sleep(50);
                }

                Console.Clear();
                Console.WriteLine("GAME OVER!");
                Console.WriteLine($"Your final score: {game.pacman.score}");
                Console.WriteLine("Want to play again?");
                Console.WriteLine("1 = Yes");
                Console.WriteLine("2 = No");
  
                string input = Console.ReadLine();  

                if (input == "1")
                {
                    playAgain = true;  
                }
                else if (input == "2")
                {
                    playAgain = false; 
                }
                else
                {
                    Console.WriteLine("Invalid input, exiting...");
                    playAgain = false;
                }
            }

            Console.WriteLine("Thank you for playing! Bye!");
            Thread.Sleep(1000);
        }

    }
}
