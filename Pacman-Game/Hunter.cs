using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class Hunter : ICollision
    {
        public void CollideWith(ICollision other)
        {
            other.CollisionEffect(this);
        }
        public void CollisionEffect(Hunter other)
        {
            //nothing
        }
        public void CollisionEffect(Hunted other)
        {
            //nothings
        }
    }

}
