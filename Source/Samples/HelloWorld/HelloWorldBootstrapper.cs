using HelloWorld.ViewModels;
using ViewWelder;
using ViewWelder.ViewModels;

namespace HelloWorld
{
    public class HelloWorldBootstrapper : ViewWelderBootstrapper
    {
        protected override ViewModelBase CreateRootViewModel() => new HelloWorldViewModel();
    }
}
