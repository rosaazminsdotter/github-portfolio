using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class GhostColliderFactory
    {
        public ICollision Create(Mode mode, ICharacter character)
        {
            return mode switch
            {
                Mode.NotScared => new Hunter(),
                Mode.Scared => new Hunted(character),
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
        }
    }

}
