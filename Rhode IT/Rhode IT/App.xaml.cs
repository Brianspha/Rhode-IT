using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using Rhode_IT.Views;
using Prism.Navigation;
using TK.CustomMap.Api.Google;
using Rhode_IT.Databases;
using Rhode_IT.Models;
using System.Collections.Generic;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Rhode_IT
{
    public partial class App : PrismApplication
    {
        private MainDataBase db;

                /* 
        * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
        * This imposes a limitation in which the App class must have a default constructor. 
        * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
        */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzMzOTZAMzEzNjJlMzQyZTMwSDdIU0lXazFvSFpJY2hOM01JeXZHOG1pTDN5RWxqcDhkS1VsZEZoWUYvQT0=");
            GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
            db = new MainDataBase();
            LoginDetails dets = db.hasLoggedInBefore();
        }
        public App(IPlatformInitializer initializer, GeoJSONData data, List<VenueLocation> list) : base(initializer)
        {
            GmsPlace.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            GmsDirection.Init("AIzaSyCJN3Cd-Sp1a5V5OnkvTR-Gqhx7A3S-b6M");
            var temp = new GEOJSONTOJSONParser(data, list);//the Dat file sent by each platform to the parent App
            temp.Process();
        
        }
        protected override void OnInitialized()
        {
            InitializeComponent();
            db = new MainDataBase();
            LoginDetails dets = db.hasLoggedInBefore();
            if (!string.IsNullOrEmpty(dets.password) && !string.IsNullOrEmpty(dets.userID))
            {
                MainPage = new TabbedView(dets);
            }
            else
            {
                MainPage = new MainPage();
            }
            // await NavigationService.NavigateAsync("NavigationPage/ScannerPage");
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TabbedView>();
            containerRegistry.RegisterForNavigation<MainPage>();

        }
    }
}
