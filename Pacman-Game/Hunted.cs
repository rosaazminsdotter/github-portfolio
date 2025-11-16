using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class Hunted : ICollision
    {
        ICharacter character;
        public Hunted(ICharacter character)
        {
            this.character = character;
        }


        public void CollideWith(ICollision other)
        {
            other.CollisionEffect(this);
        }
        public void CollisionEffect(Hunter other)
        {
            character.Die();
        }


        public void CollisionEffect(Hunted other)
        {
            //nothings
        }
    }

}
