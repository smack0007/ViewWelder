using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace ViewWelder
{
    public class ViewResolver : IViewResolver
    {
        private Assembly assembly;
        private IViewResolverInflector inflector;

        public ViewResolver(
            Assembly assembly = null,
            IViewResolverInflector inflector = null)
        {
            if (assembly == null)
                assembly = Assembly.GetEntryAssembly();

            if (inflector == null)
                inflector = new ViewResolverInflector();

            this.assembly = assembly;
            this.inflector = inflector;
        }

        public FrameworkElement Resolve(ViewModel viewModel)
        {
            var viewModelName = viewModel.GetType().FullName;

            var viewName = this.inflector.InflectViewName(viewModelName);

            var viewType = this.assembly.GetType(viewName);

            if (viewType == null)
                throw new ViewResolverException($"Unable to find View \"{viewName}\" for ViewModel \"{viewModelName}\".");

            object view = Activator.CreateInstance(viewType);

            if (!(view is FrameworkElement))
                throw new ViewResolverException($"Resolved view \"{viewName}\" for ViewModel \"{viewModelName}\" is not an instance of {nameof(FrameworkElement)}.");

            var initializeComponentMethod = view.GetType().GetMethods().SingleOrDefault(x => x.Name == "InitializeComponent" && !x.GetParameters().Any());

            if (initializeComponentMethod != null)
                initializeComponentMethod.Invoke(view, null);

            var frameworkElement = (FrameworkElement)view;

            frameworkElement.DataContext = viewModel;

            return frameworkElement;
        }
    }
}
