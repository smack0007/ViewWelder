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

            var frameworkElement = (FrameworkElement)view;

            InitializeView(frameworkElement, frameworkElement);

            frameworkElement.DataContext = viewModel;

            viewModel.ViewContext = new ViewContext(frameworkElement);

            if (viewModel is IViewAware)
                ((IViewAware)viewModel).SetView(frameworkElement);

            return frameworkElement;
        }

        private static void InitializeView(FrameworkElement current, FrameworkElement root)
        {
            var initializeComponentMethod = current.GetType().GetMethods().SingleOrDefault(x =>
                x.Name == "InitializeComponent" &&
                x.ReturnType == typeof(void) &&
                !x.GetParameters().Any());

            if (initializeComponentMethod != null)
            {
                try
                {
                    initializeComponentMethod.Invoke(current, null);
                }
                catch (Exception ex)
                {
                    throw new ViewResolverException($"Failed while calling InitializeComponent on type '{current.GetType()}' while initializing view '{root.GetType()}'.", ex);
                }
            }

            foreach (var child in current.GetLogicalChildren().OfType<FrameworkElement>())
            {
                InitializeView(child, root);
            }
        }
    }
}
