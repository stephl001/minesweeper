using System.IO;
using System;
using System.Text;

namespace Minesweeper.Tests.Input
{
    public sealed class MemoryLineReader : StreamReader
    {
        internal MemoryLineReader(string[] lines)
            : base(GetStreamFromLines(lines))
        {
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
