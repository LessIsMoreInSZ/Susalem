using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace susalem.EasyDemo.ViewModels.Dialogs
{
    internal class WarningViewModel : BindableBase, IDialogAware
    {
        public string Title => "Warn";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Text"))
            {
                Text = parameters.GetValue<string>("Text");
            }
        }

        /// <summary>
        /// 化学品料号
        /// </summary>
        private string? text;

        public string? Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(); }
        }

        public ICommand ConfirmCommand
        {
            get => new DelegateCommand(() =>
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { }));
            });
        }
    }
}
