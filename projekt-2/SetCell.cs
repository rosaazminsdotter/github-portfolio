using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public class SetCell : ICell
    {
        public int value { get; }
       
        public SetCell(int value)
        {
            this.value = value;
        }
        public void ChangeCell(int value)
        {
            Console.WriteLine("You can't change pre-set cell");
        }
    }


}
