using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ViewWelder
{
    public class ViewBinder : IViewBinder
    {
        private readonly ViewBinderInflector inflector;

        public ViewBinder(ViewBinderInflector inflector = null)
        {
            if (inflector == null)
                inflector = new ViewBinderInflector();

            this.inflector = inflector;
        }

        public void Bind(ViewModelBase viewModel, FrameworkElement view)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            view.DataContext = viewModel;

            var properties = viewModel.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var methods = viewModel.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public);

            var controlFields = view.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(x => typeof(Control).IsAssignableFrom(x.FieldType));

            foreach (var property in properties)
            {
                var controlField = controlFields.SingleOrDefault(x => x.Name == property.Name);

                if (controlField == null)
                    continue;

                var control = (Control)controlField.GetValue(view);

                BindControlValue(control, viewModel, property);

                if (control is ItemsControl)
                {
                    var itemsSourceProperty = properties.SingleOrDefault(x => x.Name == this.inflector.InflectItemsSourceName(control.Name));

                    if (itemsSourceProperty != null)
                    {
                        BindItemsSource((ItemsControl)control, viewModel, itemsSourceProperty);
                    }
                }
            }

            foreach (var method in methods)
            {
                var controlField = controlFields.SingleOrDefault(x => x.Name == method.Name);

                if (controlField == null)
                    continue;

                var control = (Control)controlField.GetValue(view);

                var isEnabledProperty = properties.SingleOrDefault(x => x.Name == this.inflector.InflectIsEnabledName(control.Name));

                BindControlAction(control, viewModel, method, isEnabledProperty);                
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

        private void BindItemsSource(ItemsControl control, object viewModel, PropertyInfo itemsProperty)
        {
            var binding = new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath(itemsProperty.Name),
                Mode = BindingMode.OneWay
            };

            BindingOperations.SetBinding(control, ItemsControl.ItemsSourceProperty, binding);
        }

        private void BindControlAction(Control control, object viewModel, MethodInfo method, PropertyInfo toggleProperty)
        {
            if (control is Button)
            {
                var button = (Button)control;
                button.Click += (s, e) => method.Invoke(viewModel, null);

                if (toggleProperty != null && toggleProperty.PropertyType == typeof(bool))
                {
                    var binding = new Binding()
                    {
                        Source = viewModel,
                        Path = new PropertyPath(toggleProperty.Name),
                        Mode = BindingMode.OneWay
                    };

                    BindingOperations.SetBinding(control, Button.IsEnabledProperty, binding);
                }
            }
        }
    }
}
