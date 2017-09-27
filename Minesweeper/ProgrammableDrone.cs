using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public class ProgrammableDrone : Drone
    {
        private delegate Drone DroneCommand(Drone d);
         
        public ProgrammableDrone(int xlimit, int ylimit, int x, int y, FacingDirection dir, string program)
            : base(xlimit, ylimit, x, y, dir)
        {
            if (program.Any(IsInvalidProgramInput))
                throw new ArgumentException("Only <,> and * are accepted as program input.", nameof(program));

            Program = program;
        }

        private ProgrammableDrone(Drone drone)
            : this(drone.XLimit, drone.YLimit, drone.X, drone.Y, drone.Direction, string.Empty)
        {
        }

        private static bool IsInvalidProgramInput(char c)
        {
            return ((c != '*') && (c != '<') && (c != '>'));
        }

        public string Program { get; }

        public ProgrammableDrone ExecuteProgram()
        {
            Drone startingDrone = this;
            Drone finalDrone = GetCommandsFromProgram().Aggregate(startingDrone, (currentDrone, command) => command(currentDrone));
            if (ReferenceEquals(startingDrone, finalDrone))
                return this;

            return new ProgrammableDrone(finalDrone);
        }

        private static readonly IDictionary<char, DroneCommand> _commandsMap = new Dictionary<char, DroneCommand>
        {
            { '<', d => d.SpinLeft() },
            { '>', d => d.SpinRight() },
            { '*', d => d.MoveForward() }
        };
        private IEnumerable<DroneCommand> GetCommandsFromProgram()
        {
            return Program.Select(c => _commandsMap[c]);
        }
    }
}
