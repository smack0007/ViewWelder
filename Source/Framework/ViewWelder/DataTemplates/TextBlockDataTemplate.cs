using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewWelder.ViewModels;

namespace ViewWelder.DataTemplates
{
    public class TextBlockDataTemplate : DataTemplate
    {
        public TextBlockDataTemplate(string bindingPath)
            : base(typeof(ViewModelBase))
        {
            var binding = new Binding()
            {
                Path = new PropertyPath(bindingPath),
                Mode = BindingMode.OneWay,
            };

            var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetBinding(TextBlock.TextProperty, binding);

            this.VisualTree = textBlockFactory;
        }
    }
}
