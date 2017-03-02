using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class HelloWorldViewModel : ViewModelBase
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
                    this.NotifyOfPropertyChange(nameof(this.CanSayHello));
                }
            }
        }

        private string username = "";

        public string Username
        {
            get { return this.username; }

            set
            {
                if (value != this.username)
                {
                    this.username = value;
                    this.NotifyOfPropertyChange(nameof(this.Username));
                    this.NotifyOfPropertyChange(nameof(this.CanSayHello));
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

        public HelloWorldViewModel()
        {
            this.UserType = this.UserTypeItems.First();
        }

        public bool CanSayHello => !string.IsNullOrEmpty(this.Name) &&
                                   !string.IsNullOrEmpty(this.Username);

        public void SayHello()
        {
            // TODO: Abstract this out.
            MessageBox.Show($"Hello {this.username} ({this.Name}).");
        }
    }
}
