using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    class BoardIterator : IEnumerator<((int, int), IBoardObject)>
    {
        private Board board;
        private int currentRow = 0;
        private int currentCol = -1;


        public BoardIterator(Board board)
            => this.board = board;


        public ((int, int), IBoardObject) Current
        {
            get
            {
                if (currentRow < 0 || currentRow > board.Height || currentCol < 0 || currentCol > board.Width)
                    throw new InvalidOperationException();


                return ((currentRow, currentCol), board.grid[currentRow][currentCol]);
            }
        }
        object IEnumerator.Current => Current;


        public bool MoveNext()
        {
            currentCol++;
            
            if (currentCol >= board.Width)
            {
                currentCol = 0;
                currentRow++;
            }

            return currentRow < board.Height;
        }


        public void Reset()
        {
            currentRow = 0;
            currentCol = -1;
        }


        public void Dispose() { }
    }



}
