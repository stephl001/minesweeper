using Minesweeper.Input;
using System;
using System.Collections.Generic;
using System.IO;
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

            internal int XLimit;
            internal int YLimit;
        }

        public BattlefieldInput(StreamReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            ProgrammableDrones = ReadDronesFromInput(reader).AsReadOnly();
        }

        private static List<ProgrammableDrone> ReadDronesFromInput(StreamReader reader)
        {
            if (reader.EndOfStream)
                throw new BattlefieldInputFormatException("Input cannot be empty.");
            
            InputLimit limits = ReadInputLimits(reader);
            return new List<ProgrammableDrone>();
        }

        private static InputLimit ReadInputLimits(TextReader reader)
        {
            try
            {
                string line = reader.ReadLine();
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
