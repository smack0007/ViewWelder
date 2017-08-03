using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ViewWelder;

namespace HelloWorld.ViewModels
{
    public class ShellViewModel : ViewModel, IViewAware
    {
        private string title;
        private PersonViewModel selectedPerson;

        private FrameworkElement view;

        public string Title
        {
            get => this.title;

            set
            {
                if (value != this.title)
                {
                    this.title = value;
                    this.NotifyOfPropertyChange(nameof(this.Title));
                }
            }
        }

        public ObservableCollection<PersonViewModel> People { get; } = new ObservableCollection<PersonViewModel>()
        {
            new PersonViewModel() { FirstName = "Zachary", LastName = "Snow" },
            new PersonViewModel() { FirstName = "Daniela", LastName = "Snow" },
            new PersonViewModel() { FirstName = "Hannah", LastName = "Snow" },
            new PersonViewModel() { FirstName = "Henry", LastName = "Snow" }
        };

        public PersonViewModel SelectedPerson
        {
            get => this.selectedPerson;

            set
            {
                if (value != this.selectedPerson)
                {
                    if (this.selectedPerson != null)
                    {
                        this.selectedPerson.PropertyChanged -= this.SelectedPerson_PropertyChanged;
                    }

                    this.selectedPerson = value;

                    if (this.selectedPerson != null)
                    {
                        this.selectedPerson.PropertyChanged += this.SelectedPerson_PropertyChanged;
                    }

                    this.NotifyOfPropertyChange(nameof(this.SelectedPerson));
                    this.NotifyOfPropertyChange(nameof(this.GetStatus));
                }
            }
        }

        public ShellViewModel()
        {
            this.Title = "Hello World!";
            this.SelectedPerson = this.People.First();
        }

        public void SetView(FrameworkElement view)
        {
            this.view = view;
            this.view.SizeChanged += View_SizeChanged;
        }

        private void View_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Title = $"Hello World! {this.view.Width}x{this.view.Height}";
        }

        private void SelectedPerson_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(PersonViewModel.ToString))
            {
                this.NotifyOfPropertyChange(nameof(this.GetStatus));
            }
        }

        public string GetStatus()
        {
            return $"Editing {this.selectedPerson.ToString()}...";
        }
    }
}
