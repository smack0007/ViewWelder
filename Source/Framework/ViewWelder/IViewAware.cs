using System.Windows;

namespace ViewWelder
{
    public interface IViewAware
    {
        void SetView(FrameworkElement view);
    }
}
