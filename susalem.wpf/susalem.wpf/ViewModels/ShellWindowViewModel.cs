using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using susalem.wpf.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace susalem.wpf.ViewModels
{
    public partial class ShellWindowViewModel : ObservableObject
    {
        MetroWindow shell;
        [ObservableProperty]
        MenuItem selectedItem;
        [ObservableProperty]
        ObservableCollection<MenuItem> menu = new()
        {
            new(){Name="Message",Icon="CommentDotsRegular"},
            new(){Name="Community",Icon="CommentsRegular"},
            new(){Name="Enterprise",Icon="BuildingRegular"}
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

        public ShellWindowViewModel()
        {
            SelectedItem = Menu.FirstOrDefault();
        }
    }
}
