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

        struct InputDrone
        {
            internal int X;
            internal int Y;
            internal FacingDirection Direction;
        }

        private readonly StreamReader _reader;

        public BattlefieldInput(StreamReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            _reader = reader;
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

        public IEnumerable<ProgrammableDrone> GetProgrammableDrones()
        {
            if (_reader.EndOfStream)
                throw new BattlefieldInputFormatException("Input cannot be empty.");

            InputLimit limits = ReadInputLimits(_reader);
            while (!_reader.EndOfStream)
            {
                string initialDronePosDefinition = _reader.ReadLine();
                if (_reader.EndOfStream)
                    throw new BattlefieldInputFormatException("Missing input line for drone initialization.");
                string program = _reader.ReadLine();

                ProgrammableDrone drone = BuildProgrammableDroneFromInputs(limits, initialDronePosDefinition, program);
                yield return drone;
            }
        }

        private static ProgrammableDrone BuildProgrammableDroneFromInputs(InputLimit limits, string initialDronePosDefinition, string program)
        {
            InputDrone inputDrone = ReadDroneInput(initialDronePosDefinition);
            ProgrammableDrone drone;
            try
            {
                drone = new ProgrammableDrone(limits.XLimit, limits.YLimit, inputDrone.X, inputDrone.Y, inputDrone.Direction, program);
            }
            catch (ArgumentException ex)
            {
                throw new BattlefieldInputFormatException("Invalid drone initial position format.", ex);
            }

            return drone;
        }

        private static InputDrone ReadDroneInput(string input)
        {
            InputDrone droneInput;

            string[] initArgs = input.Split(' ');
            try
            {                
                droneInput.X = int.Parse(initArgs[0]);
                droneInput.Y = int.Parse(initArgs[1]);
                droneInput.Direction = GetFacingDirection(initArgs[2]);
            }
            catch (Exception ex) when (ex is FormatException || ex is KeyNotFoundException || ex is IndexOutOfRangeException)
            {
                throw new BattlefieldInputFormatException("Invalid drone initial position format.", ex);
            }

            return droneInput;
        }

        private static readonly IDictionary<string, FacingDirection> _directionMappings = new Dictionary<string, FacingDirection>
        {
            { "E", FacingDirection.East },
            { "W", FacingDirection.West },
            { "S", FacingDirection.South },
            { "N", FacingDirection.North },
        };
        private static FacingDirection GetFacingDirection(string dir)
        {
            return _directionMappings[dir];
        }
    }
}
