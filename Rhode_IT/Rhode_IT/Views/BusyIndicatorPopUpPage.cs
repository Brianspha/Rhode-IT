using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Rhode_IT.Views
{
	public class BusyIndicatorPopUpPage : PopupPage
    {
		SfPopupLayout popupLayout;
        DataTemplate templateView;
        public BusyIndicatorPopUpPage()
        {
            popupLayout = new SfPopupLayout();
            templateView = new DataTemplate(() =>
            {
                SfBusyIndicator busyIndicator = new SfBusyIndicator();
                busyIndicator.AnimationType = AnimationTypes.Ball;
                busyIndicator.ViewBoxWidth = 150;
                busyIndicator.ViewBoxHeight = 150;
                busyIndicator.TextColor = Color.SkyBlue;
                return busyIndicator;
            });
            popupLayout.PopupView.ContentTemplate = templateView;
            popupLayout.Show();

        }
        private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        {
            popupLayout.Show();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }
        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}