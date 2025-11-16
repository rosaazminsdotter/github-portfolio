using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    // KRAV #4:
    // 1: Factory Method Pattern
    // 2: IFruitFactory har två subtyper för två olika svårighetsnivåer. Båda lägger in frukt på brädet som den returnerar. 
    //    Easy lägger in frukt och ibland extra liv, medan Mard bara lägger in frukt.
    // 3: Brädet byter utseende under spelets gång (random) och går därför inte att placera in frukterna/extra-liv från start
    public interface IFruitFactory
    {
        Board board { get; }
        Random random { get; }
        void Update(Game game);
        void FruitEaten();
    }
}
