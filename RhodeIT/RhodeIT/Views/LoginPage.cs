using RhodeIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RhodeIT.Views
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