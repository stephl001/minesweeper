using FluentAssertions;
using Minesweeper.Input;
using Minesweeper.Tests.Input;
using System.Linq;
using Xunit;

namespace Minesweeper.Tests
{
    public sealed class LineReaderTests
    {
        [Fact]
        public void TestReadLines()
        {
            ILineReader linereader = new ResourceLineReader("Sample.txt");
            linereader.ReadLines().Count().Should().Be(7);
        }
    }
}
