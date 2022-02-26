using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command Register { get; set; }

        public LoginViewModel()
        {
            Register = new Command(GoToRegisterPage);
        }
        public async void GoToRegisterPage()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
    }
}
