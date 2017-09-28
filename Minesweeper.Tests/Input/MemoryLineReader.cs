using Minesweeper.Input;
using System.Collections.Generic;

namespace Minesweeper.Tests.Input
{
    public sealed class MemoryLineReader : ILineReader
    {
        private readonly string[] _allLines;

        internal MemoryLineReader(string[] lines)
        {
            _allLines = lines;
        }

        public IEnumerable<string> ReadLines()
        {
            return _allLines;
        }
    }
}
