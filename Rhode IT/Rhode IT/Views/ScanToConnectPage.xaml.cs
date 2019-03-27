using Rhode_IT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rhode_IT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanToConnectPage : ContentPage
	{
		public ScanToConnectPage ()
		{
			InitializeComponent ();
         //   BindingContext = new ScannerPageViewModel();
            scanView.Options.DelayBetweenAnalyzingFrames = 5;
            scanView.Options.DelayBetweenContinuousScans = 5;
            scanView.AutoFocus();
            Content = grid;
        }

        private void Back(object sender, EventArgs e)
        {

        }
    }
}