using System;

namespace ViewWelder
{
    public class ViewResolverInflector : IViewResolverInflector
    {
        public string InflectViewName(string viewModelName)
        {
            if (viewModelName == null)
                throw new ArgumentNullException(nameof(viewModelName));

            return viewModelName.Replace("ViewModel", "View");
        }
    }
}
