using System;

namespace Minesweeper.Input
{
    public class BattlefieldInputFormatException : Exception
    {
        protected internal BattlefieldInputFormatException(string message)
            : base(message)
        {
        }

        protected internal BattlefieldInputFormatException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}
