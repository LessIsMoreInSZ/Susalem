using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace susalem.wpf.Views
{
    /// <summary>
    /// TravelWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TravelWindow : Window
    {
        public TravelWindow()
        {
            InitializeComponent();
        }
         void Previous(object sender, RoutedEventArgs e)
        {
            tran.SelectedIndex--;
        }
        void Next(object sender, RoutedEventArgs e)
        {
            tran.SelectedIndex++;
        }
        void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = false;
            }
            catch (Exception)
            {
            }
           
            Close();
        }
    }
}
