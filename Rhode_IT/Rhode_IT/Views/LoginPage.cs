using Rhode_IT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rhode_IT.Views
{
	public class LoginPage : ContentPage
	{
        LoginPageViewModel model;
		public LoginPage ()
		{
            model = new LoginPageViewModel();
            Content = model.Maincontent;
		}
	}
}