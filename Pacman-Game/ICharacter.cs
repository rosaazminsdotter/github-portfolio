using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    // KRAV #2 och 3:
    // 1: Strategy Pattern och Bridge Pattern.
    // 2: Subtyperna av ICharacter kan byta mellan ICollision-subtyperna
    // 3: Både pacman och ghost behövde båda kunna vara hunter och hunted.
    public interface ICharacter
    {
        ICollision collider { get; }
        char appearence { get; }
        (int row, int col) startPosition { get; }
        (int row, int col) currentPosition { get; }
        ConsoleColor color { get; }


        (int, int) NextMove(Game game);
        void UpdatePosition((int row, int col) newPosition, Game game, IBoardObject boardObject);
        void Switch(Mode mode);
        void Die();
        void ResetPosition((int row, int col) pos);
    }

}
