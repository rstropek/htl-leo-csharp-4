using Maze.Library;
using System;
using System.Collections;

namespace Maze.Tests
{
    /// <inheritdoc />
    internal class Robot : IRobot
    {
        private readonly BitArray maze;
        internal (int y, int x) position;
        private readonly int sideLength;
        private (int y, int x) exit;
        internal bool hcfCalled;

        public event EventHandler? ReachedExit;

        public Robot(BitArray maze, int sideLength, (int y, int x) position, (int y, int x) exit)
        {
            this.maze = maze;
            this.position = position;
            this.sideLength = sideLength;
            this.exit = exit;
        }

        /// <inheritdoc />
        public bool CanIMove(Direction direction)
        {
            if (hcfCalled)
            {
                throw new InvalidOperationException();
            }

            return direction switch
            {
                Direction.Up => position.y > 0 && !maze[(position.y - 1) * sideLength + position.x],
                Direction.Down => position.y < sideLength - 1 && !maze[(position.y + 1) * sideLength + position.x],
                Direction.Left => position.x > 0 && !maze[position.y * sideLength + position.x - 1],
                Direction.Right => position.x < sideLength - 1 && !maze[position.y * sideLength + position.x + 1],
                _ => throw new ArgumentOutOfRangeException(nameof(direction)),
            };
        }

        /// <inheritdoc />
        public void Move(Direction direction)
        {
            if (hcfCalled)
            {
                throw new InvalidOperationException();
            }

            if (!TryMove(direction))
            {
                throw new InvalidOperationException();
            }
        }

        /// <inheritdoc />
        public bool TryMove(Direction direction)
        {
            if (hcfCalled)
            {
                throw new InvalidOperationException();
            }

            if (!CanIMove(direction))
            {
                return false;
            }

            switch (direction)
            {
                case Direction.Up:
                    position.y--;
                    break;
                case Direction.Down:
                    position.y++;
                    break;
                case Direction.Left:
                    position.x--;
                    break;
                case Direction.Right:
                    position.x++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction));
            }

            if (position.Equals(exit) && ReachedExit != null)
            {
                ReachedExit(this, new EventArgs());
            }

            return true;
        }

        public void HaltAndCatchFire()
        {
            hcfCalled = true;
        }
    }
}
