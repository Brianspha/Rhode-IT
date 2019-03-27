using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Acr.UserDialogs;
using Realms;
using System.Threading.Tasks;
using TK.CustomMap.Api;
using TK.CustomMap.Api.Google;
using TK.CustomMap.Api.OSM;
using TK.CustomMap.Interfaces;
using TK.CustomMap.Overlays;
using Xamarin.Forms;
using TK.CustomMap;
using Xamarin;
using TK.CustomMap.iOSUnified;
using Syncfusion.SfRadialMenu.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.TextInputLayout;
using Syncfusion.XForms.iOS.PopupLayout;
using Rhode_IT.iOS.Helpers;
using Rhode_IT.Databases;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Prism;
using Prism.Ioc;

namespace Rhode_IT.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
           SfPopupLayoutRenderer.Init();
            SfButtonRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            new SfBusyIndicatorRenderer();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            var renderer = new TKCustomMapRenderer();
            var temp = new ResourceHelper();
            MainDataBase db = new MainDataBase();
            SfPopupLayoutRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Syncfusion.ListView.XForms.iOS.SfListViewRenderer.Init();
            SfRadialMenuRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            if (db.FirstRun())
            {
                LoadApplication(new App(new iOSInitializer(),temp.Process(), temp.GetParsedVenuesWithSubjects()));
            }
            else
            {
                LoadApplication(new App(new iOSInitializer()));
            }
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
            }
        }
    }
}
