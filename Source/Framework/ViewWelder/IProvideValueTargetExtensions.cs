using System.Windows.Markup;

namespace ViewWelder
{
    internal static class IProvideValueTargetExtensions
    {
        public static bool TargetObjectIsSharedDependencyProperty(this IProvideValueTarget target)
        {
            return target.TargetObject.GetType().FullName == "System.Windows.SharedDp";
        }
    }
}
