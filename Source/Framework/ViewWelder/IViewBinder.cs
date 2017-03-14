using System.Windows;
using ViewWelder.ViewModels;

namespace ViewWelder
{
    public interface IViewBinder
    {
        void Bind(ViewModelBase viewModel, FrameworkElement view, IViewResolver viewResolver);
    }
}
