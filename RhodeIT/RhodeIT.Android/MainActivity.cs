
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using RhodeIT.Databases;

namespace RhodeIT.Droid
{
    [Activity(Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    [assembly: ExportRenderer(typeof(ClusteredMap), typeof(ClusteredMapRenderer))]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            XamEffects.Droid.Effects.Init();
            ImageCircleRenderer.Init();
            Syncfusion.XForms.Android.PopupLayout.SfPopupLayoutRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState); // initialize for Xamarin.Forms.GoogleMaps
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            UserDialogs.Init(this);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
        }

    }
}