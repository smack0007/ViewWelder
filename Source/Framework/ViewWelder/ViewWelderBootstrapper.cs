using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewWelder
{
    public abstract class ViewWelderBootstrapper
    {
        protected ViewWelderBootstrapper()
        {
            this.SubscribeApplicationEvents();
        }

        protected virtual ViewResolver CreateViewResolver()
        {
            return new ViewResolver();
        }

        protected abstract ViewModelBase CreateRootViewModel();

        private void SubscribeApplicationEvents()
        {
            Application.Current.Startup += this.Application_Startup;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.OnStartup(e);

            var rootViewModel = this.CreateRootViewModel();

            Window window = null;

            var viewResolver = new ViewResolver();

            try
            {
                window = viewResolver.Resolve<Window>(rootViewModel);
            }
            catch (InvalidCastException)
            {
                throw new ViewWelderException("The View resolved from the root ViewModel must be a Window.");
            }

            window.Show();

            Application.Current.MainWindow = window;
        }

        protected virtual void OnStartup(StartupEventArgs e)
        {
        }
    }
}
