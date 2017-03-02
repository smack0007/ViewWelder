using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewWelder
{
    public class ViewBinderInflector
    {
        public virtual string InflectItemsSourceName(string name)
        {
            return name + "ItemsSource";
        }

        public virtual string InflectIsEnabledName(string name)
        {
            return name + "IsEnabled";
        }
    }
}
