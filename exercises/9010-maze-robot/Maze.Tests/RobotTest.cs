using Maze.Library;
using System;
using System.Collections;
using Xunit;

namespace Maze.Tests
{
    /// <summary>
    /// Tests the <see cref="Robot"/> class
    /// </summary>
    public class RobotTest
    {
        [Fact]
        public void CanIMoveBorder()
        {
            var maze = new BitArray(new[] { false });
            var robot = new Robot(maze, 1, (0, 0), (0, 0));
            Assert.False(robot.CanIMove(Direction.Down));
            Assert.False(robot.CanIMove(Direction.Up));
            Assert.False(robot.CanIMove(Direction.Left));
            Assert.False(robot.CanIMove(Direction.Right));
        }

        [Fact]
        public void CanIMoveBlocked()
        {
            var maze = new BitArray(new[] { true, true, true, true, false, true, true, true, true });
            var robot = new Robot(maze, 3, (1, 1), (2, 1));
            Assert.False(robot.CanIMove(Direction.Down));
            Assert.False(robot.CanIMove(Direction.Up));
            Assert.False(robot.CanIMove(Direction.Left));
            Assert.False(robot.CanIMove(Direction.Right));
        }

        [Fact]
        public void CanIMoveSuccess()
        {
            var maze = new BitArray(new[] { false, false, false, false, false, false, false, false, false });
            var robot = new Robot(maze, 3, (1, 1), (2, 1));
            Assert.True(robot.CanIMove(Direction.Down));
            Assert.True(robot.CanIMove(Direction.Up));
            Assert.True(robot.CanIMove(Direction.Left));
            Assert.True(robot.CanIMove(Direction.Right));
        }

        [Fact]
        public void TryMove()
        {
            var maze = new BitArray(new[] { false, false, false, false, false, false, false, true, false });
            var robot = new Robot(maze, 3, (0, 1), (2, 1));
            Assert.True(robot.TryMove(Direction.Down));
            Assert.False(robot.TryMove(Direction.Down));
        }

        [Fact]
        public void Move()
        {
            var maze = new BitArray(new[] { false, false, false, false, false, false, false, true, false });
            var robot = new Robot(maze, 3, (0, 1), (2, 1));
            robot.Move(Direction.Down);
            Assert.Throws<InvalidOperationException>(() => robot.Move(Direction.Down));
        }

        [Fact]
        public void ReachedEnd()
        {
            var maze = new BitArray(new[] { false, false, false, false, false, false, false, false, false });
            var robot = new Robot(maze, 3, (0, 1), (2, 1));
            var reachedEnd = false;
            robot.ReachedExit += (_, __) => reachedEnd = true;
            robot.Move(Direction.Down);
            robot.Move(Direction.Down);
            Assert.True(reachedEnd);
        }

        [Fact]
        public void HaltAndCatchFire()
        {
            var maze = new BitArray(new[] { false });
            var robot = new Robot(maze, 1, (0, 0), (0, 0));
            robot.HaltAndCatchFire();
            Assert.Throws<InvalidOperationException>(() => robot.CanIMove(Direction.Down));
            Assert.Throws<InvalidOperationException>(() => robot.TryMove(Direction.Down));
            Assert.Throws<InvalidOperationException>(() => robot.Move(Direction.Down));
        }
    }
}
