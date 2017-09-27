using FluentAssertions;
using System;
using Xunit;

namespace Minesweeper.Tests
{
    public sealed class ProgrammableDroneTests
    {
        [Fact]
        public void TestProgrammableDroneInitialization()
        {
            var pd = new ProgrammableDrone(50, 50, 10, 10, FacingDirection.North, "");
            pd.Program.Should().Be(string.Empty);
        }

        [Fact]
        public void TestProgrammableDroneInitializationBadProgram()
        {
            Action act = () => new ProgrammableDrone(50, 50, 10, 10, FacingDirection.North, "xyz");
            act.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("program");
            act = () => new ProgrammableDrone(50, 50, 10, 10, FacingDirection.North, "xyz");
            act.ShouldThrow<ArgumentException>().And.ParamName.Should().Be("program");
        }

        [Fact]
        public void TestProgrammableDroneInitializationValidProgram()
        {
            Action act = () => new ProgrammableDrone(50, 50, 10, 10, FacingDirection.North, "<*>*<<<>*>*<<*>*<*>>*<***");
            act.ShouldNotThrow<ArgumentException>();
        }

        [Fact]
        public void TestProgrammableDroneProgramExecution()
        {
            var drone = new ProgrammableDrone(50, 50, 0, 0, FacingDirection.North, "");
            ProgrammableDrone newDrone = drone.ExecuteProgram();
            ReferenceEquals(drone, newDrone).Should().BeTrue();

            //Go up
            drone = new ProgrammableDrone(5, 5, 0, 0, FacingDirection.North, "********");
            newDrone = drone.ExecuteProgram();
            newDrone.X.Should().Be(0);
            newDrone.Y.Should().Be(5);
            newDrone.Direction.Should().Be(FacingDirection.North);

            //Go down
            drone = new ProgrammableDrone(5, 5, 0, 5, FacingDirection.South, "********");
            newDrone = drone.ExecuteProgram();
            newDrone.X.Should().Be(0);
            newDrone.Y.Should().Be(0);
            newDrone.Direction.Should().Be(FacingDirection.South);

            //Go east
            drone = new ProgrammableDrone(5, 5, 0, 0, FacingDirection.East, "********");
            newDrone = drone.ExecuteProgram();
            newDrone.X.Should().Be(5);
            newDrone.Y.Should().Be(0);
            newDrone.Direction.Should().Be(FacingDirection.East);

            //Go west
            drone = new ProgrammableDrone(5, 5, 5, 0, FacingDirection.West, "********");
            newDrone = drone.ExecuteProgram();
            newDrone.X.Should().Be(0);
            newDrone.Y.Should().Be(0);
            newDrone.Direction.Should().Be(FacingDirection.West);
        }

        [Fact]
        public void TestProgrammableDroneProgramExecutionWithRightSpins()
        {            
            var drone = new ProgrammableDrone(5, 5, 0, 0, FacingDirection.North, "*****>*****>*****>*****");
            ProgrammableDrone newDrone = drone.ExecuteProgram();
            newDrone.X.Should().Be(0);
            newDrone.Y.Should().Be(0);
            newDrone.Direction.Should().Be(FacingDirection.West);
        }

        [Fact]
        public void TestProgrammableDroneProgramExecutionWithLeftSpins()
        {
            var drone = new ProgrammableDrone(5, 5, 0, 0, FacingDirection.East, "*****<*****<*****<*****");
            ProgrammableDrone newDrone = drone.ExecuteProgram();
            newDrone.X.Should().Be(0);
            newDrone.Y.Should().Be(0);
            newDrone.Direction.Should().Be(FacingDirection.South);
        }

        [Fact]
        public void TestProgrammableDroneProgramExecutionWithMixedSpins()
        {
            var drone = new ProgrammableDrone(5, 5, 0, 0, FacingDirection.East, "*****<*<*****>*>*****<*<*****>*>*****<*<*****");
            ProgrammableDrone newDrone = drone.ExecuteProgram();
            newDrone.X.Should().Be(0);
            newDrone.Y.Should().Be(5);
            newDrone.Direction.Should().Be(FacingDirection.West);
        }
    }
}
