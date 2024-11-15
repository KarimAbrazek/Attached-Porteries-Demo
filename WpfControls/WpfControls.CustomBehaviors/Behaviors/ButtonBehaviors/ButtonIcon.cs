using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace WpfControls.CustomBehaviors.Behaviors
{
    public static class ButtonIcon
    {


        #region DependencyProperties
        // Register an attached property for the icon
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.RegisterAttached(
                "Icon",
                typeof(UIElement),
                typeof(ButtonIcon),
                new PropertyMetadata(null, OnIconChanged));

        // Register an attached property for showing/hiding the icon
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.RegisterAttached(
                "ShowIcon",
                typeof(bool),
                typeof(ButtonIcon),
                new PropertyMetadata(true, OnShowIconChanged));
        #endregion

        #region Set Get 

        public static UIElement GetIcon(DependencyObject obj)
        {
            return (UIElement)obj.GetValue(IconProperty);
        }

        public static void SetIcon(DependencyObject obj, UIElement value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static bool GetShowIcon(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowIconProperty);
        }

        public static void SetShowIcon(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowIconProperty, value);
        }
        #endregion


        #region Main methods
        private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button)
            {
                if (e.NewValue is UIElement newIcon)
                {
                    button.Content = new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Children =
                        {
                            newIcon,
                            new TextBlock { Text = button.Content?.ToString() }
                        }
                    };
                }
            }
        }


        private static void OnShowIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button && button.Content is StackPanel panel)
            {
                bool showIcon = (bool)e.NewValue;
                if (panel.Children.Count > 0)
                {
                    panel.Children[0].Visibility = showIcon ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        #endregion

        #region private Method



        #endregion

    }
}