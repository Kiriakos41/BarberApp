using System;
using System.IO;
using App1.Helpers;
using App1.ViewModels;
using Firebase.Auth;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Drawing;
using Xamarin.Essentials;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public string ImageString { get; set; }
        MediaFile photo { get; set; }
        public string WebAPIkey = "AIzaSyB7FsF5bZucwpfw_pyH-LUNrjKsdadBEek";
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel();
        }

        async void signupbutton_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(UserNewEmail.Text, UserNewPassword.Text);
                string gettoken = auth.FirebaseToken;
                var content = await auth.GetFreshAuthAsync();

                var fb = new FireBaseAppHelper();
                await fb.AddPerson(content.User.LocalId, UserNewName.Text, UserNewEmail.Text, UserNewPhone.Text, ImageString);

                await App.Current.MainPage.DisplayAlert("Alert", gettoken, "Ok");
                await Shell.Current.GoToAsync("//AboutPage");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.PickPhotoAsync();
            var ph = System.IO.File.ReadAllBytes(photo.Path);
            ImageString = Convert.ToBase64String(ph);
            var img = Convert.FromBase64String(ImageString);

            //imgChoosed.Source = img;

            Image image = new Image();
            Stream stream = new MemoryStream(img);
            imgChoosed.Source = ImageSource.FromStream(() => { return stream; });

        }

    }
}