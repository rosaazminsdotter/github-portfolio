using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public interface ICollision
    {
        void CollideWith(ICollision other);
        void CollisionEffect(Hunter other);
        void CollisionEffect(Hunted other);
    }

}
