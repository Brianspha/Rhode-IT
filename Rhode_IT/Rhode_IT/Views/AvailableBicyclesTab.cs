using Rhode_IT.Databases;
using Rhode_IT.Models;
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

        MainDataBase db;
        IPConfig current;
		public AvailableBicyclesTab ()
		{
            db = new MainDataBase();
            current = db.getCurrentDockingStationIP();
			Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}