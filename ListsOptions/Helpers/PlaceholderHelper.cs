using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;

namespace ListsOptionsUI.Helpers
{
    public static class PlaceholderHelper
    {
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.RegisterAttached("PlaceholderText", typeof(string), typeof(PlaceholderHelper),
                new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

        public static void SetPlaceholderText(UIElement element, string value)
        {
            element.SetValue(PlaceholderTextProperty, value);
        }

        public static string GetPlaceholderText(UIElement element)
        {
            return (string)element.GetValue(PlaceholderTextProperty);
        }

        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.Loaded += (s, ev) => ShowPlaceholder(textBox);
                textBox.GotFocus += RemovePlaceholder;
                textBox.LostFocus += (s, ev) => ShowPlaceholder(textBox);
            }

            if (d is PasswordBox passwordBox)
            {
                passwordBox.Loaded += (s, ev) => ShowPasswordPlaceholder(passwordBox);
                passwordBox.GotFocus += RemovePasswordPlaceholder;
                passwordBox.LostFocus += (s, ev) => ShowPasswordPlaceholder(passwordBox);
            }
        }

        private static void ShowPlaceholder(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = GetPlaceholderText(textBox);
                textBox.Foreground = Brushes.Gray;
            }
        }

        private static void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text == GetPlaceholderText(textBox))
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private static void ShowPasswordPlaceholder(PasswordBox passwordBox)
        {
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Tag = GetPlaceholderText(passwordBox); // Показва се като tag
                passwordBox.Foreground = Brushes.Gray;
            }
        }

        private static void RemovePasswordPlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.Password = "";
                passwordBox.Tag = "";
                passwordBox.Foreground = Brushes.Black;
            }
        }
    }
    public static class PasswordPlaceholderHelper
    {
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.RegisterAttached("PlaceholderText", typeof(string), typeof(PasswordPlaceholderHelper),
                new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

        public static void SetPlaceholderText(DependencyObject element, string value)
        {
            element.SetValue(PlaceholderTextProperty, value);
        }

        public static string GetPlaceholderText(DependencyObject element)
        {
            return (string)element.GetValue(PlaceholderTextProperty);
        }

        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.Loaded += (s, ev) => AddPlaceholder(passwordBox);
                passwordBox.PasswordChanged += (s, ev) => TogglePlaceholder(passwordBox);
            }
        }

        private static void AddPlaceholder(PasswordBox passwordBox)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(passwordBox);
            if (layer != null)
            {
                layer.Add(new PasswordPlaceholderAdorner(passwordBox, GetPlaceholderText(passwordBox)));
            }
        }

        private static void TogglePlaceholder(PasswordBox passwordBox)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(passwordBox);
            if (layer != null)
            {
                layer.Update(passwordBox);
            }
        }
    }

    public class PasswordPlaceholderAdorner : Adorner
    {
        private readonly PasswordBox _passwordBox;
        private readonly TextBlock _placeholder;

        public PasswordPlaceholderAdorner(PasswordBox passwordBox, string placeholderText)
            : base(passwordBox)
        {
            _passwordBox = passwordBox;
            _placeholder = new TextBlock
            {
                Text = placeholderText,
                Foreground = Brushes.Gray,
                Margin = new Thickness(5, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            _passwordBox.PasswordChanged += (s, e) => InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (string.IsNullOrEmpty(_passwordBox.Password))
            {
                dc.DrawText(
                    new FormattedText(
                        _placeholder.Text,
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"),
                        _passwordBox.FontSize,
                        _placeholder.Foreground),
                    new Point(5, 3)); // Adjust position if needed
            }
        }
    }
}
