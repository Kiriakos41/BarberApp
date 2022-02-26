using Xamarin.Forms;
using App1.ViewModels;


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
            vm.OnAppearing();
            vm.LoadItems();
        }
    }
}