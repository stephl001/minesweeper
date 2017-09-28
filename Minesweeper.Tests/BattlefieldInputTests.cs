using FluentAssertions;
using Minesweeper.Input;
using Minesweeper.Tests.Input;
using System;
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
            Action act = () => new BattlefieldInput(new MemoryLineReader(new string[] { }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Input cannot be empty.");
            act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "a" }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");
            act.ShouldThrow<BattlefieldInputFormatException>().And.InnerException.Should().BeOfType<FormatException>();
            act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "" }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");
            act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "8 a" }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");
            act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "9" }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");
            act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "-9 12" }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");
            act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "9 -12" }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("First input line must contain exactly two positive integers separated by a space.");

            act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "9 12" }));
            act.ShouldNotThrow<BattlefieldInputFormatException>();
        }

        [Fact]
        public void TestBadDronesInitialization()
        {
            Action act = () => new BattlefieldInput(new MemoryLineReader(new string[] { "5 5", "test" }));
            act.ShouldThrow<BattlefieldInputFormatException>().And.Message.Should().Be("Number of lines in input must be odd.");
        }

        [Fact]
        public void TestNoDronesInitialization()
        {
            var input = new BattlefieldInput(new MemoryLineReader(new string[] { "5 5" }));
            input.ProgrammableDrones.Should().BeEmpty();
        }
    }
}
