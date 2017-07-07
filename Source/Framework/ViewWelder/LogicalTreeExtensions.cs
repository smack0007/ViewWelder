using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ViewWelder
{
    internal static class LogicalTreeExtensions 
    {
        public static IEnumerable<DependencyObject> GetLogicalChildren(this DependencyObject element)
        {
            return LogicalTreeHelper.GetChildren(element).Cast<DependencyObject>();
        }
    }
}
