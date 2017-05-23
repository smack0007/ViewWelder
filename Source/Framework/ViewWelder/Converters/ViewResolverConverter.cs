using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ViewWelder.Converters
{
    public class ViewResolverConverter : IValueConverter
    {
        private IViewResolver viewResolver;
                
        private void EnsureViewResolver()
        {
            if (this.viewResolver == null)
            {
                this.viewResolver = (IViewResolver)Application.Current.Resources[ViewWelderResourceKeys.ViewResolver];
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ViewModel))
                return null;

            this.EnsureViewResolver();

            return this.viewResolver.Resolve((ViewModel)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
