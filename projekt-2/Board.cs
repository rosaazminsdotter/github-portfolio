using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public class Board
    {
        public Level level;

        // 1: BeroendeInjektion
        // 2: Vilken level som helst kan sättas in här. 
        // 3: En board kan ha olika levlar. Så beroende på vilken level de har kommer brädet att fungera olika.  
        public Board(Level level)
        {
            this.level = level;
        }

        public bool CheckAnswer()
        {
            bool correct = true;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (level.Board[i, j].value != level.Facit[i, j].value)
                    {
                        return false; 
                    }
                }
            }
            return correct;
        }

        public void Display(ICell x)
        {
            if(x.value==0)
            {               
               Console.Write("[ ]");
            }
            else
            {
                if(x is SetCell)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(x.value);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("]");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"[{x.value}]");
                }                  
            }
        }

        public void DisplayBoard()
        {         
            ICell x;
            for (int i = 0; i < 9; i++)
            {
                if (i > 0 && i % 3 == 0)
                {
                    Console.WriteLine(new string(' ', 12 * 2 + 2 * (12 / 3) - 1));
                }
                for (int j = 0; j < 9; j++)
                {
                    if (j > 0 && j % 3 == 0)
                    {
                        Console.Write("| ");
                        
                    }
                    x = level.Board[i, j];
                    Display(x);
                }
                Console.WriteLine();
            }
        }
       
        public void ClearBoard()
        {
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();           
        }
    }
}

