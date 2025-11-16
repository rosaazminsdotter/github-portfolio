using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{

    public class Ghost : ICharacter
    {
        public IAutoMove move { get; private set; }
        public IAutoMove goalOrientedMove { get; private set; }
        public IAutoMove randomMove { get; private set; }
        public ICollision collider { get; private set; }
        public char appearence { get; private set; } = '@';
        public ConsoleColor color { get; private set; } = ConsoleColor.Blue;
        public (int row, int col) startPosition { get; private set; }
        public (int row, int col) currentPosition { get; private set; }
        public List<List<IBoardObject>> board;
        GhostColliderFactory factory = new GhostColliderFactory();
        public (int row, int col)? lastPosition;


        public Ghost(IAutoMove move, (int, int) startPosition, List<List<IBoardObject>> board)
        {
            this.goalOrientedMove = move;
            this.move = move;
            this.randomMove = new RandomMove();
            this.startPosition = startPosition;
            this.board = board;
            currentPosition = startPosition;
            ICollision collision = new Hunter();
            collider = collision;
        }
        public (int, int) NextMove(Game game)
        {
            return move.NextMove(game, this);
        }
        public void UpdatePosition((int row, int col) newPosition, Game game, IBoardObject boardObject)
        {
            currentPosition = newPosition;
        }


        public void Switch(Mode mode)
        {
            collider = factory.Create(mode, this);
            if (mode == Mode.Scared)
                move = randomMove;
            else
                move = goalOrientedMove;
        }
        public void Die()
        {
            currentPosition = startPosition;
            lastPosition = null;
        }
        public void ResetPosition((int row, int col) pos)
        {
            currentPosition = pos;
        }
    }

}
