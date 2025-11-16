using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class GoalOrientedMove : IAutoMove
    {

        public (int row, int col) NextMove(Game game, Ghost ghost)
        {
            var board = game.board;
            var pacman = game.pacman;
            var currentPosition = ghost.currentPosition;
            var lastPosition = ghost.lastPosition;

            var target = pacman.currentPosition;

           
            var possibleSteps = new List<(int row, int col)>
            {
                (0, 1),   
                (1, 0),   
                (0, -1), 
                (-1, 0)  
            };

            var validSteps = possibleSteps

            // KRAV #6:
            // 1: LINQs metod-syntax
            // 2: Vi använder LINQ för att filtrera ut ogilitiga steg
            // 3: Vi ville simplifiera algoritmen för hur ghost rör sig frammåt på brädet
                .Where(step =>
                {
                    int newRow = currentPosition.row + step.row;
                    int newCol = currentPosition.col + step.col;

                    bool canMove = newRow >= 0 && newRow < board.Height &&
                                   newCol >= 0 && newCol < board.Width &&
                                   board.grid[newRow][newCol].canMove;

                    if (lastPosition.HasValue)
                    {
                        if (newRow == lastPosition.Value.row && newCol == lastPosition.Value.col)
                            canMove = false;
                    }

                    return canMove;
                }).ToList();

            if (validSteps.Count == 0)
                return (0, 0); 

            var bestStep = validSteps
                .OrderBy(step =>
                {
                    int newRow = currentPosition.row + step.row;
                    int newCol = currentPosition.col + step.col;
                    int dx = newCol - target.col;
                    int dy = newRow - target.row;
                    return dx * dx + dy * dy;
                })
                .First();

        
            ghost.lastPosition = currentPosition;

            return bestStep;
        }
    }

}
