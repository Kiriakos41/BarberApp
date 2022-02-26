
using App1.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTc1Mzc4QDMxMzkyZTM0MmUzMG5NamV5YnAzV2xWZXJqazFSblIyL0tlY3laTFlFV25hc3lPRUtTb2pWekk9");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
