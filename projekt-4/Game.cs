using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class Game
    {
        public Board board { get; private set; }
        ICharacter[] characters = new ICharacter[5];
        int switchCounter = 0;   
        public Pacman pacman { get; private set; }
        Ghost ghost1;
        Ghost ghost2;
        Ghost ghost3;
        Ghost ghost4;
        public IFruitFactory factory { get; private set; }
        public bool IsGameOver => pacman.lives < 0;


        Mode currentMode = Mode.NotScared;


        public Game(Board board)
        {
            this.board = board;
            
        }


        public void CreateChars()
        {
            pacman = new Pacman(board);
            ghost1 = new Ghost(new GoalOrientedMove(), (1, 1), board.grid);
            ghost2 = new Ghost(new GoalOrientedMove(), (1, 31), board.grid);
            ghost3 = new Ghost(new GoalOrientedMove(), (21, 1), board.grid);
            ghost4 = new Ghost(new GoalOrientedMove(), (21, 31), board.grid);


            characters[0] = pacman;
            characters[1] = ghost1;
            characters[2] = ghost2;
            characters[3] = ghost3;
            characters[4] = ghost4;
        }


        public void Switch(Mode newMode)
        {
            if (newMode is Mode.Scared)
            {
                if (currentMode is Mode.Scared)
                {
                    switchCounter = 0;
                }
                else
                {
                    currentMode = Mode.Scared;
                    for (int i = 0; i < characters.Length; i++)
                    {
                        characters[i].Switch(currentMode);
                    }
                }
            }
            else
            {
                currentMode = Mode.NotScared;
                for (int i = 0; i < characters.Length; i++)
                {
                    characters[i].Switch(currentMode);
                }
            }
        }


        void Collision(ICollision char1, ICollision char2)
        {
            char1.CollideWith(char2);
            char2.CollideWith(char1);
        }


        void CheckCollision()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                ICharacter character = characters[i];


                for (int j = 0; j < characters.Length; j++)
                {
                    ICharacter other = characters[j];
                    if (character.currentPosition == other.currentPosition && character != other)
                    {
                        Collision(character.collider, other.collider);
                    }
                }
            }
        }


        //Använder IEnumerable
        void Print()
        {
            
            Console.SetCursorPosition(0, 1); 
            IEnumerator<((int, int), IBoardObject)> enumerator = board.GetEnumerator();


            int lastRow = -1;


            while (enumerator.MoveNext())
            {
                var (coords, obj) = enumerator.Current;
                var (row, col) = coords;


                if (row != lastRow)
                {
                    if (lastRow != -1)
                        Console.WriteLine();
                    lastRow = row;
                }


                bool isCharacterHere = false;
                foreach (var c in characters)
                {
                    if (c.currentPosition.row == row && c.currentPosition.col == col)
                    {
                        Console.ForegroundColor = c.color;
                        Console.Write(c.appearence);
                        Console.ResetColor();
                        isCharacterHere = true;
                        break;
                    }
                }


                if (!isCharacterHere)
                {
                    Console.ForegroundColor = obj.color;
                    Console.Write(obj.character);
                    Console.ResetColor();
                }

            }


            Console.WriteLine();


           
        }


        void MoveCharacters()
        {
            foreach (var c in characters)
            {
                (int row, int col) moveDir = c.NextMove(this);
                (int row, int col) newPos = (c.currentPosition.row + moveDir.row,
                                             c.currentPosition.col + moveDir.col);

                IBoardObject obj = board.grid[newPos.row][newPos.col];
                if (obj.canMove)
                    c.UpdatePosition(newPos, this, obj);
            }
           
        }

        private void CheckBoardClear()
        {
            bool hasPointsLeft = false;

            foreach (var row in board.grid)
            {
                foreach (var obj in row)
                {
                    if (obj is Point || obj is PowerPill)
                    {
                        hasPointsLeft = true;
                        break;
                    }
                }
                if (hasPointsLeft) break;
            }

            if (!hasPointsLeft)
            {
                ResetBoard();
            }
        }

        private void ResetBoard()
        {
    
            PowerPillsFactory powerFactory = new PowerPillsFactory(board);
            powerFactory.Update(this); 
            pacman.ResetPosition(pacman.startPosition);
            ((Ghost)characters[1]).ResetPosition(ghost1.startPosition);
            ((Ghost)characters[2]).ResetPosition(ghost2.startPosition);
            ((Ghost)characters[3]).ResetPosition(ghost3.startPosition);
            ((Ghost)characters[4]).ResetPosition(ghost4.startPosition);
        }


        void ManageMode()
        {
            if (currentMode == Mode.Scared)
            {
                switchCounter++;


                if (switchCounter >= 30)
                {
                    Switch(Mode.NotScared);
                    switchCounter = 0;
                }
            }
        }
        public void RemoveBoardObject((int row, int col) coordinates)
        {
            board.grid[coordinates.row][coordinates.col] = new Empty();
        }

        // KRAV #1:
        // 1: Generics
        // 2: Metoden tar in oliika egenskaper hos pacman som har olika datatyper och presenterar dem över brädet när man kör.
        // 3: Vi ville ha en enda metod som presenterar informationen.
        public void PrintStatus<T>
            (T entity, Func<T, int> getLives, Func<T, int> getScore, Func<T, List<string>> getFruits)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 0);

            Console.Write($"Lives: {getLives(entity)}  Score: {getScore(entity)}  Fruits: ");

            var fruits = getFruits(entity);
            if (pacman.eatenFruits.Count > 0)
                Console.Write(string.Join(", ", pacman.eatenFruits));
            else
                Console.Write("None");

            Console.WriteLine();
        }

        public void Play(IFruitFactory factory)
        {
 
            MoveCharacters();
            ManageMode();
            factory.Update(this); 
            PrintStatus(pacman, p => p.lives, p => p.score, p => p.eatenFruits);
            Print();
            CheckCollision();
            CheckBoardClear();
        }
    }

}
