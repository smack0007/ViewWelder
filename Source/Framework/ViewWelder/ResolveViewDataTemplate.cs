using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewWelder.Converters;

namespace ViewWelder
{
    public class ResolveViewDataTemplate : DataTemplate
    {
        private readonly System.Windows.Data.Binding binding;

        public PropertyPath Path
        {
            get => this.binding.Path;
            set => this.binding.Path = value;
        }

        public BindingMode Mode
        {
            get => this.binding.Mode;
            set => this.binding.Mode = value;
        }

        public ResolveViewDataTemplate()
        {
            this.binding = new Binding() { Converter = new ViewResolverConverter() };

            var ccFactory = new FrameworkElementFactory(typeof(ContentControl));
            ccFactory.SetBinding(ContentControl.ContentProperty, binding);

            this.VisualTree = ccFactory;
        }
    }
}
