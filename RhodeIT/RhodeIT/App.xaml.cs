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
        private LocalDataBase db;
        public static double ScreenHeight;
        public static double ScreenWidth;
        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTE3MjNAMzEzNzJlMzEyZTMwUUU4Q3NGUitCN0ZVQ09yTmxReDV3VlBRZ0xWNUdOdEJNYmoxWjhNNTh2bz0=;OTE3MjRAMzEzNzJlMzEyZTMwbjJUSnlaTkFLUGdKYXdhYjBNeHBsQUNhcThGVFNvZndoL1owQ0tTTUJIbz0=;OTE3MjVAMzEzNzJlMzEyZTMwWVpzcGM2RFZKUXN2VUh6eHRRNzVvbjIrV1Uzck1jS0wva2ZFcEpnaUFUQT0=;OTE3MjZAMzEzNzJlMzEyZTMwRlNSU2R2NjlHNkZ4T0I2b3ZDSnJ3V2YzMlpmL1Z5VWpWbVNiOFZJNGp4MD0=;OTE3MjdAMzEzNzJlMzEyZTMwT2dlRzFrRFJxTnY2bnhBSGVjQVpocEhjWFVhbzVDQmxLU0VLVGEwbXptRT0=;OTE3MjhAMzEzNzJlMzEyZTMwWE40LzRkN1ZqNnFNUms5QlRkSDVLN2ZsblNvNy92YnlrWnZRVXdXNU16ST0=;OTE3MjlAMzEzNzJlMzEyZTMwV1ppeDRIdUkwWkdNa1ZtMFhjaVlSVDZzT1JIMFk5K3VZN2hIRkdxVjB5VT0=;OTE3MzBAMzEzNzJlMzEyZTMwVFVqNjJRSUFqcUNTVVJVOWdCU1Z2Sy9kVDBxUU9yQTJTNGdjdjY0N3Uzdz0=;OTE3MzFAMzEzNzJlMzEyZTMwSDZpQnpWZUhKbW5xdkxjMEFqZTFYbTE3SHIwVStVUmNUWW9PMHVURWtWZz0=;OTE3MzJAMzEzNzJlMzEyZTMwT2dlRzFrRFJxTnY2bnhBSGVjQVpocEhjWFVhbzVDQmxLU0VLVGEwbXptRT0=");
            db = new LocalDataBase();
            LoginDetails dets = db.hasLoggedInBefore();
            if (!string.IsNullOrEmpty(dets.userID))
            {
                Current.MainPage = new MainMenuTab();
            }
            else
            {
                Current.MainPage = new LoginPage();
            }
        }

        public App(GeoJSONData geoJSONData, List<VenueLocation> locations)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTA3MjBAMzEzNzJlMzEyZTMwVUlxWWx0MnZsaUV4WlBXajk0dmdoNm1GaE9NY0ducUNrUEZ4Q0J2Q0krRT0=;OTA3MjFAMzEzNzJlMzEyZTMwQzY2OUhmQldpa1dPZnQ2VDFqdGhoZTZ5elFuenAyKzdiMTVmK2tBZlFsYz0=;OTA3MjJAMzEzNzJlMzEyZTMwVEtNRC84UVFiQnZtU2tIZWxUMC9DaXhPNzlGMS9EZDJQU2FFWmt5djRJMD0=;OTA3MjNAMzEzNzJlMzEyZTMwT3ZSenM0QSsxeVVkc3cvSWw5N2ZTZjRNRnVOb2tWcTFkbXEvLzhRekswZz0=;OTA3MjRAMzEzNzJlMzEyZTMwb1NOc0d0ZTIwY0ZQV3dNVjBnaHVCVXVZelNjNzZXRUhFejRvbVBSZFdnST0=;OTA3MjVAMzEzNzJlMzEyZTMwUnFOYm0yQlRaenJkdEl0Nmh1ZlpSa09NTjJVclAvYVBIcVJ5ZkdWTy9uND0=;OTA3MjZAMzEzNzJlMzEyZTMwU1E5R1daREtET0xqNVEwMmVpdy9BeTNLM1UwZDFWVXAzdWU0UXpIQU9nQT0=;OTA3MjdAMzEzNzJlMzEyZTMwSWpjSzJsaTZBcHR2YTN4WFBnTFM3bmRvVUJMUmZiZkRuY3EzYTBLZjU5dz0=;OTA3MjhAMzEzNzJlMzEyZTMwVkNkQ0g4eWtkZkMxVCtrUlU3M2pPbExqUkRlUTBpZnVlRDg5MFdvTE5VTT0=;OTA3MjlAMzEzNzJlMzEyZTMwb1NOc0d0ZTIwY0ZQV3dNVjBnaHVCVXVZelNjNzZXRUhFejRvbVBSZFdnST0=");
            db = new LocalDataBase();
            GEOJSONParser temp = new GEOJSONParser(geoJSONData, locations);
            temp.Process();
            db = new LocalDataBase();
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

