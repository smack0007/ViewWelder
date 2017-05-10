using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class ShellViewModel : ViewModel
    {
        public PersonViewModel Person { get; } = new PersonViewModel() { FirstName = "Hannah", LastName = "Snow" };
    }
}
