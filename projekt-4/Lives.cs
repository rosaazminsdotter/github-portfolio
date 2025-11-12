using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    internal class Lives : IBoardObject
    {
        public char character { get; } = 'o';
        public int points { get; } = 0;
        public bool canMove { get; } = true;
        public ConsoleColor color { get; private set; } = ConsoleColor.Red;
        public void Interact(Pacman pacman, Game game)
        {
            pacman.AddLives();
        }
    }
}
