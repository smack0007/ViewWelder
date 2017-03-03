using System;
using System.Globalization;
using System.Windows.Data;

namespace ViewWelder.Converters
{
    public class ViewModelToViewConverter : IValueConverter
    {
        private readonly IViewResolver viewResolver;

        public ViewModelToViewConverter(IViewResolver viewResolver)
        {
            if (viewResolver == null)
                throw new ArgumentNullException(nameof(viewResolver));

            this.viewResolver = viewResolver;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ViewModelBase))
                return null;

            return this.viewResolver.Resolve((ViewModelBase)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
