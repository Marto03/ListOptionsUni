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
            else if (d is PasswordBox passwordBox)
            {
                passwordBox.Loaded += (s, ev) => AddPasswordBoxPlaceholder(passwordBox);
                passwordBox.GotFocus += RemovePasswordBoxPlaceholder;
                passwordBox.LostFocus += (s, ev) => AddPasswordBoxPlaceholder(passwordBox);
            }
        }

        // За TextBox - Placeholder вътре в самото поле
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

        // За PasswordBox - Adorner (наслояване на текст)
        private static void AddPasswordBoxPlaceholder(PasswordBox passwordBox)
        {
            var layer = AdornerLayer.GetAdornerLayer(passwordBox);
            if (layer == null) return;

            RemovePasswordBoxPlaceholder(passwordBox, null);

            if (string.IsNullOrEmpty(passwordBox.Password))
            {
                var placeholder = new PasswordPlaceholderAdorner(passwordBox, GetPlaceholderText(passwordBox));
                layer.Add(placeholder);
            }
        }

        internal static void RemovePasswordBoxPlaceholder(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                var layer = AdornerLayer.GetAdornerLayer(passwordBox);
                if (layer == null) return;

                var adorners = layer.GetAdorners(passwordBox);
                if (adorners == null) return;

                foreach (var adorner in adorners)
                {
                    if (adorner is PasswordPlaceholderAdorner)
                    {
                        layer.Remove(adorner);
                    }
                }
            }
        }
    }

    // Adorner за визуализация на placeholder в PasswordBox
    public class PasswordPlaceholderAdorner : Adorner
    {
        private readonly TextBlock _placeholderTextBlock;

        public PasswordPlaceholderAdorner(PasswordBox passwordBox, string placeholderText)
            : base(passwordBox)
        {
            _placeholderTextBlock = new TextBlock
            {
                Text = placeholderText,
                Foreground = Brushes.Gray,
                Margin = new Thickness(5, 2, 0, 0),
                FontSize = 15,
                IsHitTestVisible = false // ТОВА ПОЗВОЛЯВА КЛИКОВЕТЕ ДА МИНАВАТ ПРЕЗ ТЕКСТА!
            };

            AddVisualChild(_placeholderTextBlock);

            // Добавяме събитие за клик - когато кликнеш на текста, се маха и фокусира полето
            this.MouseDown += (s, e) =>
            {
                passwordBox.Focus();
                PlaceholderHelper.RemovePasswordBoxPlaceholder(passwordBox, null);
            };
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index) => _placeholderTextBlock;

        protected override Size ArrangeOverride(Size finalSize)
        {
            _placeholderTextBlock.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
