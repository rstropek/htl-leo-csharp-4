using Maze.Library;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Maze.Tests
{
    internal static class Mazes
    {
        public const string Straight = "         ";
        public static readonly (int y, int x) StraightStart = (0, 0);
        public static readonly (int y, int x) StraightEnd = (0, 2);
        public static readonly IEnumerable<Direction> StraightSolution = new[]
        {
            Direction.Right, Direction.Right
        };

        public const string Snake = " X   X   X X X X   X   XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        public static readonly (int y, int x) SnakeStart = (0, 0);
        public static readonly (int y, int x) SnakeEnd = (0, 7);
        public static readonly IEnumerable<Direction> SnakeSolution = new[]
        {
            Direction.Down, Direction.Down, Direction.Right, Direction.Right, 
            Direction.Up, Direction.Up, Direction.Right, Direction.Right, 
            Direction.Down, Direction.Down, Direction.Right, Direction.Right, 
            Direction.Up, Direction.Up, Direction.Right
        };

        public const string DeadEnd = " X X X      X  XXXXX        XXXXXXXXXXXXXXXXXXXXX";
        public static readonly (int y, int x) DeadEndStart = (0, 0);
        public static readonly (int y, int x) DeadEndEnd = (0, 6);
        public static readonly IEnumerable<Direction> DeadEndSolution = new[]
        {
            Direction.Down, Direction.Down, Direction.Down,
            Direction.Right, Direction.Right, Direction.Right, Direction.Right, Direction.Right, Direction.Right,
            Direction.Up, Direction.Up, Direction.Up
        };

        public const string Circle = @" X   X  X X      X X  X   X XXXXXXXXXXXXXXXXXXXXX";
        public static readonly (int y, int x) CircleStart = (0, 0);
        public static readonly (int y, int x) CircleEnd = (3, 6);
        public static readonly IEnumerable<Direction> CircleSolution = new[]
        {
            Direction.Down, Direction.Down, Direction.Right, Direction.Right, 
            Direction.Down, Direction.Right, Direction.Right, 
            Direction.Up, Direction.Up, Direction.Right, Direction.Right,
            Direction.Down, Direction.Down
        };

        public const string NoSolution = @" X XXXXXX";
        public static readonly (int y, int x) NoSolutionStart = (0, 0);
        public static readonly (int y, int x) NoSolutionEnd = (0, 2);

        public static BitArray MazeFromString(string source) => new BitArray(source.Select(c => c != ' ').ToArray());
    }
}
