using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace susalem.wpf.ViewModels
{
    public partial class LoginWindowViewModel : ObservableObject
    {
      
        Window _window;

        [ObservableProperty]
        int index = 0;

        [ObservableProperty]
        string userInfo;

        [ObservableProperty]
        string password;

        [RelayCommand]
        void Loaded(Window window) => _window = window;

        [RelayCommand]
        private void Index0() => Index = 0;

        [RelayCommand]
        private void Index1() => Index = 1;

        [RelayCommand]
        async Task SignIn()
        {
            await Task.Delay(100);
            _window.DialogResult = true;
        }

        [RelayCommand]
        void SignUp(PasswordBox passwordBox)
        {
            MessageBox.Show("Not implemented!");
        }
        public LoginWindowViewModel()
        {

        }
    }
}
