using HslCommunication.Core.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace susalem.EasyDemo.ViewModels.Dialogs
{
    public class MessageViewModel : BindableBase, IDialogAware
    {
        #region IDialogAware实现

        public string Title => "";

        public event Action<IDialogResult>? RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Content"))
            {
                Content = parameters.GetValue<string>("Content");
            }
        }

        #endregion

        private string _languagePath = Path.Combine(AppContext.BaseDirectory, "Language");

        public MessageViewModel(IEventAggregator aggregator)
        {

        }


      

        public ICommand SaveCommand
        {
            get => new DelegateCommand(() =>
            {
                DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                RequestClose?.Invoke(dialogResult);
            });
        }

        public ICommand CancelCommand
        {
            get => new DelegateCommand(() =>
            {
                DialogResult dialogResult = new DialogResult(ButtonResult.Cancel);
                RequestClose?.Invoke(dialogResult);
            });
        }

      
        private string? _content;

        public string? Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged(); }
        }
    }
}
