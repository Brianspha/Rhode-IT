using System;
using System.Collections.Generic;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Rhode_IT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzMzOTZAMzEzNjJlMzQyZTMwSDdIU0lXazFvSFpJY2hOM01JeXZHOG1pTDN5RWxqcDhkS1VsZEZoWUYvQT0=");
            GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
            MainPage = new MainPage();
        }

        public App(GeoJSONData data, List<VenueLocation> list)
        {
            GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
            var temp = new GEOJSONTOJSONParser(data, list);//the Dat file sent by each platform to the parent App
            temp.Process();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            TKNativePlacesApi.Instance.DisconnectAndRelease();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
