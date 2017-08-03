using System.Collections.Generic;
using System.ComponentModel;

namespace ViewWelder
{
    public abstract class ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly Dictionary<string, string> propertyErrors = new Dictionary<string, string>();

        internal ViewContext ViewContext { get; set; }

        public IDialogPresenter DialogPresenter => this.ViewContext;

        string IDataErrorInfo.Error => null;

        string IDataErrorInfo.this[string columnName] => this.GetPropertyError(columnName);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyOfPropertyChange(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ClearPropertyErrors()
        {
            this.propertyErrors.Clear();
        }

        protected string GetPropertyError(string propertyName)
        {
            if (this.propertyErrors.TryGetValue(propertyName, out var error))
            {
                return error;
            }

            return null;
        }

        protected void RemovePropertyError(string propertyName)
        {
            this.propertyErrors.Remove(propertyName);
        }

        protected void SetPropertyError(string propertyName, string error)
        {
            this.propertyErrors[propertyName] = error;
        }
    }
}
