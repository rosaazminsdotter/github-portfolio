using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public abstract class Level
    {
        // 1: Abstrakt klass
        // 2: Vi använder oss av den abstrakta klassen Level för att den har gemensamma egenskaper och metoder för alla nivåer (Easy, Medium, Hard).
        // 3: Vi har använt det för att undvika onödig kodupprepning och för att definiera gemensam funktionalitet på en högre nivå.

        public ICell[,] Board { get; protected set; }
       
        public ICell[,] Facit { get; protected set; }
       
        protected ICell[,] FillEmptyCells(ICell[,] board) //KODÅTERANVÄNDNING / KONKRET MEDLEM
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == null)
                    {
                        board[i, j] = new ChangableCell(0);
                    }
                }
            }
            return board;
        }

        // 1: Åtkomstmodifieraren 'protected' 
        // 2: protected: Här gömmer vi implementationens detaljer genom att använda skyddad (protected) metod för att fylla tomma celler. 
        // 3: protected: Det ger en tydlig gränssnittsnivå för hur nivåerna ska fylla tomma celler. Vi ville att denna metod enbart går att använda av subtyperna.
        
        public int CalculatedScore(); // ABSTRAKT MEDLEM
        
        protected abstract void SetBoard(); // ABSTRAKT MEDLEM
        protected abstract void SetFacit(); 
    }
}
