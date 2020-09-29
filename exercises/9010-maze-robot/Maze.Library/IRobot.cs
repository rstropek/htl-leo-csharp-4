using System;

namespace Maze.Library
{
    /// <summary>
    /// Represents a robot moving through a maze
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Checks whether the robot can move into a given direction
        /// </summary>
        /// <param name="direction">Directionto move to</param>
        /// <returns>
        /// <c>true</c> if the robot can move into the given direction, otherwise false.
        /// </returns>
        bool CanIMove(Direction direction);

        /// <summary>
        /// Tries to move into a given direction
        /// </summary>
        /// <param name="direction">Directionto move to</param>
        /// <returns>
        /// <c>true</c> if the robot could move into the given direction, otherwise false.
        /// </returns>
        /// <remarks>
        /// If the return value is <c>true</c>, the robot has moved (i.e. its position has changed
        /// accordingly). If the return value is <c>false</c>, the robot's position has not changed.
        /// </remarks>
        bool TryMove(Direction direction);

        /// <summary>
        /// Move into a given direction
        /// </summary>
        /// <param name="direction">Directionto move to</param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the robot cannot move into the given direction.
        /// </exception>
        /// <remarks>
        /// If no exception is thrown, the robot has moved (i.e. its position has changed
        /// accordingly). In case of an exception, the robot's position has not changed.
        /// </remarks>
        void Move(Direction direction);


        /// <summary>
        /// Shut down the robot
        /// </summary>
        /// <remarks>
        /// This is typically done when the robot cannot find an exit.
        /// </remarks>
        void HaltAndCatchFire();

        /// <summary>
        /// Raised when the robot has reached the exit
        /// </summary>
        event EventHandler ReachedExit;
    }
}
