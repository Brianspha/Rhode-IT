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

        public TabbedView ()
		{
            Title = "Rhode IT";
            tabbedViewViewModel = new TabbedViewViewModel();
            BindingContext = tabbedViewViewModel;
            Content = tabbedViewViewModel.main;
        }
        public TabbedView(TabbedViewViewModel viewModel)
        {
            Title = "Rhode IT";
            tabbedViewViewModel = viewModel;
            BindingContext = tabbedViewViewModel;
        }
    }
}