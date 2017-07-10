using System;

namespace ViewWelder
{
    public class ViewResolverException : ViewWelderException
    {
        public ViewResolverException(string message)
            : base(message)
        {
        }

        public ViewResolverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
