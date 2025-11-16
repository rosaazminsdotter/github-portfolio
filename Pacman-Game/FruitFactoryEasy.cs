using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class FruitFactoryEasy : IFruitFactory
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
        private const double LivesChance = 0.20;

        public FruitFactoryEasy(Board board)
        {
            this.board = board;
        }

       
        public void Update(Game game)
        {
            if (IsFruitOnBoard() || IsLivesOnBoard())
                return;

            if (frameDelay > 0)
            {
                frameDelay--;
                return;
            }

            var pos = GetRandomEmptySpot();
            if (!pos.HasValue)
                return;

            if (random.NextDouble() < LivesChance)
            {
                board.grid[pos.Value.row][pos.Value.col] = new Lives(); 
            }
            else if (nextFruitIndex < fruits.Count)
            {
                var fruit = fruits[nextFruitIndex];
                board.grid[pos.Value.row][pos.Value.col] = new Fruit(
                    fruit.symbol, fruit.points, fruit.name, fruit.color, this, game
                );
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
        private bool IsLivesOnBoard()
        {
            IEnumerator<((int row, int col), IBoardObject)> enumerator = board.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var (_, obj) = enumerator.Current;
                if (obj is Lives)
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
