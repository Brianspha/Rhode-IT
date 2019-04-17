using Rhode_IT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rhode_IT.Views
{
	public class UserProfile : ContentPage
	{
        UserProfileViewModel UserProfileViweModel;
		public UserProfile ()
		{
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
	}
}