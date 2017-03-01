using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ViewWelder
{
    public class ViewBinder
    {
        public void Bind(object viewModel, object view)
        {
            if (!(view is FrameworkElement))
            {
                throw new ViewBinderException("View must be an instance of DependencyObject.");
            }

            var viewFrameworkElement = (FrameworkElement)view;
            viewFrameworkElement.DataContext = viewModel;

            var properties = viewModel.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var controlFields = view.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Where(x => typeof(Control).IsAssignableFrom(x.FieldType));

            foreach (var property in properties)
            {
                var controlField = controlFields.SingleOrDefault(x => x.Name == property.Name);

                if (controlField == null)
                    continue;

                var control = (Control)controlField.GetValue(view);

                BindControlValue(control, viewModel, property);

                if (control is ItemsControl)
                {
                    var itemsProperty = properties.SingleOrDefault(x => x.Name == property.Name + "Items");

                    if (itemsProperty != null)
                    {
                        BindControlItemsSource((ItemsControl)control, viewModel, itemsProperty);
                    }
                }
            }
        }

        private void BindControlValue(Control control, object viewModel, PropertyInfo property)
        {
            var binding = new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath(property.Name),
            };

            if (property.CanRead && property.CanWrite)
            {
                binding.Mode = BindingMode.TwoWay;
            }
            else if (property.CanRead && !property.CanWrite)
            {
                binding.Mode = BindingMode.OneWay;
            }

            if (control is ComboBox)
            {
                BindingOperations.SetBinding(control, ComboBox.SelectedItemProperty, binding);
            }
            else if (control is TextBox)
            {
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                BindingOperations.SetBinding(control, TextBox.TextProperty, binding);
            }
        }

        private void BindControlItemsSource(ItemsControl control, object viewModel, PropertyInfo itemsProperty)
        {
            var binding = new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath(itemsProperty.Name),
                Mode = BindingMode.OneWay
            };

            BindingOperations.SetBinding(control, ItemsControl.ItemsSourceProperty, binding);
        }
    }
}
