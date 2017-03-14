using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewWelder.Converters;
using ViewWelder.ViewModels;

namespace ViewWelder.DataTemplates
{
    public class ViewResolverDataTemplate : DataTemplate
    {
        public ViewResolverDataTemplate(string bindingPath = null)
        {
            var binding = new Binding()
            {
                Mode = BindingMode.OneWay,
                Converter = new ViewResolverConverter()
            };

            if (bindingPath != null)
            {
                binding.Path = new PropertyPath(bindingPath);
            }

            var ccFactory = new FrameworkElementFactory(typeof(ContentControl));
            ccFactory.SetBinding(ContentControl.ContentProperty, binding);

            this.VisualTree = ccFactory;
        }
    }
}
