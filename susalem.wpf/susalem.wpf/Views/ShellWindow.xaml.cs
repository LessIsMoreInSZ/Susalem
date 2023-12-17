using MahApps.Metro.Controls;
using Prism.Regions;
using susalem.wpf.Constants;
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
using System.Windows.Shapes;

namespace susalem.wpf.Views
{
    /// <summary>
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow : MetroWindow
    {
        public ShellWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            min.Click += (s, e) => this.WindowState = WindowState.Minimized;
            close.Click += (s, e) => this.Close();
            max.Click += (s, e) =>
            {
                if (this.WindowState==WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                    ico.Kind = MahApps.Metro.IconPacks.PackIconForkAwesomeKind.WindowRestore;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    ico.Kind = MahApps.Metro.IconPacks.PackIconForkAwesomeKind.WindowMaximize;
                }
            };

            RegionManager.SetRegionName(list, Regions.List);
            RegionManager.SetRegionManager(list,regionManager);
            RegionManager.SetRegionName(main, Regions.Main);
            RegionManager.SetRegionManager(main, regionManager);
            RegionManager.SetRegionName(pane, Regions.Pane);
            RegionManager.SetRegionManager(pane, regionManager);

        }

        public void WindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton==MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
