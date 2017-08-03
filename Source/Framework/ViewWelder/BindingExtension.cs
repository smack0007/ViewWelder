using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ViewWelder
{
    public class BindingExtension : MarkupExtension
    {
        [DefaultValue(BindingMode.Default)]
        public BindingMode Mode { get; set; } = BindingMode.Default;

        public PropertyPath Path { get; set; }
        
        public BindingExtension()
        {
        }

        public BindingExtension(string path)
        {
            this.Path = new PropertyPath(path);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (target.TargetObjectIsSharedDependencyProperty())
                return this;

            var view = target.TargetObject as FrameworkElement;

            if (view == null)
                throw new ViewWelderException($"The target object must be an instance of {nameof(FrameworkElement)}.");

            var property = target.TargetProperty as DependencyProperty;

            if (property == null)
                throw new ViewWelderException($"The target property must be an instance of {nameof(DependencyProperty)}.");

            var metadata = property.GetMetadata(view.GetType());

            var mode = this.Mode;

            //if (mode == BindingMode.Default)
            //{
            //    if (metadata.BindsTwoWayByDefault)
            //    {
            //        mode = BindingMode.TwoWay;
            //    }
            //    else
            //    {
            //        mode = BindingMode.OneWay;
            //    }
            //}

            var binding = new Binding()
            {
                Path = this.Path,
                Mode = mode
            };

            return binding.ProvideValue(serviceProvider);
        }
    }
}
