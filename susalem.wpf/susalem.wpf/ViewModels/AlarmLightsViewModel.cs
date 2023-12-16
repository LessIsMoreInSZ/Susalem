using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using susalem.wpf.Enums;
using susalem.wpf.Messengers;
using susalem.wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.ViewModels
{
    public partial class AlarmLightsViewModel : ObservableObject, IRecipient<AlarmLightMessage>
    {
        [ObservableProperty]
        ObservableCollection<AlarmLight> alarmLights = new();
        //= new(Enumerable.Range(0, 5).Select(n => new AlarmLight
        //{
        //    Id = $"Machine No.{n}",
        //    State = (AlarmState)n,
        //}));


        public void Receive(AlarmLightMessage message)
        {
            var alarmLight = AlarmLights.FirstOrDefault(n => n.Id == message.Value.Id);
            if (alarmLight != null)
            {
                alarmLight.State = message.Value.State;
            }
            else
            {
                AlarmLights.Add(message.Value);
            }
        }
        public AlarmLightsViewModel()
        {
            WeakReferenceMessenger.Default.Register(this);
        }
    }
}
