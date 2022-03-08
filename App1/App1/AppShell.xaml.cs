using App1.Helpers;
using App1.ViewModels;
using App1.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            MessagingCenter.Subscribe<App, string>(App.Current, "Admin", (snd, arg) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    Rantebou.IsVisible = true;
                });
            });
            MessagingCenter.Subscribe<App, string>(App.Current, "User", (snd, arg) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    Rantebou.IsVisible = false;
                });
            });

            InitializeComponent();
            Routing.RegisterRoute("//RegisterPage", typeof(RegisterPage));
            Routing.RegisterRoute("//LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("//ProfilePage", typeof(ProfilePage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
            Preferences.Remove("MyFirebaseRefreshToken");
            Preferences.Remove("Image");
            Shell.Current.FlyoutIsPresented = false;
        }


    }
}
