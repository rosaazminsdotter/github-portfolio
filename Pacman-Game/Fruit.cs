using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    internal class Fruit : IBoardObject
    {
        public char character { get; private set; }
        public bool canMove { get; } = true;
        public int points { get; private set; }
        public string name { get; private set; }
        public ConsoleColor color { get; private set; }
        private IFruitFactory factory;
        private Game game;

        public Fruit(char symbol, int points, string name, ConsoleColor color, IFruitFactory factory, Game game)
        {
            this.character = symbol;
            this.points = points;
            this.name = name;
            this.color = color;
            this.factory = factory;
            this.game = game;
        }

        public void Interact(Pacman pacman, Game game)
        {
            pacman.AddPoints(points);
            pacman.AddFruit(name);
            factory.FruitEaten();
            game.RemoveBoardObject(pacman.currentPosition);
        }
    }
}
