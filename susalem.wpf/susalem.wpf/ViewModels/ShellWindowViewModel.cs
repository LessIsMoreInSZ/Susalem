using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace susalem.wpf.ViewModels
{
    public partial class ShellWindowViewModel:ObservableObject
    {
        [ObservableProperty]
        MenuItem selectedItem; 
        [ObservableProperty]
        ObservableCollection<MenuItem> menu = new()
        {
            new(){Name="Message",Icon="CommentDotsRegular"},
            new(){Name="Community",Icon="CommentsRegular"},
            new(){Name="Enterprise",Icon="BuildingRegular"}
        };
        public ShellWindowViewModel()
        {
            SelectedItem = Menu.FirstOrDefault();
        }
    }
}
