using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Prism;
using RhodeIT.ViewModels;
using Xamarin.Forms.Xaml;
using SlideOverKit;
using RhodeIT.Databases;

namespace RhodeIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsTab : MenuContainerPage
    {
        MapsTabViewModel viewModel;
        public MapsTab()
        {
            var show = new Command(() => ShowMenu());
            ShowMenu();
            viewModel = new MapsTabViewModel(SlideMenu,show);
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
            Application.Current.MainPage = new LoginPage();
            RhodeITDB db = new RhodeITDB();
            db.LogOut();
        }
    }
}