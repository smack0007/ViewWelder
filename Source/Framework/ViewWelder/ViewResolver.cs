using System;
using System.Reflection;
using System.Windows;

namespace ViewWelder
{
    public class ViewResolver : IViewResolver
    {
        private Assembly viewAssembly;
        private ViewResolverInflector viewInflector;
        private IViewBinder viewBinder;

        public ViewResolver(
            Assembly assembly = null,
            ViewResolverInflector inflector = null,
            IViewBinder binder = null)
        {
            if (assembly == null)
                assembly = Assembly.GetEntryAssembly();

            if (inflector == null)
                inflector = new ViewResolverInflector();

            if (binder == null)
                binder = new ViewBinder();

            this.viewAssembly = assembly;
            this.viewInflector = inflector;
            this.viewBinder = binder;
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

            this.viewBinder.Bind(viewModel, frameworkElement, this);

            return frameworkElement;
        }
    }
}
