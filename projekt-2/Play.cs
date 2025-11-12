using System;

namespace Soduko_Grupp47
{
    public class Play
    {
        Board playBoard;
        // 1: Objektkomposition
        // 2: Klassen ‘Play’ har ett fält av klassen ‘Board’
        // 3: Play has-a board. Vi spelar med ett bräde
        Player player;

        int helpCount = 0;
        // 1: Inkapsling
        // 2: privat int värde, Sparar antalet gånger man har kallat på Help(int row, int col) metoden. Använder även den för att beräkna CalculatedScore!
        // 3: Den används enbart i denna klass, den ska alltså inte gå att manipulera utanför! Därför valde vi att ha den som private
        int wrong = 0;
        
        int totalScore = 100;
       
        /*public int CalculatedScore
        {
            get => totalScore - 10 * helpCount - 20 * wrong;         
        }*/
        // 1: Computed properties
        // 2: Vi ville göra sudokut mer "spännande" så vi skapade detta poäng system! Poäng subtraheras under spelets omgång varje gång man skriver help, eller varje gång man fått fel efter man skrivit done 
        // 3: Vi ville ha en simpel matematisk metod som använde sig av helpCount, wrong och totalScore. Så att spelaren ska kunna se hur många poäng de har kvar samt att spelet slutar när poängen nått 0 eller minus.

        public Play()
        {
            Welcomme();
            bool input = false;

            Console.WriteLine("Choose difficulty level:\n\nEasy - Medium - Hard");


            while (!input)
            {
                string a = Console.ReadLine();

                if (a == "easy" || a == "Easy" || a == "EASY" || a == null)
                {
                    playBoard = new Board(new Easy());
                    input = true;
                   
                }
                else if (a == "medium" || a == "Medium" || a == "MEDIUM")
                {
                    playBoard = new Board(new Medium());
                    input = true;
        
                }
                else if (a == "hard" || a == "Hard" || a == "HARD")
                {
                    playBoard = new Board(new Hard());
                    input = true;
                }
                else
                {
                    Console.WriteLine("Wrong input, try again");
                }
            }
        }

        public void MakeMove()
        {
            Console.Clear();
            bool stop = false;
            Console.WriteLine($"Total Score = {CalculatedScore} / {totalScore}");
            playBoard.DisplayBoard();


            while (!stop)
            {
                if (CalculatedScore <= 0)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have reached 0 points. YOU LOSE!!!");
                    break;
                }
                else
                {
                    Console.WriteLine($"{player.Name} which cell and number would you like to fill in?");
                    string a = Console.ReadLine();
                    if (a == "stop")
                    {
                        Console.WriteLine("The game is over! Quitter :<");
                        break;
                    }
                    else if (a == "done")
                    {
                        if (playBoard.CheckAnswer())
                        {
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Congratulation {player.Name}! You have won the game! Your score is {CalculatedScore} out of a {totalScore}");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("The answer is incorrect. The game will continue");
                            wrong++;
                            playBoard.ClearBoard();
                            Console.WriteLine($"Total Score = {CalculatedScore} / {totalScore}");
                            playBoard.DisplayBoard();
                        }
                    }
                    else if (a == "help")
                    {
                        bool x = false;
                        Console.WriteLine("Which cell do you need help with? Write row and column");

                        while (!x)
                        {
                            string help = Console.ReadLine();
                            string[] helpCell = help.Split(' ');
                            if (helpCell.Length != 2)
                            {
                                Console.WriteLine("Submit 2 numbers, space between each number");
                            }
                            else
                            {
                                if (int.TryParse(helpCell[0], out int row) && int.TryParse(helpCell[1], out int col))
                                {
                                    if(row < 1 || row > 9 && col < 1 || col > 9)
                                    {
                                        Console.WriteLine("Please write a number between 1 and 9");
                                    }
                                    else
                                    {
                                        Help(row - 1, col - 1);
                                        playBoard.ClearBoard();
                                        Console.WriteLine($"Total Score = {CalculatedScore} / {totalScore}");
                                        playBoard.DisplayBoard();
                                        break;
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Submit numbers only.");
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] input = a.Split(' ');
                        if (input.Length != 3)
                        {
                            Console.WriteLine("Submit 3 numbers, space between each number");
                        }
                        else
                        {
                            if (int.TryParse(input[0], out int row) & int.TryParse(input[1], out int col) & int.TryParse(input[2], out int value))
                            {

                                if(row < 1 || row > 9 || col < 1 || col > 9)
                                {
                                    Console.WriteLine("index out of bound");
                                }
                                else
                                {
                                    // 1: Subtypspolymorfism
                                    // 2: En ICell kan antingen vara SetCell eller ChangableCell. Deras metoder 'ChangeCell' beteer sig olika. I SetCells kommer ChangeCell metoden att ge ett felmeddelande medans den i ChangableCell kommer att ändra cellens värde, alternativt ge ett felmeddelande om inputen inte är mellan 1-9. 
                                    // 3: Celler beter sig olika beroende på vilken typ av cell det är. 
                                    ICell cell = playBoard.level.Board[row - 1, col - 1];
                                    cell.ChangeCell(value);
                                    playBoard.ClearBoard();
                                    Console.WriteLine($"Total Score = {CalculatedScore} / {totalScore}");
                                    playBoard.DisplayBoard();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Submit numbers only.");
                            }
                        }
                    }                    
                }                 
            }
        }
        public void Welcomme()
        {
            Console.WriteLine("Welcomme to the sudoku game! \nThe rules for sudoku are simple.\n\nA 9×9 square must be filled in with numbers from 1-9 with no repeated numbers in each line, horizontally or vertically." +
                "\nTo challenge you more, there are 3×3 squares marked out in the grid, and each of these squares can't have any repeat numbers either.\n");
            Console.WriteLine("To fill a cell in the sudoku, simply type the the row number followed by a space, then the column number that is also followed by a space and lastly the number you want to put in\n");
            Console.WriteLine("To submit your final board simply write done. If you wish to quit the game you may write stop\n");
            Console.WriteLine("If you write done witout being correct you will lose 20 points. So use this carefully!\n");
            Console.WriteLine("If you wish to recive help you can write help, remember that you will use up 10 points for this!\n");
            Console.WriteLine("You will start with a 100 points, if you lose them all you will LOSE the game. Good luck!!!\n");
            Console.WriteLine("Please state your name or press enter if you wish to be anonymus");
            string name = Console.ReadLine();

            if(name == "")
            {
                 player = new Player();
            }
            else
            {
                 player = new Player(name);
            }
            Console.Clear();
        }
        public void Help(int row, int col)
        {
            Console.Clear();
            playBoard.DisplayBoard();
            Console.WriteLine($"In row {row + 1} and col {col + 1} the answer should be {playBoard.level.Facit[row, col].value}");
            helpCount++;          
        }
    }
}
