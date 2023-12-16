using CommunityToolkit.Mvvm.Messaging.Messages;
using susalem.wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Messengers
{
    public class AlarmLightMessage : ValueChangedMessage<AlarmLight>
    {
        public AlarmLightMessage(AlarmLight value) : base(value)
        {
        }
    }
}
