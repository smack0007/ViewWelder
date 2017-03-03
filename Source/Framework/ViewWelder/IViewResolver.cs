using System.Windows;

namespace ViewWelder
{
    public interface IViewResolver
    {
        FrameworkElement Resolve(ViewModelBase viewModel);
    }
}