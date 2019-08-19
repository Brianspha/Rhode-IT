using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rhode_IT.Views;
using Prism.Navigation;
using Rhode_IT.Databases;
using Rhode_IT.Models;
using System.Collections.Generic;
using Acr.UserDialogs;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Rhode_IT
{
    public partial class App : Application
    {
        private MainDataBase db;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzMzOTZAMzEzNjJlMzQyZTMwSDdIU0lXazFvSFpJY2hOM01JeXZHOG1pTDN5RWxqcDhkS1VsZEZoWUYvQT0=");
            db = new MainDataBase();
            LoginDetails dets = db.hasLoggedInBefore();
            if (!string.IsNullOrEmpty(dets.password) && !string.IsNullOrEmpty(dets.userID))
            {
                Application.Current.MainPage = new MainMenuTab();
            }
            else
            {
                Application.Current.MainPage = new LoginPage();
            }
        }

        public App(GeoJSONData geoJSONData, List<VenueLocation> locations)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTA3MjBAMzEzNzJlMzEyZTMwVUlxWWx0MnZsaUV4WlBXajk0dmdoNm1GaE9NY0ducUNrUEZ4Q0J2Q0krRT0=;OTA3MjFAMzEzNzJlMzEyZTMwQzY2OUhmQldpa1dPZnQ2VDFqdGhoZTZ5elFuenAyKzdiMTVmK2tBZlFsYz0=;OTA3MjJAMzEzNzJlMzEyZTMwVEtNRC84UVFiQnZtU2tIZWxUMC9DaXhPNzlGMS9EZDJQU2FFWmt5djRJMD0=;OTA3MjNAMzEzNzJlMzEyZTMwT3ZSenM0QSsxeVVkc3cvSWw5N2ZTZjRNRnVOb2tWcTFkbXEvLzhRekswZz0=;OTA3MjRAMzEzNzJlMzEyZTMwb1NOc0d0ZTIwY0ZQV3dNVjBnaHVCVXVZelNjNzZXRUhFejRvbVBSZFdnST0=;OTA3MjVAMzEzNzJlMzEyZTMwUnFOYm0yQlRaenJkdEl0Nmh1ZlpSa09NTjJVclAvYVBIcVJ5ZkdWTy9uND0=;OTA3MjZAMzEzNzJlMzEyZTMwU1E5R1daREtET0xqNVEwMmVpdy9BeTNLM1UwZDFWVXAzdWU0UXpIQU9nQT0=;OTA3MjdAMzEzNzJlMzEyZTMwSWpjSzJsaTZBcHR2YTN4WFBnTFM3bmRvVUJMUmZiZkRuY3EzYTBLZjU5dz0=;OTA3MjhAMzEzNzJlMzEyZTMwVkNkQ0g4eWtkZkMxVCtrUlU3M2pPbExqUkRlUTBpZnVlRDg5MFdvTE5VTT0=;OTA3MjlAMzEzNzJlMzEyZTMwb1NOc0d0ZTIwY0ZQV3dNVjBnaHVCVXVZelNjNzZXRUhFejRvbVBSZFdnST0=");
            db = new MainDataBase();
            GEOJSONTOJSONParser temp = new GEOJSONTOJSONParser(geoJSONData, locations);
            temp.Process();
            db = new MainDataBase();
            LoginDetails dets = db.hasLoggedInBefore();
            var current = UserDialogs.Instance;
            var paramaters = new NavigationParameters();
            if (!string.IsNullOrEmpty(dets.password) && !string.IsNullOrEmpty(dets.userID))
            {
                Current.MainPage = new MainMenuTab();
            }
            else
            {
                Current.MainPage = new LoginPage();
            }
        }

    }
}

