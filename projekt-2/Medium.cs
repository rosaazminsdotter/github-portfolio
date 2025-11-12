using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public class Medium : Level
    {
       
        public Medium()
        {
            SetBoard();
            FillEmptyCells(Board);
            SetFacit();
        }
        protected override void SetBoard()
        {
            Board = new ICell[9, 9];
            Board[0, 0] = new SetCell(5);
            Board[0, 2] = new SetCell(3);
            Board[0, 4] = new SetCell(1);
            Board[0, 6] = new SetCell(6);
            Board[1, 1] = new SetCell(2);
            Board[1, 2] = new SetCell(9);
            Board[1, 5] = new SetCell(7);
            Board[1, 6] = new SetCell(3);
            Board[2, 1] = new SetCell(6);
            Board[2, 5] = new SetCell(3);
            Board[2, 7] = new SetCell(5);
            Board[2, 8] = new SetCell(9);
            Board[3, 4] = new SetCell(9);
            Board[3, 5] = new SetCell(6);
            Board[3, 6] = new SetCell(2);
            Board[3, 8] = new SetCell(4);
            Board[4, 2] = new SetCell(4);
            Board[4, 3] = new SetCell(3);
            Board[4, 6] = new SetCell(5);
            Board[4, 7] = new SetCell(6);
            Board[5, 0] = new SetCell(2);
            Board[5, 3] = new SetCell(5);
            Board[5, 7] = new SetCell(9);
            Board[5, 8] = new SetCell(3);
            Board[6, 0] = new SetCell(9);
            Board[6, 1] = new SetCell(4);
            Board[6, 5] = new SetCell(2);
            Board[7, 0] = new SetCell(3);
            Board[7, 4] = new SetCell(4);
            Board[7, 5] = new SetCell(5);
            Board[8, 1] = new SetCell(5);
            Board[8, 3] = new SetCell(9);
            Board[8, 6] = new SetCell(4);
        }
       

        protected override void SetFacit()
        {
            Facit = new ICell[9, 9]
            {
            { new SetCell(5), new SetCell(7), new SetCell(3), new SetCell(4), new SetCell(1), new SetCell(9), new SetCell(6), new SetCell(8), new SetCell(2) },
            { new SetCell(8), new SetCell(2), new SetCell(9), new SetCell(6), new SetCell(5), new SetCell(7), new SetCell(3), new SetCell(4), new SetCell(1) },
            { new SetCell(4), new SetCell(6), new SetCell(1), new SetCell(2), new SetCell(8), new SetCell(3), new SetCell(7), new SetCell(5), new SetCell(9) },
            { new SetCell(1), new SetCell(3), new SetCell(5), new SetCell(8), new SetCell(9), new SetCell(6), new SetCell(2), new SetCell(7), new SetCell(4) },
            { new SetCell(7), new SetCell(9), new SetCell(4), new SetCell(3), new SetCell(2), new SetCell(1), new SetCell(5), new SetCell(6), new SetCell(8) },
            { new SetCell(2), new SetCell(8), new SetCell(6), new SetCell(5), new SetCell(7), new SetCell(4), new SetCell(1), new SetCell(9), new SetCell(3) },
            { new SetCell(9), new SetCell(4), new SetCell(7), new SetCell(1), new SetCell(6), new SetCell(2), new SetCell(8), new SetCell(3), new SetCell(5) },
            { new SetCell(3), new SetCell(1), new SetCell(8), new SetCell(7), new SetCell(4), new SetCell(5), new SetCell(9), new SetCell(2), new SetCell(6) },
            { new SetCell(6), new SetCell(5), new SetCell(2), new SetCell(9), new SetCell(3), new SetCell(8), new SetCell(4), new SetCell(1), new SetCell(7) }
            };
        }
       
    }
}
