using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public abstract class Level
    {
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
        protected abstract void SetBoard(); 
        protected abstract void SetFacit(); 
    }
}
