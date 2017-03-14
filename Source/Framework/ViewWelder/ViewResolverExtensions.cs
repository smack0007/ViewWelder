using System.Windows;
using ViewWelder.ViewModels;

namespace ViewWelder
{
    public static class ViewResolverExtensions
    {
        public static TView Resolve<TView>(this IViewResolver viewResolver, ViewModelBase viewModel)
            where TView : FrameworkElement
        {
            return (TView)viewResolver.Resolve(viewModel);
        }

        public static object Resolve<TViewModel>(this IViewResolver viewResolver)
            where TViewModel : ViewModelBase, new()
        {
            return viewResolver.Resolve(new TViewModel());
        }

        public static TView Resolve<TViewModel, TView>(this IViewResolver viewResolver)
            where TViewModel : ViewModelBase, new()
            where TView : FrameworkElement
        {
            return (TView)viewResolver.Resolve(new TViewModel());
        }
    }
}
