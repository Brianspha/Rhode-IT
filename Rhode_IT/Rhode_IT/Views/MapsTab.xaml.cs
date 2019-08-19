using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Prism;
using Rhode_IT.ViewModels;

namespace Rhode_IT.Views
{
    public partial class MapsTab : ContentPage
    {
        MapsTabViewModel viewModel;
        public MapsTab()
        {
            viewModel = new MapsTabViewModel();
            Content = viewModel.Main;
        }

    }
}