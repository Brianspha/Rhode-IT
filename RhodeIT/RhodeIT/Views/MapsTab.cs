using RhodeIT.Databases;
using RhodeIT.ViewModels;
using SlideOverKit;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RhodeIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsTab : MenuContainerPage
    {
        private MapsTabViewModel viewModel;
        public MapsTab()
        {
            Command show = new Command(() => ShowMenu());
            ShowMenu();
            viewModel = new MapsTabViewModel(SlideMenu, show);
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
            Content = viewModel.Main;
        }
        public int CheckPlatform()
        {
            return Device.RuntimePlatform == Device.iOS ? 0 : 1;
        }
        private void LogOut()
        {
            RhodeITDB db = new RhodeITDB();
            db.LogOut();
            Application.Current.MainPage = new LoginPage();
            
        }
    }
}