using HelloWorld.ViewModels;
using ViewWelder;

namespace HelloWorld
{
    public class HelloWorldBootstrapper : ViewWelderBootstrapper
    {
        protected override ViewModel CreateRootViewModel() => new ShellViewModel();
    }
}
