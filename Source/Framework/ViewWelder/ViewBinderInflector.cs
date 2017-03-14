using System;

namespace ViewWelder
{
    public class ViewBinderInflector
    {
        public virtual string InflectClickMethodName(Type controlType, string controlName)
        {
            return controlName;
        }

        public virtual string InflectContentPropertyName(Type controlType, string controlName)
        {
            return controlName;
        }

        public virtual string InflectHeaderPropertyName(Type controlType, string controlName)
        {
            return controlName + "Header";
        }

        public virtual string InflectItemsSourcePropertyName(Type controlType, string controlName)
        {
            return controlName + "ItemsSource";
        }

        public virtual string InflectIsEnabledPropertyName(Type controlType, string controlName)
        {
            return controlName + "IsEnabled";
        }

        public virtual string InflectTextPropertyName(Type controlType, string controlName)
        {
            return controlName;
        }

        public virtual string InflectTitlePropertyName(Type controlType, string controlName)
        {
            return controlName + "Title";
        }
    }
}
