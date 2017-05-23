using System.Windows;
using System.Windows.Media;

namespace ViewWelder
{
    internal static class VisualTreeHelperEx
    {
        public static T GetAncestor<T>(DependencyObject element)
            where T : DependencyObject
        {
            var pointer = element;

            while (pointer != null && !(pointer is T))
                pointer = VisualTreeHelper.GetParent(pointer);

            return pointer as T;
        }
    }
}
