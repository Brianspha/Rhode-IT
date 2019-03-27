using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


namespace Rhode_IT.Views
{
    public partial class Scanner : ContentPage
    {
        public Scanner()
        {
            StackLayout temp = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
            SfButton scan = new SfButton();
            scan.BackgroundColor = Color.DeepSkyBlue;
            scan.Text = "Click to scan";
            scan.TextColor = Color.Black;
            scan.Clicked += OpenScanner;
            scan.VerticalOptions = LayoutOptions.CenterAndExpand;
            scan.HorizontalOptions = LayoutOptions.CenterAndExpand;
            temp.Children.Add(scan);
            Content = temp;

        }

        private void OpenScanner(object sender, EventArgs e)
        {
            Scan();
        }

        public async void Scan()
        {

            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) => {
                ScannerPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopAsync();
                    DisplayAlert("Scan results", result.Text, "OK");
                });
            };

            await Navigation.PushAsync(ScannerPage);

        }
    }
}
