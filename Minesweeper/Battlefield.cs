using System;
using System.IO;

namespace Minesweeper
{
    public sealed class Battlefield
    {
        private readonly BattlefieldInput _input;

        public Battlefield(BattlefieldInput input)
        {
            _input = input;
        }

        public void Execute(StreamWriter sw)
        {
            foreach (ProgrammableDrone pd in _input.GetProgrammableDrones())
            {
                Drone finalDrone = pd.ExecuteProgram();
                sw.WriteLine(finalDrone.ToString());
            }
        }
    }
}
