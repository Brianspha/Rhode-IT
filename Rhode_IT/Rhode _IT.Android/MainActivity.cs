
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Rhode_IT.Android;
using Rhode_IT.Databases;
using Rhode_IT.Helpers;

namespace Rhode_IT.Droid
{
    [Activity(Label = "Rhode_IT", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,NoHistory =true)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Rg.Plugins.Popup.Popup.Init(this,savedInstanceState);
            UserDialogs.Init(this);
            var db = new MainDataBase();
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
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}