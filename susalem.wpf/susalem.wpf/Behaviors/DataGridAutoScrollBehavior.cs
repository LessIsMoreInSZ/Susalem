using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace susalem.wpf.Behaviors
{
    public class DataGridAutoScrollBehavior : Behavior<DataGrid>
    {
        public static double GetVerticalScrollOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalScrollOffsetProperty);
        }

        public static void SetVerticalScrollOffset(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalScrollOffsetProperty, value);
        }

        public static readonly DependencyProperty VerticalScrollOffsetProperty = DependencyProperty.RegisterAttached("VerticalScrollOffset", typeof(double), typeof(DataGridAutoScrollBehavior), new PropertyMetadata(0d, OnVerticalScrollOffsetChanged));

        private static void OnVerticalScrollOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid dg)
            {
                var child = VisualTreeHelper.GetChild(dg, 0);
                if (child is Border bd)
                {
                    if (bd.Child is ScrollViewer sv)
                    {
                        sv.ScrollToVerticalOffset((double)e.NewValue);
                    }
                }
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.LoadingRow += OnLoadingRow;
        }


        private void OnLoadingRow(object? sender, DataGridRowEventArgs e)
        {
            try
            {
                var child = VisualTreeHelper.GetChild(AssociatedObject, 0);
                if (child is Border bd)
                {
                    if (bd.Child is ScrollViewer sv)
                    {
                        var begin = sv.VerticalOffset;
                        var to = sv.ExtentHeight;
                        var doubleAnimation = new DoubleAnimation(begin, to, new Duration(TimeSpan.FromSeconds(2)));
                        AssociatedObject.BeginAnimation(VerticalScrollOffsetProperty, doubleAnimation);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.LoadingRow -= OnLoadingRow;
        }



    }
}
