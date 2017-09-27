using System.Collections.Generic;

namespace Minesweeper.Input
{
    public interface ILineReader
    {
        IEnumerable<string> ReadLines();
    }
}
