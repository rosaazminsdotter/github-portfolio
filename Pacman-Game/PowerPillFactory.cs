using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
     public class PowerPillsFactory 
    {
        Board board;
        Random random = new Random();

        private const double PowerPillChance = 0.020;

        public PowerPillsFactory(Board board)
        {
            this.board = board;
        }

        // KRAV #5:
        // 1: Enumerable & Enumerator
        // 2: Brädet är en IEnumerable som skapar en Enumerable som låter oss iterera över brädet
        // 3: För att olika klasser ska kunna iterera över brädet separat och för att undvika duplicerad kod
        public void Update(Game game)
        {
            IEnumerator<((int row, int col), IBoardObject)> enumerator = board.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var (coords, obj) = enumerator.Current;

                if (obj is Empty)
                {
                    if (random.NextDouble() < PowerPillChance)
                    {
                        board.grid[coords.row][coords.col] = new PowerPill();
                    }
                    else
                    {
                        board.grid[coords.row][coords.col] = new Point();
                    }
                }
            }
        }
    }

}
