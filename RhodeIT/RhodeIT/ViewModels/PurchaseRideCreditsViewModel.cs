using Syncfusion.XForms.PopupLayout;
using Syncfusion.XForms.TextInputLayout;
using System;
using Xamarin.Forms;

namespace RhodeIT.ViewModels
{
    public class PurchaseRideCreditsViewModel
    {

        public SfPopupLayout PopupLayout;
        private DataTemplate PopUpContents;

        public PurchaseRideCreditsViewModel()
        {
            SetUp();
        }

        private void SetUp()
        {

            PopUpContents = new DataTemplate(() =>
            {

                SfTextInputLayout rideCreditsInputLayout = new SfTextInputLayout
                {
                    Hint = "Amount",
                    FocusedColor = Color.Black,
                    UnfocusedColor = Color.Black,
                    ContainerType = ContainerType.Outlined,
                    OutlineCornerRadius = 8
                };
                Entry amount = new Entry();
                rideCreditsInputLayout.InputView = amount;
                rideCreditsInputLayout.ErrorText = "Field cant be left blank";
                rideCreditsInputLayout.FocusedColor = Color.Black;
                return new StackLayout { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Children = { rideCreditsInputLayout } };

            });
            PopupLayout = new SfPopupLayout();
            PopupLayout.PopupView.ContentTemplate = PopUpContents;
            PopupLayout.PopupView.AnimationMode = AnimationMode.Fade;
            PopupLayout.PopupView.AppearanceMode = AppearanceMode.TwoButton;
            PopupLayout.PopupView.HorizontalOptions = LayoutOptions.CenterAndExpand;
            PopupLayout.PopupView.VerticalOptions = LayoutOptions.Center;
            PopupLayout.Closed += PopupLayout_Closed;
            PopupLayout.Opened += PopupLayout_Opened;
        }

        public void PopupLayout_Opened(object sender, EventArgs e)
        {
            // PopupLayout.Show();
        }

        private void PopupLayout_Closed(object sender, EventArgs e)
        {

        }

        private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        {

        }
    }
}
