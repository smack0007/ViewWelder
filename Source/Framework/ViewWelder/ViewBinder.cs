﻿using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ViewWelder.Converters;

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

        public void Bind(ViewModelBase viewModel, FrameworkElement view, IViewResolver viewResolver)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (viewResolver == null)
                throw new ArgumentNullException(nameof(viewResolver));

            view.DataContext = viewModel;

            var properties = viewModel.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var methods = viewModel.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Public);

            if (view is Window)
            {
                BindWindow((Window)view, viewModel, properties);
            }

            var controls = view.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(x => typeof(Control).IsAssignableFrom(x.FieldType))
                .Select(x => (Control)x.GetValue(view));

            foreach (var control in controls)
            {
                if (control is Button)
                {
                    BindButton((Button)control, viewModel, properties, methods);
                }
                else if (control is ComboBox)
                {
                    BindComboBox((ComboBox)control, viewModel, properties);
                }
                else if (control is ContentControl)
                {
                    BindContentControl((ContentControl)control, viewModel, properties, viewResolver);
                }
                else if (control is TextBox)
                {
                    BindTextBox((TextBox)control, viewModel, properties);
                }
            }
        }

        private void BindWindow(Window control, ViewModelBase viewModel, PropertyInfo[] properties)
        {
            var titlePropertyName = this.inflector.InflectWindowTitlePropertyName();
            var titleProperty = properties.SingleOrDefault(x => x.Name == titlePropertyName);

            if (titleProperty != null)
            {
                var binding = new Binding()
                {
                    Source = viewModel,
                    Path = new PropertyPath(titleProperty.Name),
                    Mode = BindingMode.OneWay
                };

                BindingOperations.SetBinding(control, Window.TitleProperty, binding);
            }
        }

        private void BindButton(Button control, ViewModelBase viewModel, PropertyInfo[] properties, MethodInfo[] methods)
        {
            var clickMethodName = this.inflector.InflectClickMethodName(control.Name);
            var clickMethod = methods.SingleOrDefault(x => x.Name == clickMethodName);

            if (clickMethod != null && clickMethod.GetParameters().Count() == 0)
            {
                control.Click += (s, e) => clickMethod.Invoke(viewModel, null);
            }

            var isEnabledPropertyName = this.inflector.InflectIsEnabledPropertyName(control.Name);
            var isEnabledProperty = properties.SingleOrDefault(x => x.Name == isEnabledPropertyName);

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

            var selectedItemPropertyName = this.inflector.InflectContentPropertyName(control.Name);
            var selectedItemProperty = properties.SingleOrDefault(x => x.Name == selectedItemPropertyName);

            if (selectedItemProperty != null)
            {
                BindValueProperty(control, ComboBox.SelectedItemProperty, viewModel, selectedItemProperty);
            }
        }

        private void BindContentControl(ContentControl control, ViewModelBase viewModel, PropertyInfo[] properties, IViewResolver viewResolver)
        {
            var contentPropertyName = this.inflector.InflectContentPropertyName(control.Name);
            var contentProperty = properties.SingleOrDefault(x => x.Name == contentPropertyName);

            if (contentProperty != null && typeof(ViewModelBase).IsAssignableFrom(contentProperty.PropertyType))
            {
                var binding = new Binding()
                {
                    Source = viewModel,
                    Path = new PropertyPath(contentProperty.Name),
                    Mode = BindingMode.OneWay,
                    Converter = new ViewModelToViewConverter(viewResolver)
                };

                BindingOperations.SetBinding(control, ContentControl.ContentProperty, binding);
            }
        }

        private void BindItemsControl(ItemsControl control, ViewModelBase viewModel, PropertyInfo[] properties)
        {
            var itemsSourcePropertyName = this.inflector.InflectItemsSourcePropertyName(control.Name);
            var itemsSourceProperty = properties.SingleOrDefault(x => x.Name == itemsSourcePropertyName);

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
            var textPropertyName = this.inflector.InflectTextPropertyName(control.Name);
            var textProperty = properties.SingleOrDefault(x => x.Name == textPropertyName);

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
