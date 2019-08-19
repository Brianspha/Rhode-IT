using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Rhode_IT.Droid.Activities
{
    [Activity(Label = "Rhode IT", NoHistory = true, Icon = "@drawable/icon", Theme = "@style/MainTheme.Base", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class LaunchScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Splash);
            // Create your application here
            ThreadPool.QueueUserWorkItem(o => LoadActivity());
            // Create your application here
        }
        private void LoadActivity()
        {
            Thread.Sleep(1000); // Simulate a long pause
            Bundle animationBundle = ActivityOptions.MakeCustomAnimation(this, Resource.Animation.abc_fade_in, Resource.Animation.abc_fade_out).ToBundle();

            // Pass on the Bundle object to the StartActivity method 
            RunOnUiThread(() => StartActivity(new Intent(this, typeof(MainActivity)), animationBundle));
        }
    }
}