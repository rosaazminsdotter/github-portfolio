using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class Empty : IBoardObject
    {
        public char character { get; } = ' ';
        public int points { get; } = 0;
        public ConsoleColor color { get; private set; }

        public bool canMove { get; } = true;
        public void Interact(Pacman pacman, Game game)
        {
            Console.WriteLine("On empty");
        }
    }

}
