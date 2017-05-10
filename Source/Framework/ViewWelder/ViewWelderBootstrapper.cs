using System;
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

        protected abstract ViewModel CreateRootViewModel();

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
            Application.Current.Resources["viewResolver"] = viewResolver;

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
