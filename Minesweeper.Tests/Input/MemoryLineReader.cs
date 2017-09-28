using System.IO;
using System;
using System.Text;

namespace Minesweeper.Tests.Input
{
    public sealed class MemoryLineReader : StreamReader
    {
        private readonly string[] _allLines;
        private int _currentIndex;

        internal MemoryLineReader(string[] lines)
            : base(GetStreamFromLines(lines))
        {
            _allLines = lines;
        }

        private static Stream GetStreamFromLines(string[] lines)
        {
            MemoryStream ms = new MemoryStream();
            using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8, 1024, true))
                Array.ForEach(lines, sw.WriteLine);

            ms.Seek(0L, SeekOrigin.Begin);
            return ms;
        }
    }
}
