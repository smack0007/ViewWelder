using ViewWelder.ViewModels;

namespace HelloWorld.ViewModels
{
    public class ListViewModel : HeaderedItemViewModel<string>
    {
        public ListViewModel()
        {
            this.Header = "List";
        }
    }
}
