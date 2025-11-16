using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class RandomMove : IAutoMove
    {

        private Random random = new Random();

        public (int row, int col) NextMove(Game game, Ghost ghost)
        {
            var board = game.board;
            var currentPosition = ghost.currentPosition;

            var possibleSteps = new List<(int row, int col)>
        {
            (0, 1),   
            (1, 0),   
            (0, -1),  
            (-1, 0)   
        };

            var validSteps = possibleSteps
                .Where(step =>
                {
                    int newRow = currentPosition.row + step.row;
                    int newCol = currentPosition.col + step.col;
                    return newRow >= 0 && newRow < board.Height &&
                           newCol >= 0 && newCol < board.Width &&
                           board.grid[newRow][newCol].canMove;
                }).ToList();

            if (validSteps.Count == 0)
                return (0, 0);

            return validSteps[random.Next(validSteps.Count)];
        }


    }
}
    


