using Minesweeper.Input;
using System.Collections.Generic;

namespace Minesweeper.Tests.Input
{
    public sealed class MemoryLineReader : ILineReader
    {
        private readonly string[] _allLines;
        private int _currentIndex;

        internal MemoryLineReader(string[] lines)
        {
            _allLines = lines;
        }

        public bool EndOfStream
        {
            get
            {
                return (_currentIndex < _allLines.Length);
            }
        }

        public void Dispose()
        {
        }

        public IEnumerable<string> ReadLines()
        {
            foreach (string line in _allLines)
            {
                yield return line;
                _currentIndex++;
            }
        }
    }
}
