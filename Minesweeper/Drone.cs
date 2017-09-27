using System;

namespace Minesweeper
{
    public enum FacingDirection
    {
        East,
        North,
        West,
        South
    };

    public class Drone
    {
        private const int DirectionsCount = 4;

        public Drone(int xlimit, int ylimit, int x, int y)
            : this(xlimit, ylimit, x, y, FacingDirection.East)
        {
        }

        public Drone(int xlimit, int ylimit, int x, int y, FacingDirection dir)
        {
            if (xlimit < 0)
                throw new ArgumentOutOfRangeException(nameof(xlimit), "A limit cannot be negative.");
            if (ylimit < 0)
                throw new ArgumentOutOfRangeException(nameof(ylimit), "A limit cannot be negative.");
            if ((x < 0) || (x > xlimit))
                throw new ArgumentOutOfRangeException(nameof(x), "X position must be greater or equal to 0 and less or equal to XLimit.");
            if ((y < 0) || (y > ylimit))
                throw new ArgumentOutOfRangeException(nameof(y), "Y position must be greater or equal to 0 and less or equal to YLimit.");
            XLimit = xlimit;
            YLimit = ylimit;
            X = x;
            Y = y;
            Direction = dir;
        }

        private Drone(Drone sourceDrone, FacingDirection direction)
            : this(sourceDrone.XLimit, sourceDrone.YLimit, sourceDrone.X, sourceDrone.Y, direction)
        {
        }

        public int XLimit { get; }
        public int YLimit { get; }

        public int X { get; }
        public int Y { get; }

        public FacingDirection Direction { get; }

        public Drone SpinLeft()
        {
            FacingDirection newDirection = (FacingDirection)(((int)Direction + 1) % DirectionsCount);
            return new Drone(this, newDirection);
        }

        public Drone SpinRight()
        {
            FacingDirection newDirection = (FacingDirection)(((int)Direction + DirectionsCount - 1) % DirectionsCount);
            return new Drone(this, newDirection);
        }
    }
}
