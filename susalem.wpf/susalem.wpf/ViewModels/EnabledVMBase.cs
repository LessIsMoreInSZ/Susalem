using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace susalem.wpf.ViewModels
{
    public partial class EnabledVMBase : ObservableObject, IRecipient<PropertyChangedMessage<bool>>
    {
        [ObservableProperty]
        protected bool isEnabled;
        public EnabledVMBase()
        {
            RegisterIsEnabled();
        }
        public void Receive(PropertyChangedMessage<bool> message)
        {
            IsEnabled= message.NewValue;
        }

        protected void RegisterIsEnabled()
        {
            WeakReferenceMessenger.Default.Register<PropertyChangedMessage<bool>>(this);
        }
    }
}
