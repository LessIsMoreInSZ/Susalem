using CommunityToolkit.Mvvm.ComponentModel;
using susalem.wpf.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.Models
{
   public partial  class AlarmLight:LowerComputerBase
    {
        [ObservableProperty]
        AlarmState state;
    }
}
