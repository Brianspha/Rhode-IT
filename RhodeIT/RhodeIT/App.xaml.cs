using RhodeIT.Databases;
using RhodeIT.Helpers;
using RhodeIT.Models;
using RhodeIT.Views;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(new LightTheme());
            XF.Material.Forms.Material.Init(this);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Variables.SyncFusionLicense);
            db = new RhodeITDB();
            LoginDetails dets = db.GetUserDetails();
            if (!string.IsNullOrEmpty(dets.User_ID))
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

