using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public interface IAutoMove
    {
        (int row, int col) NextMove(Game game, Ghost ghost);
    }
}
