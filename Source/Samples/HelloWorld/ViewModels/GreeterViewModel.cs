using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ViewWelder.ViewModels;

namespace HelloWorld.ViewModels
{
    public class GreeterViewModel : HeaderedItemViewModel<string>
    {
        public GreeterViewModel()
        {
            this.Header = "Greeter";
            this.UserType = this.UserTypeItemsSource.First();
        }

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
                    this.NotifyOfPropertyChange(nameof(this.SayHelloIsEnabled));
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
                    this.NotifyOfPropertyChange(nameof(this.SayHelloIsEnabled));
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

        public IEnumerable<string> UserTypeItemsSource { get; } = new string[] { "Administrator", "Power User", "User" };


        public bool SayHelloIsEnabled => !string.IsNullOrEmpty(this.Name) &&
                                         !string.IsNullOrEmpty(this.Username);

        public void SayHello()
        {
            // TODO: Abstract this out.
            MessageBox.Show($"Hello {this.username} ({this.Name}).");
        }
    }
}
