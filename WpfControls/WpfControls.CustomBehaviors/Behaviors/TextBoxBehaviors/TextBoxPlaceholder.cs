using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace WpfControls.CustomBehaviors.Behaviors
{
    public static class TextBoxPlaceholder
    {

        #region DependencyProperties

        // Register the attached property for the placeholder text
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(TextBoxPlaceholder),
                new PropertyMetadata(string.Empty, OnPlaceholderChanged));
        #endregion

        #region Set Get 

        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        #endregion


        #region Main methods
        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.GotFocus -= RemovePlaceholder;
                textBox.LostFocus -= ShowPlaceholder;

                textBox.GotFocus += RemovePlaceholder;
                textBox.LostFocus += ShowPlaceholder;

                // Initialize the placeholder if the TextBox is not focused and empty
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    ShowPlaceholder(textBox, null);
                }
            }
        }
        #endregion

        #region private Method

        private static void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == GetPlaceholder(textBox))
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black; // Change to your preferred text color
            }
        }

        private static void ShowPlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = GetPlaceholder(textBox);
                textBox.Foreground = Brushes.Gray; // Change to your preferred placeholder color
            }
        }

        #endregion











    }
}