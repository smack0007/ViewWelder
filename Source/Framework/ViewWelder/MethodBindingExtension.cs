using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ViewWelder
{
    public class MethodBindingExtension : MarkupExtension
    {
        [ConstructorArgument("methodName")]
        public string MethodName { get; set; }

        public MethodBindingExtension()
        {
        }

        public MethodBindingExtension(string methodName)
        {
            this.MethodName = methodName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(this.MethodName))
                throw new ViewWelderException($"The {nameof(this.MethodName)} property is not set.");

            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            var view = target.TargetObject as FrameworkElement;

            if (view == null)
                throw new ViewWelderException($"The target object must be an instance of {nameof(FrameworkElement)}.");

            var property = target.TargetProperty as DependencyProperty;

            if (property == null)
                throw new ViewWelderException($"The target property must be an instance of {nameof(DependencyProperty)}.");

            var context = new MethodBindingContext(view, this.MethodName);

            var binding = new Binding()
            {
                Source = context,
                Path = new PropertyPath(nameof(context.Value)),
                Mode = BindingMode.OneWay
            };

            return binding.ProvideValue(serviceProvider);
        }
    }
}
