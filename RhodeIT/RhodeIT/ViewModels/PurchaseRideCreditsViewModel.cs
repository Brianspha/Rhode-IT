using Acr.UserDialogs;
using RhodeIT.Classes;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using Syncfusion.XForms.PopupLayout;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace RhodeIT.ViewModels
{
    public class PurchaseRideCreditsViewModel : INotifyPropertyChanged
    {

        public SfPopupLayout PopupLayout;
        private DataTemplate PopUpContents;
        private Entry amount;
        private LoginDetails Details;
        private RhodeITDB db;
        private RhodesDataBase RhodesDataBase;

        public event PropertyChangedEventHandler PropertyChanged;

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
                amount = new Entry();
                rideCreditsInputLayout.InputView = amount;
                rideCreditsInputLayout.ErrorText = "Field cant be left blank";
                rideCreditsInputLayout.FocusedColor = Color.Black;
                return new StackLayout { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Children = { rideCreditsInputLayout } };

            });
            PopupLayout = new SfPopupLayout();
            PopupLayout.PopupView.HeaderTitle = "Puchase Ride Credits";
            PopupLayout.PopupView.ContentTemplate = PopUpContents;
            PopupLayout.PopupView.AnimationMode = AnimationMode.Fade;
            PopupLayout.PopupView.AppearanceMode = AppearanceMode.TwoButton;
            PopupLayout.PopupView.HorizontalOptions = LayoutOptions.CenterAndExpand;
            PopupLayout.PopupView.VerticalOptions = LayoutOptions.Center;
            PopupLayout.Closed += PopupLayout_Closed;
            PopupLayout.Opened += PopupLayout_Opened;
            PopupLayout.PopupView.AcceptCommand = new Command(() => { Purchase_Ride_Credits(); });
            db = new RhodeITDB();
            RhodesDataBase = new RhodesDataBase();

        }

        public void PopupLayout_Opened(object sender, EventArgs e)
        {
            // PopupLayout.Show();
        }
        public void Purchase_Ride_Credits()
        {
            RhodeITService rhodeITServices = new RhodeITService();
            IUserDialogs dialog = UserDialogs.Instance;
            dialog.ShowLoading("Purchasing Ride Credits...");
            try
            {
                bool result = int.TryParse(amount.Text, out int rideCredit);
                if (!result)
                {
                    throw new InvalidNumberException("Invalid");
                }
                try
                {
                    if (rideCredit <= 0)
                    {
                        throw new InvalidNumberException("");
                    }
                    else
                    {
                        Details = db.GetUserDetails();
                        rhodeITServices.UpdateCreditRequestAsync(Details.Ethereum_Address, rideCredit).ConfigureAwait(false);
                        Details.RideCredits += rideCredit;
                        RhodesDataBase.ChargeUserRideCreditBalanceToAccount(Details).ConfigureAwait(false);
                        db.UpdateLoginDetails(Details);
                        dialog.HideLoading();
                        dialog.Alert(string.Format("Succesfully recharged ride credits with {0}", rideCredit), "Success", "OK");
                    }
                }
                catch (Exception e)
                {
                    dialog.HideLoading();
                    PopupLayout.Dismiss();
                    Console.WriteLine("Error whilst purcasing credits: " + e.Message);
                    dialog.Alert("Something went wrong whilst purchasing credit", "Insufficient Funds", "OK");
                }
            }
            catch (InvalidNumberException e)
            {
                dialog.HideLoading();
                PopupLayout.Dismiss();
                Console.WriteLine("Error whilst purcasing credits: " + e.Message);
                dialog.Alert("Please ensure you entered a valid number e.g. 12", "Invalid Number", "OK");
            }
        }
        private void PopupLayout_Closed(object sender, EventArgs e)
        {

        }

        private void ClickToShowPopup_Clicked(object sender, EventArgs e)
        {

        }
    }
}
