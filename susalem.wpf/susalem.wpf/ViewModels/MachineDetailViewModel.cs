using CommunityToolkit.Mvvm.ComponentModel;
using susalem.wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.ViewModels
{
    public partial class MachineDetailViewModel:EnabledVMBase
    {
        [ObservableProperty]
        ObservableCollection<MachineCommand> commandList = new()
        {
            new(){DateTime= DateTime.Now,Command="(Test)Try Connect..."},
            new(){DateTime= DateTime.Now,Command="(Test)Successfully Connected."},
            new(){DateTime= DateTime.Now,Command="(Test)Failed to Connect!"},
        };
        public MachineDetailViewModel()
        {
            
        }
    }
}
