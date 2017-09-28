using Minesweeper.Input;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Minesweeper.Tests.Input
{
    public sealed class ResourceLineReader : ILineReader
    {
        private readonly StreamReader _reader;

        internal ResourceLineReader(string resourceName)
        {
            string resourcePath = $"Minesweeper.Tests.Input.{resourceName}";

            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
            _reader = new StreamReader(s);
            EndOfStream = _reader.EndOfStream;
        }

        public bool EndOfStream { get; private set; }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed)
                return;

            _reader.Dispose();
        }

        public IEnumerable<string> ReadLines()
        {
            while (!_reader.EndOfStream)
            {
                yield return _reader.ReadLine();
                EndOfStream = _reader.EndOfStream;
            }
        }
    }
}
