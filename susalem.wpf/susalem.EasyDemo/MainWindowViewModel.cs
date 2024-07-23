using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace susalem.EasyDemo
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public ICommand ParaSettingCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("ParameterSettingView", keyValuePairs);
            });
        }

        public ICommand LoginCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("LoginRecordView", keyValuePairs);

            });
        }

        public ICommand ChambrierenCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("ChambrierenView", keyValuePairs);
            });
        }

        public ICommand OperateCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("OperateMachineView", keyValuePairs);
            });
        }

        public ICommand HistoryCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("HistoryRecordView", keyValuePairs);
            });
        }

        public ICommand AlarmCommand
        {
            get => new DelegateCommand(() =>
            {
                NavigationParameters keyValuePairs = new NavigationParameters();
                //keyValuePairs.Add("Menu", menuItem);
                _regionManager.Regions["MainRegion"].RequestNavigate("AlarmRecordView", keyValuePairs);
            });
        }

    }
}
