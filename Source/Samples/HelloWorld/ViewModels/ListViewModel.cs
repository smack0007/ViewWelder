using ViewWelder.ViewModels;

namespace HelloWorld.ViewModels
{
    public class ListViewModel : HeaderedItemViewModel<ListHeaderViewModel>
    {
        public ListViewModel()
        {
            this.Header = new ListHeaderViewModel();
        }
    }
}
