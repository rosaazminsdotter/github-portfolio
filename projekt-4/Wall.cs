using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{

    public class Wall : IBoardObject
    {
        public char character { get; private set; } = '■';
        public bool canMove { get; } = false;
        public int points { get; } = 0;
        public ConsoleColor color { get; private set; } = ConsoleColor.White;
        public void Interact(Pacman pacman, Game game)
        {
            Console.WriteLine("Hit a wall");
        }
    }

}
