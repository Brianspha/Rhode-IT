using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rhode_IT.Views
{
	public class AvailableBicyclesTab : ContentPage
	{
        /// <summary>
        /// Recieves information regarding which bicycle docking station to connect to i.e. IP address
        /// </summary>
        /// <param name="text">IP address of the docking station</param>
		public AvailableBicyclesTab (string text)
		{
			Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}