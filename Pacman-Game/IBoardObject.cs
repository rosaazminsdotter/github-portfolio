using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public interface IBoardObject
    {
        char character { get; } 
        int points { get; }
        bool canMove { get; }
        ConsoleColor color { get; }
        void Interact(Pacman pacman, Game game);

    }



}
