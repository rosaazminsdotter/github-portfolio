using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public interface ICell
    {
        // 1: Interface
        // 2: Vi använder ICell-gränssnittet för att definiera en gemensam struktur för celler (i sudoku brädet) och deras egenskaper (deras värde och om de värdet kan ändras)
        // 3: Vi har använt det för att tillåta olika typer av celler att implementera samma gränssnitt och därigenom möjliggöra enhetlig behandling (i vårt fall ChangableCell och SetCell)
        int value { get; }
        
        void ChangeCell(int x);
    }
}
