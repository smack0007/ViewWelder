using System.Windows;

namespace ViewWelder
{
    public class ViewContext : IDialogPresenter
    {
        private readonly FrameworkElement element;

        internal ViewContext(FrameworkElement element)
        {
            this.element = element;
        }

        public void ShowInformationMessage(string message, string title)
        {
            var owner = VisualTreeHelperEx.GetAncestor<Window>(this.element);

            if (owner != null)
            {
                MessageBox.Show(owner, message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
