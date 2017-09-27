using System;
using System.Linq;

namespace Minesweeper
{
    public class ProgrammableDrone : Drone
    {
        public ProgrammableDrone(int xlimit, int ylimit, int x, int y, FacingDirection dir, string program)
            : base(xlimit, ylimit, x, y, dir)
        {
            if (program.Any(IsInvalidProgramInput))
                throw new ArgumentException("Only <,> and * are accepted as program input.", nameof(program));

            Program = program;
        }

        private static bool IsInvalidProgramInput(char c)
        {
            return ((c != '*') && (c != '<') && (c != '>'));
        }

        public string Program { get; }
    }
}
