using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace susalem.EasyDemo.ViewModels.Dialogs
{
    internal class ErrorViewModel : BindableBase, IDialogAware
    {
        public string Title => "Error";

        public event Action<IDialogResult> RequestClose;

        /// <summary>
        /// 工匠品料号
        /// </summary>
        private string? chemicalNum;

        public string? ChemicalNum
        {
            get { return chemicalNum; }
            set { chemicalNum = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 逾期时间
        /// </summary>
        private string? expirationTime;

        public string? ExpirationTime
        {
            get { return expirationTime; }
            set { expirationTime = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 机柜号
        /// </summary>
        private string? cabinetNum;

        public string? CabinetNum
        {
            get { return cabinetNum; }
            set { cabinetNum = value; RaisePropertyChanged(); }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            OverAllContext.IsError = false;
            if (parameters.ContainsKey("ChemicalNum") && parameters.ContainsKey("ExpirationTime") &&
                parameters.ContainsKey("CabinetNum"))
            {
                ChemicalNum = parameters.GetValue<string>("ChemicalNum");
                ExpirationTime = parameters.GetValue<string>("ExpirationTime");
                CabinetNum = parameters.GetValue<string>("CabinetNum");
            }
        }

        public ICommand ConfirmCommand
        {
            get => new DelegateCommand(() =>
            {
                OverAllContext.IsError = true;
                RequestClose.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters() { }));
            });
        }
    }
}
