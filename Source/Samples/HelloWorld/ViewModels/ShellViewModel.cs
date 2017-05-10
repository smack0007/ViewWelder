using System.Collections.ObjectModel;
using System.Linq;
using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class ShellViewModel : ViewModel
    {
        private PersonViewModel selectedPerson;

        public ObservableCollection<PersonViewModel> People { get; } = new ObservableCollection<PersonViewModel>()
        {
            new PersonViewModel() { FirstName = "Zachary", LastName = "Snow" },
            new PersonViewModel() { FirstName = "Daniela", LastName = "Snow" },
            new PersonViewModel() { FirstName = "Hannah", LastName = "Snow" }
        };

        public PersonViewModel SelectedPerson
        {
            get => this.selectedPerson;

            set
            {
                if (value != this.selectedPerson)
                {
                    this.selectedPerson = value;
                    this.NotifyOfPropertyChange(nameof(this.SelectedPerson));
                }
            }
        }

        public ShellViewModel()
        {
            this.SelectedPerson = this.People.First();
        }
    }
}
