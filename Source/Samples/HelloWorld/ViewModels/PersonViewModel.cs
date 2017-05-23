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
                    this.NotifyOfPropertyChange(nameof(this.CanSayHello));
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
                    this.NotifyOfPropertyChange(nameof(this.CanSayHello));
                }
            }
        }

        public bool CanSayHello => !string.IsNullOrEmpty(this.FirstName) && !string.IsNullOrEmpty(this.LastName);

        public override string ToString() => $"{this.LastName}, {this.FirstName}";

        public void SayHello()
        {
            this.DialogPresenter.ShowInformationMessage($"Hello {this.ToString()}!", "Hello");
        }
    }
}
