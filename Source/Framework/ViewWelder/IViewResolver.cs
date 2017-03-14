using System.Windows;
using ViewWelder.ViewModels;

namespace ViewWelder
{
    public interface IViewResolver
    {
        FrameworkElement Resolve(ViewModelBase viewModel);
    }
}