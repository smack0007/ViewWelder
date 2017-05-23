using System.ComponentModel;

namespace ViewWelder
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        internal ViewContext ViewContext { get; set; }

        public IDialogPresenter DialogPresenter => this.ViewContext;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyOfPropertyChange(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
