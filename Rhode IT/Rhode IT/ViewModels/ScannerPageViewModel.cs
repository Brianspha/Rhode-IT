using Acr.UserDialogs;
using Rhode_IT.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Rhode_IT.ViewModels
{
  public  class ScannerPageViewModel:INotifyPropertyChanged
    {
        ZXingScannerPage scanPage;
        private ZXingScannerView zxing;
        private ZXingDefaultOverlay overlay;
        private bool scanning;
        public Grid grid { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsScanning
        {
            get
            {
                return scanning;
            }
            set
            {
                if(value != IsScanning)
                {
                    scanning = value;
                    OnPropertyChanged(nameof(IsScanning));
                }
            }
        }

        public ScannerPageViewModel()
        {
            SetUp();
        }

       
        /// <summary>
        /// Setups the scanner page
        /// </summary>
        public void SetUp()
        {
            var current = UserDialogs.Instance;
            scanPage = new ZXingScannerPage();
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            zxing.Options = new MobileBarcodeScanningOptions()
            {
                UseFrontCameraIfAvailable = false, //update later to come from settings
                PossibleFormats = new List<BarcodeFormat>(),
                   TryHarder = true,
                   AutoRotate = false,
                   TryInverted = true,
                   DelayBetweenContinuousScans = 2000
               };
            zxing.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
            zxing.Options.PossibleFormats.Add(BarcodeFormat.DATA_MATRIX);
            zxing.Options.PossibleFormats.Add(BarcodeFormat.EAN_13);
            if (zxing.IsScanning)
            {
                zxing.AutoFocus();
            }
             zxing.OnScanResult += (result) =>
          
                Device.BeginInvokeOnMainThread(async () => {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    // Show an alert
                    await current.AlertAsync("Scanned Barcode", result.Text, "OK");
                    // Navigate away
                    var mainPage = new MainPage();//this could be content page
                    TabbedViewViewModel viewModel = new TabbedViewViewModel(4);//@dev indicate the index of the starting tab
                    var rootPage = new NavigationPage(new TabbedView(viewModel));
                    Xamarin.Forms.Application.Current.MainPage = rootPage;
                });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch,
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
             grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);
            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(scanPage);



        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
