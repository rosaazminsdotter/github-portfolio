using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public class Easy : Level
    {
        // 1: Arv av klasser
        // 2: Vi har använt oss av arv av Level-klassen för att skapa olika svårighetsgrader. I detta fall Easy
        // 3: Vi har gjort de för att vi ville återanvända och specialisera funktionalitet från den grundläggande klassen Level till specifika nivåer.

        public Easy()
        {
            SetBoard();
            FillEmptyCells(Board);
            SetFacit();
        }

        protected override void SetBoard()
        {
            Board = new ICell[9, 9];
            Board[0, 1] = new SetCell(8);
            Board[0, 2] = new SetCell(4);
            Board[0, 3] = new SetCell(6);
            Board[0, 5] = new SetCell(5);
            Board[0, 7] = new SetCell(2);
            Board[0, 8] = new SetCell(9);
            Board[1, 0] = new SetCell(9);
            Board[1, 2] = new SetCell(5);
            Board[1, 5] = new SetCell(4);
            Board[1, 6] = new SetCell(3);
            Board[1, 8] = new SetCell(6);
            Board[2, 0] = new SetCell(2);
            Board[2, 1] = new SetCell(6);
            Board[2, 3] = new SetCell(9);
            Board[2, 4] = new SetCell(3);
            Board[2, 6] = new SetCell(8);
            Board[2, 7] = new SetCell(4);
            Board[3, 2] = new SetCell(6);
            Board[3, 4] = new SetCell(2);
            Board[3, 7] = new SetCell(8);
            Board[4, 0] = new SetCell(8);
            Board[4, 1] = new SetCell(9);
            Board[4, 3] = new SetCell(4);
            Board[4, 4] = new SetCell(6);
            Board[4, 5] = new SetCell(1);
            Board[4, 7] = new SetCell(3);
            Board[4, 8] = new SetCell(7);
            Board[5, 1] = new SetCell(4);
            Board[5, 2] = new SetCell(3);
            Board[5, 3] = new SetCell(8);
            Board[5, 4] = new SetCell(5);
            Board[5, 7] = new SetCell(6);
            Board[6, 0] = new SetCell(5);
            Board[6, 1] = new SetCell(1);
            Board[6, 2] = new SetCell(8);
            Board[6, 3] = new SetCell(3);
            Board[6, 5] = new SetCell(6);
            Board[6, 7] = new SetCell(7);
            Board[7, 0] = new SetCell(6);
            Board[7, 1] = new SetCell(3);
            Board[7, 2] = new SetCell(7);
            Board[7, 4] = new SetCell(4);
            Board[7, 5] = new SetCell(2);
            Board[7, 7] = new SetCell(9);
            Board[7, 8] = new SetCell(8);
            Board[8, 0] = new SetCell(4);
            Board[8, 2] = new SetCell(9);
            Board[8, 4] = new SetCell(7);
            Board[8, 7] = new SetCell(5);
        }

       

        protected override void SetFacit()
        {
            Facit = new ICell[9, 9]
            {
            { new SetCell(3), new SetCell(8), new SetCell(4), new SetCell(6), new SetCell(1), new SetCell(5), new SetCell(7), new SetCell(2), new SetCell(9) },
            { new SetCell(9), new SetCell(7), new SetCell(5), new SetCell(2), new SetCell(8), new SetCell(4), new SetCell(3), new SetCell(1), new SetCell(6) },
            { new SetCell(2), new SetCell(6), new SetCell(1), new SetCell(9), new SetCell(3), new SetCell(7), new SetCell(8), new SetCell(4), new SetCell(5) },
            { new SetCell(1), new SetCell(5), new SetCell(6), new SetCell(7), new SetCell(2), new SetCell(3), new SetCell(9), new SetCell(8), new SetCell(4) },
            { new SetCell(8), new SetCell(9), new SetCell(2), new SetCell(4), new SetCell(6), new SetCell(1), new SetCell(5), new SetCell(3), new SetCell(7) },
            { new SetCell(7), new SetCell(4), new SetCell(3), new SetCell(8), new SetCell(5), new SetCell(9), new SetCell(2), new SetCell(6), new SetCell(1) },
            { new SetCell(5), new SetCell(1), new SetCell(8), new SetCell(3), new SetCell(9), new SetCell(6), new SetCell(4), new SetCell(7), new SetCell(2) },
            { new SetCell(6), new SetCell(3), new SetCell(7), new SetCell(5), new SetCell(4), new SetCell(2), new SetCell(1), new SetCell(9), new SetCell(8) },
            { new SetCell(4), new SetCell(2), new SetCell(9), new SetCell(1), new SetCell(7), new SetCell(8), new SetCell(6), new SetCell(5), new SetCell(3) }
            };
           
        }

    }

}
