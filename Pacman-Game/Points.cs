using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class Point : IBoardObject
    {
        public char character { get; private set; } = '.';
        public bool canMove { get; } = true;
        public int points { get; init; } = 5;
        public ConsoleColor color { get; private set; } = ConsoleColor.White;


        public void Interact(Pacman pacman, Game game)
        {
            pacman.AddPoints(points);
            Console.WriteLine("Point consumed");
        }
    }


}
