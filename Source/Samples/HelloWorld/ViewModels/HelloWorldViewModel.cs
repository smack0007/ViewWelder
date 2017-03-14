using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewWelder.ViewModels;

namespace HelloWorld.ViewModels
{
    public class HelloWorldViewModel : ViewModelBase
    {
        public string Title => $"Hello {this.Greeter.Name}";

        public ObservableCollection<IHeaderedItemViewModel<ViewModelBase>> TabsItemsSource { get; } = new ObservableCollection<IHeaderedItemViewModel<ViewModelBase>>();

        public GreeterViewModel Greeter { get; } = new GreeterViewModel();

        public ListViewModel List { get; } = new ListViewModel();

        public HelloWorldViewModel()
        {
            this.TabsItemsSource.Add(this.Greeter);
            this.TabsItemsSource.Add(this.List);

            this.Greeter.PropertyChanged += this.Greeter_PropertyChanged;
        }

        private void Greeter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.Greeter.Name))
            {
                this.NotifyOfPropertyChange(nameof(this.Title));
            }
        }
    }
}
