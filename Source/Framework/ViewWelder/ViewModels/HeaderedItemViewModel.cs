namespace ViewWelder.ViewModels
{
    public abstract class HeaderedItemViewModel<THeader> : ViewModelBase, IHeaderedItemViewModel<THeader>
        where THeader : ViewModelBase
    {
        private THeader header;

        public THeader Header
        {
            get { return this.header; }

            set
            {
                if (value != this.header)
                {
                    this.header = value;
                    this.OnHeaderChanged();
                }
            }
        }

        protected virtual void OnHeaderChanged()
        {
            this.NotifyOfPropertyChange(nameof(Header));
        }
    }
}
