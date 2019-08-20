
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using RhodeIT.Databases;
using RhodeIT.Helpers;
using RhodeIT.iOS.Helpers;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfRadialMenu.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;

namespace RhodeIT.iOS
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
            Syncfusion.SfDataGrid.XForms.iOS.SfDataGridRenderer.Init();
            ResourceHelper temp = new ResourceHelper();
            RhodeITDB db = new RhodeITDB();
            SfPopupLayoutRenderer.Init();
            SfListViewRenderer.Init();
            Rg.Plugins.Popup.Popup.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            SfListViewRenderer.Init();
            SfRadialMenuRenderer.Init();
            ImageCircleRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Xamarin.FormsGoogleMaps.Init(Variables.mapsAPIKEY);
            Syncfusion.XForms.iOS.Cards.SfCardViewRenderer.Init();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

    }
}
