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
            var importer = new ModelImporter();
            var diceGroup = importer.Load(@"D:\susalem\susalem.wpf\susalem.wpf\Assets\dice.obj");
            var dice=new ModelVisual3D() { Content = diceGroup };
            //viewport.Children.Add(dice);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
