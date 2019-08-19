using Prism.Navigation;
using Rhode_IT.Databases;
using Rhode_IT.Models;
using Rhode_IT.ViewModels;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rhode_IT.Views
{
	public class MainMenuTab : ContentPage
    {
        private MainMenuTabViewModel menuViewModel;
        LoginDetails dets;
        MainDataBase db;
        public MainMenuTab()
        {
            db = new MainDataBase();
            dets = db.hasLoggedInBefore();
            Title = "Welcome "+dets.userID;
            menuViewModel = new MainMenuTabViewModel();
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
            Content = menuViewModel.mainTabView;
        }
        private void LogOut()
        {

        }

        public int CheckPlatform()
        {
            return Device.RuntimePlatform == Device.iOS ? 0 : 1;
        }
    }
}