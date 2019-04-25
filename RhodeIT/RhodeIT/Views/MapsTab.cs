﻿using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Prism;
using RhodeIT.ViewModels;
using Xamarin.Forms.Xaml;
using SlideOverKit;

namespace RhodeIT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapsTab : MenuContainerPage
    {
        MapsTabViewModel viewModel;
        public MapsTab()
        {
            var close = new Action(HideMenu);
            var show = new Action(ShowMenu);

            viewModel = new MapsTabViewModel(SlideMenu,close,show);
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

        }
    }
}