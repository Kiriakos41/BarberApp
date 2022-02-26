﻿using App1.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private string _preferenceImage;
        public ProfilePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _preferenceImage = Preferences.Get("Image", "");
            if (!string.IsNullOrEmpty(_preferenceImage))
            {
                Stream stream = new MemoryStream(Convert.FromBase64String(_preferenceImage));
                ProfileImage.Source = ImageSource.FromStream(() => { return stream; });
            } else
            {
                CheckUserStats();
            }
        }
        public async void CheckUserStats()
        {
            var fb = new FireBaseAppHelper();
            var personId = Preferences.Get("UserID", "");
            var t = await fb.GetPerson(personId);
            var img = Convert.FromBase64String(t.Image);
            Image image = new Image();
            Stream stream = new MemoryStream(img);
            ProfileImage.Source = ImageSource.FromStream(() => { return stream; });
            Preferences.Set("Image", t.Image );
        }
    }
}