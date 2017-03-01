using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private string name = "Bob Freeman";

        public string Name
        {
            get { return this.name; }

            set
            {
                if (value != this.name)
                {
                    this.name = value;
                    this.NotifyOfPropertyChange(nameof(this.Name));
                }
            }
        }

        private string userType;

        public string UserType
        {
            get { return this.userType; }

            set
            {
                if (value != this.userType)
                {
                    this.userType = value;
                    this.NotifyOfPropertyChange(nameof(this.UserType));
                }
            }
        }

        public IEnumerable<string> UserTypeItems { get; } = new string[] { "Administrator", "Power User", "User" };

        public ShellViewModel()
        {
            this.UserType = this.UserTypeItems.First();
        }
    }
}
