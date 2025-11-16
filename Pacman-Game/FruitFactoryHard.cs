using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class FruitFactoryHard : IFruitFactory
    {
        public Board board { get; private set; }
        public Random random { get; private set; } = new Random();


        private List<(string name, char symbol, int points, ConsoleColor color)> fruits = new()
        {
            ("cherry", 'o', 100, ConsoleColor.Magenta),
            ("strawberry", 'o', 300, ConsoleColor.DarkRed),
            ("orange", 'o', 500, ConsoleColor.Yellow),
            ("apple", 'o', 700, ConsoleColor.Cyan),
            ("melon", 'o', 1000, ConsoleColor.Green)
        };

        private int nextFruitIndex = 0;
        private int frameDelay = 0; 

        public FruitFactoryHard(Board board)
        {
            this.board = board;
        }

       
        public void Update(Game game)
        {
            if (IsFruitOnBoard())
                return;

            if (frameDelay > 0)
            {
                frameDelay--;
                return;
            }

        
            if (nextFruitIndex < fruits.Count)
            {
                var fruit = fruits[nextFruitIndex];
                var pos = GetRandomEmptySpot();
                if (pos.HasValue)
                {
                    board.grid[pos.Value.row][pos.Value.col] = new Fruit(
                        fruit.symbol, fruit.points, fruit.name, fruit.color, this, game
                    );
                }
            }
        }

  
        public void FruitEaten()
        {
            nextFruitIndex++;
            frameDelay = 20;
        }

   
        private bool IsFruitOnBoard()
        {
            IEnumerator<((int row, int col), IBoardObject)> enumerator = board.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var (_, obj) = enumerator.Current;
                if (obj is Fruit)
                    return true;
            }
            return false;
        }

  
        private (int row, int col)? GetRandomEmptySpot()
        {
            List<(int row, int col)> emptySpots = new();
            IEnumerator<((int row, int col), IBoardObject)> enumerator = board.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var (coords, obj) = enumerator.Current;
                if (obj is Empty)
                    emptySpots.Add(coords);
            }

            if (emptySpots.Count == 0)
                return null;

            return emptySpots[random.Next(emptySpots.Count)];
        }
    }

}
