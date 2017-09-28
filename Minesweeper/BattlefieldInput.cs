using Minesweeper.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public sealed class BattlefieldInput
    {
        struct InputLimit
        {
            internal InputLimit(int xlimit, int ylimit)
            {
                XLimit = xlimit;
                YLimit = ylimit;
            }

            public int XLimit { get; }
            public int YLimit { get; }
        }

        public BattlefieldInput(ILineReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            ProgrammableDrones = ReadDronesFromInput(reader).AsReadOnly();
        }

        private static List<ProgrammableDrone> ReadDronesFromInput(ILineReader reader)
        {
            string[] allLines = reader.ReadLines().ToArray();
            if (allLines.Length == 0)
                throw new BattlefieldInputFormatException("Input cannot be empty.");
            if (allLines.Length %2 == 0)
                throw new BattlefieldInputFormatException("Number of input lines must be odd.");

            InputLimit limits = ReadInputLimits(allLines[0]);

            throw new NotImplementedException();
        }

        private static InputLimit ReadInputLimits(string line)
        {
            try
            {
                int[] limits = line.Split(' ').Select(int.Parse).ToArray();
                if (limits.Length != 2)
                    throw GetLimitException();
                if ((limits[0] < 0) || (limits[1] < 0))
                    throw GetLimitException();

                return new InputLimit(limits[0], limits[1]);
            }
            catch (FormatException e)
            {
                throw GetLimitException(e);
            }
        }

        private static BattlefieldInputFormatException GetLimitException(Exception e = null)
        {
            return new BattlefieldInputFormatException("First input line must contain exactly two positive integers separated by a space.", e);
        }

        public IReadOnlyList<ProgrammableDrone> ProgrammableDrones { get; }
    }
}
