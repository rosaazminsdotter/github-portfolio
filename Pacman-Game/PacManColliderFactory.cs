using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class PacmanColliderFactory
    {
        public ICollision Create(Mode mode, ICharacter character)
        {
            return mode switch
            {
                Mode.Scared => new Hunter(),
                Mode.NotScared => new Hunted(character),
                _ => throw new ArgumentOutOfRangeException(nameof(mode))
            };
        }
    }
}
