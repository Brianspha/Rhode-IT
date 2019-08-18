
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using RhodeIT.Android;
using RhodeIT.Databases;
using RhodeIT.Helpers;

namespace RhodeIT.Droid
{
    [Activity(Label = "RhodeIT", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    [assembly: ExportRenderer(typeof(ClusteredMap), typeof(ClusteredMapRenderer))]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState); // initialize for Xamarin.Forms.GoogleMaps
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            var db = new RhodeITDB();
            if (db.FirstRun())
            {
                var temp = new ResourceHelper(this.ApplicationContext, "RhodesMap.geojson", "Venues.txt");
                LoadApplication(new App(temp.ReadLocalFile(), temp.GetParsedVenuesWithSubjects()));
            }
            else
            {
                LoadApplication(new App());
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
        }

    }
}