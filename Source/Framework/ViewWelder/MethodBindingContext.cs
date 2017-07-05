using System.ComponentModel;
using System.Windows;

namespace ViewWelder
{
    public class MethodBindingContext : INotifyPropertyChanged
    {
        private readonly FrameworkElement view;
        private readonly string methodName;

        private object dataContext;

        private readonly PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Value));
        public event PropertyChangedEventHandler PropertyChanged;

        public object Value
        {
            get
            {
                if (this.dataContext == null)
                    return null;

                var method = this.dataContext.GetType().GetMethod(this.methodName);

                if (method == null)
                    return null;

                return method.Invoke(this.dataContext, new object[] { });
            }
        }

        internal MethodBindingContext(FrameworkElement view, string methodName)
        {
            this.view = view;
            this.methodName = methodName;

            this.dataContext = this.view.DataContext;

            if (this.dataContext != null && this.dataContext is INotifyPropertyChanged)
                ((INotifyPropertyChanged)this.dataContext).PropertyChanged += this.DataContext_PropertyChanged;

            this.view.DataContextChanged += this.View_DataContextChanged;
        }

        private void View_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.dataContext != null && this.dataContext is INotifyPropertyChanged)
                ((INotifyPropertyChanged)this.dataContext).PropertyChanged -= this.DataContext_PropertyChanged;

            this.dataContext = this.view.DataContext;

            if (this.dataContext != null && this.dataContext is INotifyPropertyChanged)
                ((INotifyPropertyChanged)this.dataContext).PropertyChanged += this.DataContext_PropertyChanged;

            this.PropertyChanged?.Invoke(this, this.propertyChangedEventArgs);
        }

        private void DataContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.methodName)
                this.PropertyChanged?.Invoke(this, this.propertyChangedEventArgs);
        }
    }
}
