using System;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace ViewWelder
{
    public class EventBindingExtension : MarkupExtension
    {
        [ConstructorArgument("handlerMethodName")]
        public string HandlerMethodName { get; set; }

        public EventBindingExtension()
        {
        }

        public EventBindingExtension(string handlerMethodName)
        {
            this.HandlerMethodName = handlerMethodName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(HandlerMethodName))
                throw new ViewWelderException($"The {nameof(this.HandlerMethodName)} property is not set.");

            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            var view = target.TargetObject as FrameworkElement;

            if (view == null)
                throw new ViewWelderException($"The target object must be an instance of {nameof(FrameworkElement)}.");

            var @event = target.TargetProperty as EventInfo;

            if (@event == null)
                throw new ViewWelderException($"The target property must be an instance of {nameof(EventInfo)}.");

            var context = new EventBindingContext(view, this.HandlerMethodName);
            return (RoutedEventHandler)context.View_Event;
        }
    }
}