using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewWelder.Converters;
using ViewWelder.ViewModels;

namespace ViewWelder.DataTemplates
{
    public class ViewResolverDataTemplate : DataTemplate
    {
        public ViewResolverDataTemplate()
        {
            var binding = new Binding()
            {
                Mode = BindingMode.OneWay,
                Converter = new ViewResolverConverter()
            };

            var ccFactory = new FrameworkElementFactory(typeof(ContentControl));
            ccFactory.SetBinding(ContentControl.ContentProperty, binding);

            this.VisualTree = ccFactory;
        }
    }
}
