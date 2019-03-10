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
        private MainMenuViewModel menuViewModel;

        public MainMenuTab()
		{
            Title = "MainMenu";
            menuViewModel = new MainMenuViewModel();
            BindingContext = menuViewModel;
            Content = menuViewModel.getViews();
        }
	}
}