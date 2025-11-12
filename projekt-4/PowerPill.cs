using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class PowerPill : IBoardObject
    {
        public char character { get; private set; } = 'O';
        public bool canMove { get; } = true;
        public int points { get; } = 200;

        public ConsoleColor color { get; private set; } = ConsoleColor.White;

        public void Interact(Pacman pacman, Game game) 
        {
            pacman.AddPoints(points);
            game.Switch(Mode.Scared);
            Console.WriteLine("PowerPill consumed");
        }
    }

}
