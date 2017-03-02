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

            var controls = view.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(x => typeof(Control).IsAssignableFrom(x.FieldType))
                .ToDictionary(x => x.Name, x => (Control)x.GetValue(view));

            foreach (var pair in controls)
            {
                var controlName = pair.Key;
                var control = pair.Value;
                var controlType = control.GetType();

                if (controlType == typeof(Button))
                {
                    BindButton((Button)control, viewModel, properties, methods);
                }
                else if (controlType == typeof(ComboBox))
                {
                    BindComboBox((ComboBox)control, viewModel, properties);
                }
                else if (controlType == typeof(TextBox))
                {
                    BindTextBox((TextBox)control, viewModel, properties);
                }
            }
        }

        private void BindButton(Button control, ViewModelBase viewModel, PropertyInfo[] properties, MethodInfo[] methods)
        {
            var clickMethod = methods.SingleOrDefault(x => x.Name == this.inflector.InflectClickMethodName(control.Name));

            if (clickMethod != null && clickMethod.GetParameters().Count() == 0)
            {
                control.Click += (s, e) => clickMethod.Invoke(viewModel, null);
            }

            var isEnabledProperty = properties.SingleOrDefault(x => x.Name == this.inflector.InflectIsEnabledPropertyName(control.Name));

            if (isEnabledProperty != null && isEnabledProperty.PropertyType == typeof(bool))
            {
                var binding = new Binding()
                {
                    Source = viewModel,
                    Path = new PropertyPath(isEnabledProperty.Name),
                    Mode = BindingMode.OneWay
                };

                BindingOperations.SetBinding(control, Button.IsEnabledProperty, binding);
            }
        }

        private void BindComboBox(ComboBox control, ViewModelBase viewModel, PropertyInfo[] properties)
        {
            BindItemsControl(control, viewModel, properties);

            var selectedItemProperty = properties.SingleOrDefault(x => x.Name == this.inflector.InflectSelectedItemPropertyName(control.Name));

            if (selectedItemProperty != null)
            {
                BindValueProperty(control, ComboBox.SelectedItemProperty, viewModel, selectedItemProperty);
            }
        }

        private void BindItemsControl(ItemsControl control, ViewModelBase viewModel, PropertyInfo[] properties)
        {
            var itemsSourceProperty = properties.SingleOrDefault(x => x.Name == this.inflector.InflectItemsSourcePropertyName(control.Name));

            if (itemsSourceProperty != null)
            {
                var binding = new Binding()
                {
                    Source = viewModel,
                    Path = new PropertyPath(itemsSourceProperty.Name),
                    Mode = BindingMode.OneWay
                };

                BindingOperations.SetBinding(control, ItemsControl.ItemsSourceProperty, binding);
            }
        }

        private void BindTextBox(TextBox control, ViewModelBase viewModel, PropertyInfo[] properties)
        {
            var textProperty = properties.SingleOrDefault(x => x.Name == this.inflector.InflectTextPropertyName(control.Name));

            if (textProperty != null && textProperty.PropertyType == typeof(string))
            {
                BindValueProperty(control, TextBox.TextProperty, viewModel, textProperty, UpdateSourceTrigger.PropertyChanged);
            }
        }

        private void BindValueProperty(
            Control control,
            DependencyProperty controlProperty,
            object viewModel,
            PropertyInfo viewModelProperty,
            UpdateSourceTrigger updateSourceTrigger = UpdateSourceTrigger.Default)
        {
            var binding = new Binding()
            {
                Source = viewModel,
                Path = new PropertyPath(viewModelProperty.Name),
                UpdateSourceTrigger = updateSourceTrigger
            };

            if (viewModelProperty.CanRead && viewModelProperty.CanWrite)
            {
                binding.Mode = BindingMode.TwoWay;
            }
            else if (viewModelProperty.CanRead && !viewModelProperty.CanWrite)
            {
                binding.Mode = BindingMode.OneWay;
            }

            BindingOperations.SetBinding(control, controlProperty, binding);
        }
    }
}
