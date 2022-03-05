using App1.Helpers;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string name;
        private string email;
        private string phone;
        private ImageSource image;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }
        public ImageSource Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public async void CheckUserStats()
        {
            var fb = new FireBaseAppHelper();
            var personId = Preferences.Get("UserID", "");
            var t = await fb.GetPerson(personId);
            var img = Convert.FromBase64String(t.Image);
            var _preferenceImage = Preferences.Get("Image", "");

            if (!string.IsNullOrEmpty(_preferenceImage))
            {
                Stream stream = new MemoryStream(Convert.FromBase64String(_preferenceImage));
                Image = ImageSource.FromStream(() => { return stream; });
            }
            else
            {
                Stream stream = new MemoryStream(img);
                Image = ImageSource.FromStream(() => { return stream; });
                Preferences.Set("Image", t.Image);
            }
        }
        public async void LoadPerson()
        {
            var fb = new FireBaseAppHelper();
            var personId = Preferences.Get("UserID", "");
            var t = await fb.GetPerson(personId);
            Name = t.Name;
            Email = t.Email;
            Phone = t.Phone;
        }
    }
}
