using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public Command GoBack { get; set; }

        public RegisterViewModel()
        {
            GoBack = new Command(GoToLoginPage);
        }
        public async void GoToLoginPage()
        {
            await Shell.Current.GoToAsync("//AboutPage");
        }
    }
}
