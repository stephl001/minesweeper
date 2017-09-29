using FluentAssertions;
using Minesweeper.Input;
using Minesweeper.Tests.Input;
using System;
using System.Linq;
using Xunit;

namespace Minesweeper.Tests
{
    public sealed class BattlefieldInputTests
    {
        [Fact]
        public void TestNullInitialization()
        {
            Action act = () => new BattlefieldInput(null);
            act.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be("reader");
        }

        [Fact]
        public void TestBadLimitInitialization()
        {
            var input = new BattlefieldInput(new MemoryLineReader(new string[] { }));
            Action act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Input cannot be empty.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "a" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "8 a" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "9" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "-9 12" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "9 -12" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "9 12" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldNotThrow<BattlefieldInputFormatException>();
        }

        [Fact]
        public void TestNoDronesInitialization()
        {
            var input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5" }));
            input.GetProgrammableDrones().Should().BeEmpty();
        }

        [Fact]
        public void TestNoDronesInitializationInvalidFormat()
        {
            var input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "anything" }));
            Action act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Missing input line for drone initialization.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "anything", "whatever" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Invalid drone initial position format.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "3 2 Z", "whatever" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Invalid drone initial position format.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "3", "whatever" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Invalid drone initial position format.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "", "whatever" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Invalid drone initial position format.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "3 3 W", "wrong program" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Invalid drone initial position format.");

            input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "8 3 W", ">***<**" }));
            act = () => input.GetProgrammableDrones().Count();
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Invalid drone initial position format.");
        }
    }
}
