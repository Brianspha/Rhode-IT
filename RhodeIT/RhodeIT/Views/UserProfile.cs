using RhodeIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RhodeIT.Views
{
	public class UserProfile : ContentPage
	{
        UserProfileViewModel UserProfileViweModel;
		public UserProfile ()
		{
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
            UserProfileViweModel = new UserProfileViewModel();
            Content = new StackLayout
            {
                Children =
                {
                    new Label{Text="To do"}
                },
                HorizontalOptions=LayoutOptions.CenterAndExpand,
                VerticalOptions=LayoutOptions.CenterAndExpand
              
            };
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