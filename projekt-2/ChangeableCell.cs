using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public class ChangableCell : ICell
    {
        public int value { get; private set; }
        
        public ChangableCell(int value)
        {
            this.value = value;
        }

        public void ChangeCell(int value)
        {
            if(value <= 9 && value >= 1)
            {
                this.value = value;
            }
            else
            {
                Console.WriteLine("Choose a number between 1 and nine");
            }
        }
    }
}
