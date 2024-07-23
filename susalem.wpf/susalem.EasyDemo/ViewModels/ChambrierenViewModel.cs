using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace susalem.EasyDemo.ViewModels
{
    /// <summary>
    /// 回温
    /// </summary>
    internal class ChambrierenViewModel : BindableBase
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();

        

        public ChambrierenViewModel()
        {
            RefreshDateTime();
        }

        private string _clock = string.Empty;

        public string Clock
        {
            get { return _clock; }
            set { _clock = value; RaisePropertyChanged(); }
        }

        private void RefreshDateTime()
        {
            Task.Run(() =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Clock = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    });
                }
            }, _cts.Token);
        }
    }
}
