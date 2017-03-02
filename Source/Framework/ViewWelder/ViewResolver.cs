using System;
using System.Reflection;
using System.Windows;

namespace ViewWelder
{
    public class ViewResolver
    {
        private Assembly viewAssembly;
        private ViewResolverInflector viewInflector;
        private IViewBinder viewBinder;

        public ViewResolver(
            Assembly viewAssembly = null,
            ViewResolverInflector viewInflector = null,
            IViewBinder viewBinder = null)
        {
            if (viewAssembly == null)
                viewAssembly = Assembly.GetEntryAssembly();

            if (viewInflector == null)
                viewInflector = new ViewResolverInflector();

            if (viewBinder == null)
                viewBinder = new ViewBinder();

            this.viewAssembly = viewAssembly;
            this.viewInflector = viewInflector;
            this.viewBinder = viewBinder;
        }

        public FrameworkElement Resolve(ViewModelBase viewModel)
        {
            var viewModelName = viewModel.GetType().FullName;

            var viewName = this.viewInflector.InflectViewName(viewModelName);

            var viewType = this.viewAssembly.GetType(viewName);

            if (viewType == null)
                throw new ViewResolverException($"Unable to find View \"{viewName}\" for ViewModel \"{viewModelName}\".");

            object view = Activator.CreateInstance(viewType);

            if (!(view is FrameworkElement))
                throw new ViewResolverException($"Resolved view \"{viewName}\" for ViewModel \"{viewModelName}\" is not an instance of FrameworkElement.");

            var frameworkElement = (FrameworkElement)view;

            this.viewBinder.Bind(viewModel, frameworkElement);

            return frameworkElement;
        }

        public TView Resolve<TView>(ViewModelBase viewModel)
            where TView : FrameworkElement
        {
            return (TView)this.Resolve(viewModel);
        }

        public object Resolve<TViewModel>()
            where TViewModel : ViewModelBase, new()
        {
            return this.Resolve(new TViewModel());
        }

        public TView Resolve<TViewModel, TView>()
            where TViewModel : ViewModelBase, new()
            where TView : FrameworkElement
        {
            return (TView)this.Resolve(new TViewModel());
        }
    }
}
