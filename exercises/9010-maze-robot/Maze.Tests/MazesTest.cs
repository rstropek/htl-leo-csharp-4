using Maze.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Maze.Tests
{
    /// <summary>
    /// Tests the <see cref="Mazes"/>
    /// </summary>
    public class MazesTest
    {
        [Fact]
        public void Straight()
        {
            CheckMaze(Mazes.Straight, Mazes.StraightStart, Mazes.StraightEnd, Mazes.StraightSolution);
        }

        [Fact]
        public void Snake()
        {
            CheckMaze(Mazes.Snake, Mazes.SnakeStart, Mazes.SnakeEnd, Mazes.SnakeSolution);
        }

        [Fact]
        public void DeadEnd()
        {
            CheckMaze(Mazes.DeadEnd, Mazes.DeadEndStart, Mazes.DeadEndEnd, Mazes.DeadEndSolution);
        }

        [Fact]
        public void Circle()
        {
            CheckMaze(Mazes.Circle, Mazes.CircleStart, Mazes.CircleEnd, Mazes.CircleSolution);
        }

        private void CheckMaze(string mazeString, (int y, int x) start, (int y, int x) end, IEnumerable<Direction> moves)
        {
            var maze = new BitArray(Mazes.MazeFromString(mazeString));
            var robot = new Robot(maze, (int)Math.Sqrt(maze.Count), start, end);
            foreach (var move in moves)
            {
                robot.Move(move);
            }

            Assert.Equal(end, robot.position);
        }
    }
}
