using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewWelder
{
    public class ViewResolver
    {
        private Assembly viewAssembly;
        private Func<string, string> inflectViewName;
        private ViewBinder viewBinder;

        public ViewResolver(
            Assembly viewAssembly = null,
            Func<string, string> inflectViewName = null,
            ViewBinder viewBinder = null)
        {
            if (viewAssembly == null)
                viewAssembly = Assembly.GetEntryAssembly();

            if (inflectViewName == null)
                inflectViewName = DefaultInflectViewName;

            if (viewBinder == null)
                viewBinder = new ViewBinder();

            this.viewAssembly = viewAssembly;
            this.inflectViewName = inflectViewName;
            this.viewBinder = viewBinder;
        }

        private static string DefaultInflectViewName(string viewModelName)
        {
            return viewModelName.Replace("ViewModel", "View");
        }

        public object Resolve(ViewModelBase viewModel)
        {
            var viewModelName = viewModel.GetType().FullName;

            var viewName = this.inflectViewName(viewModelName);

            var viewType = this.viewAssembly.GetType(viewName);

            if (viewType == null)
                throw new ViewResolverException($"Unable to find View \"{viewName}\" for ViewModel \"{viewModelName}\".");

            object view = Activator.CreateInstance(viewType);

            this.viewBinder.Bind(viewModel, view);

            return view;
        }

        public TView Resolve<TView>(ViewModelBase viewModel)
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
        {
            return (TView)this.Resolve(new TViewModel());
        }
    }
}
