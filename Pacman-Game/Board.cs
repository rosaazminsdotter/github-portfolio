using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacManGame
{
    public class Board : IEnumerable<((int, int), IBoardObject)>
    {
        public List<List<IBoardObject>> grid { get; private set; }

        public int Height => grid.Count; 
        public int Width => grid[0].Count;


        public Board()
        {
            string[] maze =
            {
            "#################################",
            "#...#.......................#...#",
            "#.#.#.#########.#.#########.#.#.#",
            "#.......#.......#.......#.......#",
            "###.#.#.#.#####.#.#####.#.#.#.###",
            "#...#.#.#.#...#.#.#...#.#.#.#...#",
            "#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#",
            "#...#.#.#.#...#.#.#...#.#.#.#...#",
            "#####.#.#.##.##.#.##.##.#.#.#####",
            "#.....#.#...............#.#.....#",
            "#.#####.#.##.###.###.##.#.#####.#",
            "#............#.....#............#",
            "#.#####.#.##.#######.##.#.#####.#",
            "#.....#.#...............#.#.....#",
            "#####.#.#.##.##.#.##.##.#.#.#####",
            "#...#.#.#.#...#.#.#...#.#.#.#...#",
            "#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#.#",
            "#...#.#.#.#...#.#.#...#.#.#.#...#",
            "###.#.#.#.#####.#.#####.#.#.#.###",
            "#.......#.......#.......#.......#",
            "#.#.#.#########.#.#########.#.#.#",
            "#...#.......................#...#",
            "#################################"
        };


            grid = new List<List<IBoardObject>>();


            for (int y = 0; y < maze.Length; y++)
            {
                var row = new List<IBoardObject>();
                for (int x = 0; x < maze[y].Length; x++)
                {
                    char c = maze[y][x];
                    row.Add(c switch
                    {
                        '#' => new Wall(),
                        '.' => new Empty(),
                        _ => throw new Exception($"Okänt tecken: {c} på ({x},{y})")
                    });
                }
                grid.Add(row);
            }
        }


        public IEnumerator<((int, int), IBoardObject)> GetEnumerator()
            => new BoardIterator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

