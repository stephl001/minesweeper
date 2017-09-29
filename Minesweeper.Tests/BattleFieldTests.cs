using FluentAssertions;
using Minesweeper.Tests.Input;
using System.IO;
using System.Text;
using Xunit;

namespace Minesweeper.Tests
{
    public sealed class BattleFieldTests
    {
        [Fact]
        public void TestExecution()
        {
            RunSimulation("Sample.txt", "SampleExpectedOutput.txt").Should().BeTrue();
            RunSimulation("Sample2.txt", "Sample2ExpectedOutput.txt").Should().BeTrue();
        }

        private bool RunSimulation(string sourceInputResource, string expectedOutputResource)
        {
            using (StreamReader reader = new ResourceLineReader(sourceInputResource))
            {
                var battlefieldInput = new BattlefieldInput(reader);
                var battlefield = new Battlefield(battlefieldInput);

                using (var memStream = new MemoryStream())
                {
                    using (StreamWriter sw = new StreamWriter(memStream))
                    {
                        battlefield.Execute(sw);
                        sw.Flush();
                        memStream.Seek(0L, SeekOrigin.Begin);

                        bool identical = IsOutputIdenticalToExpected(memStream, expectedOutputResource);
                        return identical;
                    }
                }
            }
        }

        private bool IsOutputIdenticalToExpected(MemoryStream memStream, string resourceName)
        {
            using (StreamReader source = new StreamReader(memStream, Encoding.UTF8, false, 1024, true))
            {
                using (StreamReader target = new ResourceLineReader(resourceName))
                {
                    while (!source.EndOfStream && !target.EndOfStream)
                    {
                        if (source.ReadLine() != target.ReadLine())
                            return false;
                    }

                    return (source.EndOfStream && target.EndOfStream);
                }
            }
        }
    }
}
