using Acr.UserDialogs;
using Rhode_IT.ViewModels;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Rhode_IT.Views
{
	public class ConnectToPlatformTab : ContentPage
	{
        ScannerPageViewModel scannerPageViewModel;
        public ConnectToPlatformTab ()
		{
            scannerPageViewModel = new ScannerPageViewModel();
            BindingContext = scannerPageViewModel;
            // The root page of your application
            Content = scannerPageViewModel.grid;
            scannerPageViewModel.SetUp();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            scannerPageViewModel.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            scannerPageViewModel.IsScanning = false;

        }
    }
}