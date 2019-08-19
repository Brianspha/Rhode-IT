using RhodeIT.Databases;
using RhodeIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RhodeIT.Views
{
    public partial class UserProfile : ContentPage
    {
        UserProfileViewModel UserProfileViweModel;
        public UserProfile()
        {
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
            UserProfileViweModel = new UserProfileViewModel();
            Content = UserProfileViweModel.Main;
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