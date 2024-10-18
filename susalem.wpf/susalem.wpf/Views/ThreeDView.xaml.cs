using HelixToolkit.Wpf;
using susalem.wpf.Visual3D;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace susalem.wpf.Views
{
    /// <summary>
    /// ThreeDView.xaml 的交互逻辑
    /// </summary>
    public partial class ThreeDView : UserControl
    {
        public ThreeDView()
        {
            InitializeComponent();
            cv.Source = "TDModels\\efem.3ds"; 

        }

        private void efem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(vp);
            HitTestResult result = VisualTreeHelper.HitTest(vp, pos);
            if (result is RayHitTestResult rayResult)
            {
                var fileModel = sender as ChildSelectableVisual3D;
                //var group = fileModel.Group;
                var child = rayResult.ModelHit as GeometryModel3D;
                child.Material = new DiffuseMaterial(Brushes.Red);
            }
        }

        private void vp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 获取鼠标点击位置
            Point mousePosition = e.GetPosition(vp);

            HitTestResult result = VisualTreeHelper.HitTest(vp, mousePosition);
            if (result is RayHitTestResult rayResult)
            {
                var fileModel = sender as ChildSelectableVisual3D;
                //var group = fileModel.Group;
                var child = rayResult.ModelHit as GeometryModel3D;
                child.Material = new DiffuseMaterial(Brushes.Red);
            }

            //if (result is RayHitTestResult rayResult)
            //{
            //    //var hitResult = o as RayHitTestResult;
            //    //if (hitResult != null && hitResult.ModelHit is MeshGeometry3D modelHit)
            //    //{
                    
            //    //}

            //    // 获取选中的模型的键值
            //    string key = result.VisualHit.?.ToString() ?? "No Key";

            //    // 显示选中的模型的键值
            //    MessageBox.Show($"Selected object with key: {key}");
            //}

        }
    }
}
