using Minesweeper;
using System;
using System.IO;

namespace BattlefieldExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader(args[0]))
            {
                BattlefieldInput input = new BattlefieldInput(sr);
                Battlefield battlefield = new Battlefield(input);

                battlefield.Execute(Console.Out);
            }
        }
    }
}
