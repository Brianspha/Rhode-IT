using Rhode_IT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rhode_IT.Views
{
	public class HistoryTab : ContentPage
	{
        HistroyTabViewModel viewModel;
		public HistoryTab ()
		{
			Content = new StackLayout {
				Children = {
					new Label { Text = "To do!" }
				},
                HorizontalOptions=LayoutOptions.CenterAndExpand,
                VerticalOptions=LayoutOptions.CenterAndExpand
			};
		}
	}
}