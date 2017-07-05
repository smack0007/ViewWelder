using System;
using System.Reflection;
using System.Windows;

namespace ViewWelder
{
    public class EventBindingContext
    {
        private readonly FrameworkElement view;
        private readonly string handlerMethodName;

        internal EventBindingContext(FrameworkElement view, string handlerMethodName)
        {
            this.view = view;
            this.handlerMethodName = handlerMethodName;
        }

        internal void View_Event(object sender, EventArgs e)
        {
            if (this.view.DataContext == null)
                return;

            var method = this.view.DataContext.GetType().GetMethod(this.handlerMethodName);

            method?.Invoke(this.view.DataContext, new object[] { });
        }
    }
}
