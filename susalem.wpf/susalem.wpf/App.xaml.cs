using Prism.Ioc;
using Prism.Unity;
using susalem.wpf.ViewModels;
using susalem.wpf.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace susalem.wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var login = Container.Resolve<LoginWindow>();
            var result= login.ShowDialog();
            if (result is null||!result.Value)
            {
                App.Current.Shutdown();
                return null;
            }
            else
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose;
                return Container.Resolve<ShellWindow>();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<LoginWindow>();
            containerRegistry.Register<LoginWindowViewModel>();
            containerRegistry.Register<ShellWindow>();
            containerRegistry.Register<ShellWindowViewModel>();
        }
    }
}
