using HslCommunication.Core.Net;
using HslCommunication.Secs.Types;
using Microsoft.Extensions.Logging;
using Prism.Mvvm;
using Prism.Regions;
using susalem.EasyDemo.Models;
using susalem.EasyDemo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace susalem.EasyDemo.ViewModels
{
    public class LoginRecordViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUserService _userService;
        public LoginRecordViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
           // _userService = userService;
            //LoadUser();
        }


        private ObservableCollection<string> _users = new ObservableCollection<string>();

        public ObservableCollection<string> Users
        {
            get { return _users; }
            set { _users = value; RaisePropertyChanged(); }
        }

        private string? _userName;

        public string? UserName
        {
            get { return _userName; }
            set { _userName = value; RaisePropertyChanged(); }
        }

        private string? _password;

        public string? Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(); }
        }

        private string? _errorMessage;

        public string? ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; RaisePropertyChanged(); }
        }

        private void LoadUser()
        {
            var _result = _userService.FindAllUser();

            if (_result!=null)
            {
                foreach (var user in _result)
                {
                    if (!string.IsNullOrWhiteSpace(user.UserName))
                    {
                        Users?.Add(user.UserName);
                    }
                }
            }
        }

        private void Login()
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(UserName))
            {
                ErrorMessage = "UserName is empty";
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                ErrorMessage = "Password is empty";
                return;
            }


            Task.Run(() =>
            {
                try
                {
                    UserModel resultModel = _userService.Login(UserName, Password,false);

                    if (resultModel!=null)
                    {
                        OverAllContext.User = resultModel!;
                        //Application.Current.Dispatcher.Invoke(() => RequestClose?.Invoke(new DialogResult(ButtonResult.OK)));
                        //AppSession.IsLogon = false;
                    }
                    else
                    {
                        ErrorMessage = "登录失败";
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                    //logger.Error(ex.Message);
                }
            });
        }


    }
}
