using System;

namespace HVAC
{
    internal class UnspecifiedTypeOfFlowException : Exception
    {
        public UnspecifiedTypeOfFlowException()
        {
        }
        public UnspecifiedTypeOfFlowException(string message)
        : base(message)
        {
        }

        public UnspecifiedTypeOfFlowException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}