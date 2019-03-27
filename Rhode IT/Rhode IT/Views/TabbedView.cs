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
	public class TabbedView : ContentPage
	{
        TabbedViewViewModel tabbedViewViewModel;
        private LoginDetails dets;

        public TabbedView ()
		{
            Title = "Rhode IT";
            tabbedViewViewModel = new TabbedViewViewModel();
            BindingContext = tabbedViewViewModel;
            Content = tabbedViewViewModel.main;
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
        }

        public TabbedView(TabbedViewViewModel viewModel)
        {
            Title = "Rhode IT";
            tabbedViewViewModel = viewModel;
            BindingContext = tabbedViewViewModel;
            Content = tabbedViewViewModel.main;
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
        }

        public TabbedView(LoginDetails dets)
        {
            this.dets = dets;
            Title = "Rhode IT";
            tabbedViewViewModel = new TabbedViewViewModel();
            BindingContext = tabbedViewViewModel;
            Content = tabbedViewViewModel.main;
            ToolbarItem logOut = new ToolbarItem { Text = "Logout", Order = ToolbarItemOrder.Primary, Priority = 1 };
            ToolbarItems.Add(new ToolbarItem("Logout", null, new Action(() => LogOut()), ToolbarItemOrder.Secondary, CheckPlatform()));
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