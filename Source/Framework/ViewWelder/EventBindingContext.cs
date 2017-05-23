using System;
using System.Reflection;
using System.Windows;

namespace ViewWelder
{
    public class EventBindingContext
    {
        private readonly FrameworkElement view;
        private readonly EventInfo @event;
        private readonly string handlerMethodName;

        internal EventBindingContext(FrameworkElement view, EventInfo @event, string handlerMethodName)
        {
            this.view = view;
            this.@event = @event;
            this.handlerMethodName = handlerMethodName;
        }

        internal void View_Event(object sender, EventArgs e)
        {
            if (this.view.DataContext == null)
                return;

            var method = this.view.DataContext.GetType().GetMethod(handlerMethodName);

            method?.Invoke(this.view.DataContext, new object[] { });
        }
    }
}
