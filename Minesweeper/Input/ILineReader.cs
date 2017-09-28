using System;
using System.Collections.Generic;

namespace Minesweeper.Input
{
    public interface ILineReader : IDisposable
    {
        bool EndOfStream { get; }
        IEnumerable<string> ReadLines();
    }
}
