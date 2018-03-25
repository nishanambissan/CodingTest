using System;
namespace ATMMachine.BusinessLogic.CustomExceptions
{
    public class OutOfMoneyException : Exception 
    {
        public OutOfMoneyException()
        {
        }

        public OutOfMoneyException(string message)
        : base(message)
        {
        }

        public OutOfMoneyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
