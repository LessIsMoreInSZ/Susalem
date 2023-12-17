using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using susalem.wpf.Enums;
using susalem.wpf.Messengers;
using susalem.wpf.Models;
using System;
using System.Collections.ObjectModel;

namespace susalem.wpf.ViewModels.ListViewModels
{
    public partial class MachineListViewModel:ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Machine> machines=new();

        [RelayCommand]
        void AddMachine()
        {
            var newId= Guid.NewGuid().ToString();
            Machine machine = new() { Id = newId ,Icon= "MicrochipSolid" };
            Machines.Add(machine);
            AlarmLight alarmLight = new() { Id = newId, State = AlarmState.Off };
            WeakReferenceMessenger.Default.Send(new AlarmLightMessage(alarmLight));
        }
    }
}
