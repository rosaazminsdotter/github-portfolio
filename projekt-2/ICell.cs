using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public interface ICell
    {
        int value { get; }
        
        void ChangeCell(int x);
    }
}
