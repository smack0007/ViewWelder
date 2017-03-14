namespace ViewWelder.ViewModels
{
    public interface IHeaderedItemViewModel<out THeader>
        where THeader : ViewModelBase
    {
        THeader Header { get; }
    }
}
