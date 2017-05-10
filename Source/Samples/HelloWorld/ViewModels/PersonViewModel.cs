using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class PersonViewModel : ViewModel
    {
        private string firstName;
        private string lastName;

        public string FirstName
        {
            get => this.firstName;

            set
            {
                if (value != this.firstName)
                {
                    this.firstName = value;
                    this.NotifyOfPropertyChange(nameof(this.FirstName));
                }
            }
        }

        public string LastName
        {
            get => this.lastName;

            set
            {
                if (value != this.lastName)
                {
                    this.lastName = value;
                    this.NotifyOfPropertyChange(nameof(this.LastName));
                }
            }
        }

        public override string ToString() => $"{this.LastName}, {this.FirstName}";
    }
}
