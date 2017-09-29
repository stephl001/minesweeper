using FluentAssertions;
using Minesweeper.Tests.Input;
using System.IO;
using Xunit;

namespace Minesweeper.Tests
{
    public sealed class LineReaderTests
    {
        [Fact]
        public void TestReadLines()
        {
            using (StreamReader linereader = new ResourceLineReader("Sample.txt"))
            {
                linereader.EndOfStream.Should().BeFalse();
                int lineCount = 0;
                while (linereader.ReadLine() != null)
                    lineCount++;
                lineCount.Should().Be(7);
                linereader.EndOfStream.Should().BeTrue();
            }
        }

        [Fact]
        public void TestMemoryReadLines()
        {
            using (StreamReader linereader = new MemoryLineReader(new string[] { }))
            {
                linereader.EndOfStream.Should().BeTrue();
            }
        }
    }
}
