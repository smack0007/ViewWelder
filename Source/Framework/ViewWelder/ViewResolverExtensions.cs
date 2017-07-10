using System.Windows;

namespace ViewWelder
{
    public static class ViewResolverExtensions
    {
        public static TView Resolve<TView>(this IViewResolver viewResolver, ViewModel viewModel)
            where TView : FrameworkElement
        {
            var view = viewResolver.Resolve(viewModel) as TView;

            if (view == null)
                throw new ViewResolverException($"The view resolved for ViewModel '{viewModel.GetType()}' was not an instance of '{typeof(TView)}'.");

            return view;
        }

        public static object Resolve<TViewModel>(this IViewResolver viewResolver)
            where TViewModel : ViewModel, new()
        {
            return viewResolver.Resolve(new TViewModel());
        }

        public static TView Resolve<TViewModel, TView>(this IViewResolver viewResolver)
            where TViewModel : ViewModel, new()
            where TView : FrameworkElement
        {
            var view = viewResolver.Resolve(new TViewModel()) as TView;

            if (view == null)
                throw new ViewResolverException($"The view resolved for ViewModel '{typeof(TViewModel)}' was not an instance of '{typeof(TView)}'.");

            return view;
        }
    }
}
