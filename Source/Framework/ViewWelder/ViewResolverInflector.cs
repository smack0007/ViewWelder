using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewWelder
{
    public class ViewResolverInflector
    {
        public virtual string InflectViewName(string viewModelName)
        {
            if (viewModelName == null)
                throw new ArgumentNullException(nameof(viewModelName));

            return viewModelName.Replace("ViewModel", "View");
        }
    }
}
