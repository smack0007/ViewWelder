﻿using System;
using System.Windows;

namespace ViewWelder
{
    public abstract class ViewWelderBootstrapper
    {
        protected ViewWelderBootstrapper()
        {
            Application.Current.Startup += this.Application_Startup;
            Application.Current.Exit += this.Application_Exit;
        }

        protected virtual ViewResolver CreateViewResolver() => new ViewResolver();

        protected abstract ViewModel CreateRootViewModel();

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.OnStartup(e);

            var rootViewModel = this.CreateRootViewModel();

            Window window = null;

            var viewResolver = this.CreateViewResolver();
            Application.Current.Resources[ViewWelderResourceKeys.ViewResolver] = viewResolver;

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

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            this.OnExit(e);
        }

        protected virtual void OnStartup(StartupEventArgs e)
        {
        }

        protected virtual void OnExit(ExitEventArgs e)
        {
        }
    }
}
