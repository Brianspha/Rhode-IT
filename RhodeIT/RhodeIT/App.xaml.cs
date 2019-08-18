using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RhodeIT.Views;
using Prism.Navigation;
using RhodeIT.Databases;
using RhodeIT.Models;
using System.Collections.Generic;
using Acr.UserDialogs;
using RhodeIT.Helpers;
using RhodeIT.Classes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RhodeIT
{
    public partial class App : Application
    {
        private RhodeITDB db;
        public static double ScreenHeight;
        public static double ScreenWidth;
        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Variables.SyncFusionLicense);
            db = new RhodeITDB();
            LoginDetails dets = db.hasLoggedInBefore();
            if (!string.IsNullOrEmpty(dets.userID))
            {
                Current.MainPage = new NavigationPage(new MainMenuTab());
            }
            else
            {
                Current.MainPage = new LoginPage();
            }
        }
    }
}

