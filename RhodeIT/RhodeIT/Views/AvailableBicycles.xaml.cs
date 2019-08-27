using RhodeIT.ViewModels;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RhodeIT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AvailableBicycles : SlideMenuView
    {
        AvailableBicylesViewModel viewModel;
        int maxHeight = 250;
        Command closeMenu;
		public AvailableBicycles (string venue, Command showMenu, StackLayout main)
		{
			InitializeComponent ();
            closeMenu = new Command(CloseMenu());
            viewModel = new AvailableBicylesViewModel(venue, showMenu, closeMenu, bicycles);
            BindingContext = viewModel;
            bicycles.WidthRequest = Application.Current.MainPage.Width;

         // You must set HeightRequest in this case
        HeightRequest = maxHeight;
        // You must set IsFullScreen in this case, 
        // otherwise you need to set WidthRequest, 
        // just like the QuickInnerMenu sample
        IsFullScreen = true;
        MenuOrientations = MenuOrientation.BottomToTop;

        // You must set BackgroundColor, 
        // and you cannot put another layout with background color cover the whole View
        // otherwise, it cannot be dragged on Android
        BackgroundColor = Color.WhiteSmoke;
        BackgroundViewColor = Color.Transparent;
  
        // In some small screen size devices, the menu cannot be full size layout.
        // In this case we need to set different size for Android.
        if (Device.RuntimePlatform == Device.Android)
            HeightRequest += 30;
        Content = viewModel.ListViewParent;
		}

        private Action CloseMenu()
        {
            return new Action(()=> {
                HeightRequest = 0;
            });
        }
    }
}
