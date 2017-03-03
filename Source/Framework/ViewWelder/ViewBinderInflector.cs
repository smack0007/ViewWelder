using System;

namespace ViewWelder
{
    public class ViewBinderInflector
    {
        public virtual string InflectClickMethodName(string name)
        {
            return name;
        }

        public virtual string InflectContentPropertyName(string name)
        {
            return name;
        }

        public virtual string InflectItemsSourcePropertyName(string name)
        {
            return name + "ItemsSource";
        }

        public virtual string InflectIsEnabledPropertyName(string name)
        {
            return name + "IsEnabled";
        }

        public virtual string InflectTextPropertyName(string name)
        {
            return name;
        }
    }
}
