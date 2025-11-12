using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public class Hard : Level
    {  
        public Hard()
        {
            SetBoard();
            FillEmptyCells(Board);
            SetFacit();
        }
        protected override void SetBoard()
        {
            Board = new ICell[9, 9];
            Board[0, 0] = new SetCell(6);
            Board[0, 1] = new SetCell(7);
            Board[0, 4] = new SetCell(8);
            Board[0, 5] = new SetCell(1);
            Board[1, 1] = new SetCell(4);
            Board[1, 3] = new SetCell(7);
            Board[1, 4] = new SetCell(3);
            Board[1, 6] = new SetCell(8);
            Board[2, 1] = new SetCell(1);
            Board[2, 5] = new SetCell(6);
            Board[2, 6] = new SetCell(7);
            Board[2, 8] = new SetCell(3);
            Board[3, 0] = new SetCell(1);
            Board[3, 2] = new SetCell(6);
            Board[3, 3] = new SetCell(3);
            Board[3, 7] = new SetCell(7);
            Board[4, 0] = new SetCell(7);
            Board[4, 5] = new SetCell(8);
            Board[4, 6] = new SetCell(1);
            Board[4, 7] = new SetCell(3);
            Board[5, 1] = new SetCell(8);
            Board[5, 2] = new SetCell(2);
            Board[5, 3] = new SetCell(4);
            Board[5, 8] = new SetCell(9);
            Board[6, 0] = new SetCell(8);
            Board[6, 3] = new SetCell(1);
            Board[6, 8] = new SetCell(6);
            Board[7, 2] = new SetCell(1);
            Board[7, 3] = new SetCell(6);
            Board[7, 4] = new SetCell(7);
            Board[7, 7] = new SetCell(8);
            Board[8, 2] = new SetCell(9);
            Board[8, 3] = new SetCell(8);
            Board[8, 8] = new SetCell(7);
        }
      
        protected override void SetFacit()
        {
            Facit = new ICell[9, 9]
        {
        { new SetCell(6), new SetCell(7), new SetCell(3), new SetCell(2), new SetCell(8), new SetCell(1), new SetCell(5), new SetCell(9), new SetCell(4) },
        { new SetCell(2), new SetCell(4), new SetCell(5), new SetCell(7), new SetCell(3), new SetCell(9), new SetCell(8), new SetCell(6), new SetCell(1) },
        { new SetCell(9), new SetCell(1), new SetCell(8), new SetCell(5), new SetCell(4), new SetCell(6), new SetCell(7), new SetCell(2), new SetCell(3) },
        { new SetCell(1), new SetCell(9), new SetCell(6), new SetCell(3), new SetCell(5), new SetCell(2), new SetCell(4), new SetCell(7), new SetCell(8) },
        { new SetCell(7), new SetCell(5), new SetCell(4), new SetCell(9), new SetCell(6), new SetCell(8), new SetCell(1), new SetCell(3), new SetCell(2) },
        { new SetCell(3), new SetCell(8), new SetCell(2), new SetCell(4), new SetCell(1), new SetCell(7), new SetCell(6), new SetCell(5), new SetCell(9) },
        { new SetCell(8), new SetCell(3), new SetCell(7), new SetCell(1), new SetCell(9), new SetCell(5), new SetCell(2), new SetCell(4), new SetCell(6) },
        { new SetCell(4), new SetCell(2), new SetCell(1), new SetCell(6), new SetCell(7), new SetCell(3), new SetCell(9), new SetCell(8), new SetCell(5) },
        { new SetCell(5), new SetCell(6), new SetCell(9), new SetCell(8), new SetCell(2), new SetCell(4), new SetCell(3), new SetCell(1), new SetCell(7) }
        };

        }
    }
}
