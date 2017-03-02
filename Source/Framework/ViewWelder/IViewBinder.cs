using System.Windows;

namespace ViewWelder
{
    public interface IViewBinder
    {
        void Bind(ViewModelBase viewModel, FrameworkElement view);
    }
}
