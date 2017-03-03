using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class HelloWorldViewModel : ViewModelBase
    {
        public GreeterViewModel Greeter { get; } = new GreeterViewModel();
    }
}
