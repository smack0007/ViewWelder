using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace ViewWelder
{
    internal static class VisualTreeExtensions
    {
        public static T GetVisualAncestor<T>(this DependencyObject element)
            where T : DependencyObject
        {
            var pointer = element;

            while (pointer != null && !(pointer is T))
                pointer = VisualTreeHelper.GetParent(pointer);

            return pointer as T;
        }

        public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject element)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(element);

            for (int i = 0; i < childCount; i++)
            {
                yield return VisualTreeHelper.GetChild(element, i);
            }
        }
    }
}
