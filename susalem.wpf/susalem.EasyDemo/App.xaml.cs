using Prism.Ioc;
using Prism.Unity;
using susalem.EasyDemo.ViewModels;
using susalem.EasyDemo.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace susalem.EasyDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();

            containerRegistry.RegisterForNavigation<AlarmRecordView, AlarmRecordViewModel>();
            containerRegistry.RegisterForNavigation<ChambrierenView, ChambrierenViewModel>();
            containerRegistry.RegisterForNavigation<HistoryRecordView, HistoryRecordViewModel>();
            containerRegistry.RegisterForNavigation<LoginRecordView, LoginRecordViewModel>();
            containerRegistry.RegisterForNavigation<OperateMachineView, OperateMachineViewModel>();
            containerRegistry.RegisterForNavigation<ParameterSettingView, ParameterSettingViewModel>();
        }
    }

}
