using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ViewWelder.ViewModels;

namespace ViewWelder.Converters
{
    public class ViewResolverConverter : IValueConverter
    {
        private IViewResolver viewResolver;

        public ViewResolverConverter()
        {
        }

        public ViewResolverConverter(IViewResolver viewResolver)
        {
            this.viewResolver = viewResolver ?? throw new ArgumentNullException(nameof(viewResolver));
        }

        private void EnsureViewResolver()
        {
            if (this.viewResolver == null)
            {
                this.viewResolver = (IViewResolver)Application.Current.Resources["viewResolver"];
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ViewModelBase))
                return null;

            this.EnsureViewResolver();

            return this.viewResolver.Resolve((ViewModelBase)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
