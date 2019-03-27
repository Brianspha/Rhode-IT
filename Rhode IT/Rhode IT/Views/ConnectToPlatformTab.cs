
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Acr.UserDialogs;
using Rhode_IT.Databases;
using Rhode_IT.Models;

namespace Rhode_IT.Views
{
    public partial class ConnectToPlatformTab : ContentPage
    {
        MainDataBase db;
        LoginDetails details;
        public ConnectToPlatformTab()
        {
            db = new MainDataBase();
            Scanner();
        }

        public async void Scanner()
        {

            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) => {
                ScannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () => {
                    var current = UserDialogs.Instance;
                    await Navigation.PopAsync();
                     await current.AlertAsync(result.Text, "Results of Scan", "OK");
                    db.saveIPConfig(new IPConfig { IPAdress = result.Text });
                    await Navigation.PushAsync(new NavigationPage(new MainMenuTab()));
                });
            };
            ScannerPage.IsScanning = true;
            await Navigation.PushAsync(ScannerPage);
        }
    }
}
