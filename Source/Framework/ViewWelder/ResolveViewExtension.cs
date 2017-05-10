using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using ViewWelder.Converters;

namespace ViewWelder
{
    public class ResolveViewExtension : MarkupExtension
    {
        private readonly Binding binding;

        public PropertyPath Path
        {
            get => this.binding.Path;
            set => this.binding.Path = value;
        }

        public ResolveViewExtension(PropertyPath path)
        {
            this.binding = new Binding()
            {
                Converter = new ViewResolverConverter(),
                Path = path ?? throw new ArgumentNullException(nameof(path))
            };
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this.binding.ProvideValue(serviceProvider);
        }
    }
}
