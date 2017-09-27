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
        }

        [Fact]
        public void TestProgrammableDroneInitializationValidProgram()
        {
            Action act = () => new ProgrammableDrone(50, 50, 10, 10, FacingDirection.North, "<*>*<<<>*>*<<*>*<*>>*<***");
            act.ShouldNotThrow<ArgumentException>();
        }
    }
}
