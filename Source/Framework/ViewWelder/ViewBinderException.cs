using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewWelder
{
    public class ViewBinderException : ViewWelderException
    {
        public ViewBinderException(string message)
            : base(message)
        {
        }
    }
}
