using Prism.Navigation;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.ViewModels;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RhodeIT.Views
{
	public class MainMenuTab : ContentPage
    {
        private MainMenuTabsViewModel menuViewModel;
        LoginDetails dets;
        RhodeITDB db;
        public MainMenuTab()
        {
            db = new RhodeITDB();
            dets = db.hasLoggedInBefore();
            Title = "Welcome "+dets.User_ID;
            menuViewModel = new MainMenuTabsViewModel();
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
            Content = menuViewModel.main;
        }
        private void LogOut()
        {
            Application.Current.MainPage = new LoginPage();
            RhodeITDB db = new RhodeITDB();
            db.logOut();
        }

        public int CheckPlatform()
        {
            return Device.RuntimePlatform == Device.iOS ? 0 : 1;
        }

    }
}