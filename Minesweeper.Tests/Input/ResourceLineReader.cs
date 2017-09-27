using Minesweeper.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Tests.Input
{
    public sealed class ResourceLineReader : ILineReader
    {
        private readonly string _resourcePath;

        internal ResourceLineReader(string resourceName)
        {
            _resourcePath = $"Minesweeper.Tests.Input.{resourceName}";
        }

        public IEnumerable<string> ReadLines()
        {
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(_resourcePath))
            {
                using (StreamReader sr = new StreamReader(s))
                {
                    while (!sr.EndOfStream)
                        yield return sr.ReadLine();
                }
            }
        }
    }
}
