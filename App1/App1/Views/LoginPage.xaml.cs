using System;
using App1.ViewModels;
using App1.Views;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public string WebAPIkey = "AIzaSyB7FsF5bZucwpfw_pyH-LUNrjKsdadBEek";
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        async void loginbutton_Clicked(System.Object sender, System.EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserLoginEmail.Text, UserLoginPassword.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("UserID", content.User.LocalId);
                var p = Preferences.Get("UserID", "");
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                await Shell.Current.GoToAsync("//AboutPage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Invalid useremail or password", "OK");
            }
        }
    }
}