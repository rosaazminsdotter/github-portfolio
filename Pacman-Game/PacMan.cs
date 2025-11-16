using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class Pacman : ICharacter
    {
        private (int row, int col) currentDirection = (0, 0);
        public ICollision collider { get; private set; }
        public char appearence { get; private set; } = 'O';

        public ConsoleColor color { get; private set; } = ConsoleColor.Yellow;
        public int score { get; private set; }
        public (int row, int col) startPosition { get; private set; } = (11, 1);
        public (int row, int col) currentPosition { get; private set; } = (11, 1);
        public int lives { get; private set; } = 3;
        public List<string> eatenFruits { get; private set; } = new List<string>();

        public Board board;
        PacmanColliderFactory factory = new PacmanColliderFactory();


        public Pacman(Board board)
        {
           
            this.board = board;
            ICollision collision = new Hunted(this);
            collider = collision;
        }
        public (int, int) NextMove(Game game)
        {

            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        currentDirection = (-1, 0);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        currentDirection = (1, 0);
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        currentDirection = (0, -1);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        currentDirection = (0, 1);
                        break;
                }
            }

            return currentDirection;
        }


        public void UpdatePosition((int row, int col) newPosition, Game game, IBoardObject boardObject)
        {
            currentPosition = newPosition;
            game.RemoveBoardObject(newPosition);
            boardObject.Interact(this, game);
        }

        public void Switch(Mode mode)
        {
            collider = factory.Create(mode, this);
        }
        public void Die()
        {
            lives--;
            if (lives >= 0)
            {
                currentPosition = startPosition;
            }
        }
        public void AddPoints(int points)
        {
            score += points;
        }
        public void AddFruit(string fruitName)
        {
            eatenFruits.Add(fruitName);
        }
        public void ResetPosition((int row, int col) pos)
        {
            currentPosition = pos;
        }
        public void AddLives()
        {
            if(lives <= 2)
            {
                lives++;
            }
            
        }
    }

}

