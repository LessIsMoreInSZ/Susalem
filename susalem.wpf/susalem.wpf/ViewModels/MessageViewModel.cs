using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace susalem.wpf.ViewModels
{
    public class MessageViewModel:BindableBase,IDialogAware
    {
        private Timer timer = new Timer(15000);
        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set { SetProperty(ref _titleName, value); }
        }

        private string _message;

        public event Action<IDialogResult> RequestClose;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public string Title => TitleName;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            TitleName = parameters.GetValue<string>("TitleName");
            Message = parameters.GetValue<string>("Message");
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        public ICommand CancelCommand
        {
            get => new DelegateCommand(() =>
            {
                DialogResult dialogResult = new DialogResult(ButtonResult.Cancel);
                RequestClose?.Invoke(dialogResult);
            });
        }

        public ICommand OKCommand
        {
            get => new DelegateCommand(() =>
            {
                DialogResult dialogResult = new DialogResult(ButtonResult.OK);
                RequestClose?.Invoke(dialogResult);
            });
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            DialogResult dialogResult = new DialogResult(ButtonResult.OK);
            Application.Current.Dispatcher.Invoke(() =>
            {
                RequestClose?.Invoke(dialogResult);
            });

        }
    }
}
