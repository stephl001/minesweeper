using System.IO;
using System.Reflection;

namespace Minesweeper.Tests.Input
{
    public sealed class ResourceLineReader : StreamReader
    {
        internal ResourceLineReader(string resourceName)
            : base(GetStreamFromResourceName(resourceName))
        {            
        }

        private static Stream GetStreamFromResourceName(string resourceName)
        {
            string resourcePath = $"Minesweeper.Tests.Input.{resourceName}";
            Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);

            return s;
        }
    }
}
