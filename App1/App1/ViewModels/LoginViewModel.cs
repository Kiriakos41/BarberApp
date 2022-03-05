using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command Register { get; set; }

        public LoginViewModel()
        {
            Register = new Command(GoToRegisterPage);
            IsIn();
        }
        public async void GoToRegisterPage()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }
        public async void IsIn()
        {
            var t = Preferences.Get("MyFirebaseRefreshToken", "");
            if (t != null)
            {
                await Shell.Current.GoToAsync("//AboutPage");
            }
        }
    }
}
