using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Prism.Regions;
using susalem.wpf.Common;
using susalem.wpf.Constants;
using susalem.wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace susalem.wpf.ViewModels
{
    public partial class ShellWindowViewModel : ObservableObject
    {
        IRegionNavigationService _navigationService;

        MetroWindow shell;

        [ObservableProperty]
        MenuItem selectedItem;

        [ObservableProperty]
        ObservableCollection<MenuItem> menu = new()
        {
            new(){Page=Pages.Machine,Icon="CubesSolid"},
            new(){Page=Pages.Message,Icon="CommentDotsRegular"},
            new(){Page=Pages.Community,Icon="CommentsRegular"},
            new(){Page=Pages.Enterprise,Icon="BuildingRegular"},
        };
        
        [RelayCommand]
        void Loaded(MetroWindow metroWindow)
        {
            shell = metroWindow;
        }

        [RelayCommand]
        async Task Travel()
        {
           var res=await shell.ShowMessageAsync("", "神秘人：你终于下定决心开启你的上位机之旅了吗？", MessageDialogStyle.AffirmativeAndNegative);
            if (res==MessageDialogResult.Affirmative)
            {
                var travelWindow = new TravelWindow();
                travelWindow.ShowDialog();
            }
            else
            {
                await shell.ShowMessageAsync("", "神秘人：很遗憾，希望你好好考虑...", MessageDialogStyle.Affirmative);
            }
        }

        [RelayCommand]
        void Navigate()
        {
            _navigationService.RequestNavigate(SelectedItem.Page);
        }

        public ShellWindowViewModel(IRegionManager regionManager)
        {
            _navigationService = regionManager.Regions[Regions.List].NavigationService;
            regionManager.Regions[Regions.Pane].NavigationService.RequestNavigate(Pages.Alarm);

        }
    }
}
