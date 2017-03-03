using System.ComponentModel;
using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class HelloWorldViewModel : ViewModelBase
    {
        public string Title => $"Hello {this.Greeter.Name}";

        public GreeterViewModel Greeter { get; } = new GreeterViewModel();

        public HelloWorldViewModel()
        {
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
