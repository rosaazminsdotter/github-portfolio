using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soduko_Grupp47
{
    public class Player
    {
        public string Name { get; }
       
        public Player(string a)
        {
            this.Name = a;
        }

        // 1: Overloading av konstruktorer
        // 2: För att göra spelet mer personligt vill vi tilltala spelaren, därför frågar vi om deras namn i början. Dessa konstruktorer körs när vi instatsierar ett objektav Player i Play klassen.
        // 3: Vi vill inte att spelaren ska BEHÖVA skriva sitt namn för att spelet ska fungera, därför har vi skapat två olika kontstruktorer beroende på input. Om de skickar in en string så blir det deras namn, och om de trycker enter utan att skriva så blir de kallade "anonymus".
        public Player() 
        {
            this.Name = "Anonymus";
        }
        
    }
}
