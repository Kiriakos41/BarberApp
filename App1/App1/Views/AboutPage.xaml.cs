using Xamarin.Forms;
using App1.ViewModels;
using Xamarin.Essentials;

namespace App1.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel vm;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = vm = new AboutViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadItems();
            var personId = Preferences.Get("UserID", "");
            if (personId != "xli4nmKr66NPDeKIvkqF6qBrbVL2")
            {
                MessagingCenter.Send<App, string>(App.Current as App, "User", "");
            }
            else
            {
                MessagingCenter.Send<App, string>(App.Current as App, "Admin", "");
            }
        }

    }
}