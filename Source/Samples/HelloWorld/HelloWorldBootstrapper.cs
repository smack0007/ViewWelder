using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWorld.ViewModels;
using ViewWelder;

namespace HelloWorld
{
    public class HelloWorldBootstrapper : ViewWelderBootstrapper
    {
        protected override ViewModelBase CreateRootViewModel() => new HelloWorldViewModel();
    }
}
