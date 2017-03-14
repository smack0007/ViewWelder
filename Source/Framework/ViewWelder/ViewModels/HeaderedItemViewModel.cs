using System;
using System.Reflection;

namespace ViewWelder.ViewModels
{
    public abstract class HeaderedItemViewModel : ViewModelBase
    {
    }

    public abstract class HeaderedItemViewModel<THeader> : HeaderedItemViewModel
        where THeader: class
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

        public override string ToString()
        {
            return this.header.ToString();
        }
    }
}
