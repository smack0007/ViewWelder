using System.Windows;

namespace ViewWelder
{
    public interface IViewResolver
    {
        FrameworkElement Resolve(ViewModel viewModel);
    }
}