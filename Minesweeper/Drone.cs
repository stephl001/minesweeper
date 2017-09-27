using System;
using System.Collections.Generic;

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

        private static readonly IDictionary<FacingDirection, Predicate<Drone>> _canMovePredicates = new Dictionary<FacingDirection, Predicate<Drone>>
        {
            { FacingDirection.East, d => d.X < d.XLimit },
            { FacingDirection.West, d => d.X > 0 },
            { FacingDirection.North, d => d.Y < d.YLimit },
            { FacingDirection.South, d => d.Y > 0 }
        };

        private static readonly IDictionary<FacingDirection, Func<Drone, Drone>> _moveHandlers = new Dictionary<FacingDirection, Func<Drone, Drone>>
        {
            { FacingDirection.East, d => new Drone(d.XLimit, d.YLimit, d.X+1, d.Y, d.Direction) },
            { FacingDirection.West, d => new Drone(d.XLimit, d.YLimit, d.X-1, d.Y, d.Direction) },
            { FacingDirection.North, d => new Drone(d.XLimit, d.YLimit, d.X, d.Y+1, d.Direction) },
            { FacingDirection.South, d => new Drone(d.XLimit, d.YLimit, d.X, d.Y-1, d.Direction) }
        };

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
            CanMoveForward = _canMovePredicates[dir](this);
            _strRep = $"{X} {Y} {Direction.ToString()[0]}";
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

        public bool CanMoveForward { get; }

        private readonly string _strRep;
        public override string ToString()
        {
            return _strRep;
        }

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

        public Drone MoveForward()
        {
            if (!CanMoveForward)
                return this;

            return _moveHandlers[Direction](this);
        }
    }
}
