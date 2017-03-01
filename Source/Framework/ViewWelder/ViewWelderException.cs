using System;

namespace ViewWelder
{
    public class ViewWelderException : Exception
    {
        public ViewWelderException(string message)
            : base(message)
        {
        }

        public ViewWelderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
