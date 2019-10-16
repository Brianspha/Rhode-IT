using Acr.UserDialogs;
using ImageCircle.Forms.Plugin.Abstractions;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Buttons;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace RhodeIT.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        public LoginDetails details;
        private SfListView myRides;
        public event PropertyChangedEventHandler PropertyChanged;

        private SfListView MyRides
        {
            get => myRides;
            set
            {
                if (value != myRides)
                {
                    myRides = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(MyRides)));
                }
            }
        }

        private PurchaseRideCreditsViewModel PurchaseRideCreditsViewModel;
        private readonly CircleImage userImage;
        private RhodeITService SmartContract;
        private StackLayout main;
        public StackLayout Main
        {
            get => main;
            set
            {
                if (value != main)
                {
                    main = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Main)));
                }
            }
        }

        private RidesViewModel RidesViewModel;

        /// <summary>
        /// Constructor
        /// </summary>
        public UserProfileViewModel()
        {
            SetUp();
        }
        /// <summary>
        /// Setsup the User Profile View to be used by the UserProfile View Page
        /// Each part of the UI is created by several methods
        /// CreateStudentNumberElements- Which creates all UI elements that show user details like student number
        /// CreateUserInformationUIElements- Parents the user details created by the CreateStudentNumberElements method
        /// InitialiseUpComingRidesList Initialises the list of rides the user has
        /// </summary>
        private void SetUp()
        {
            RhodeITDB db = new RhodeITDB();
            SmartContract = new RhodeITService();
            details = new LoginDetails();
            details = db.GetUserDetails();
            PurchaseRideCreditsViewModel = new PurchaseRideCreditsViewModel();
            Main = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            CreateStudentNumberElements(out MaterialCard studentNumberCard, out StackLayout studentNumberParent, out StackLayout topUpParent);
            CreateUserInformationUIElements(out MaterialCard userInfoParent, studentNumberCard, topUpParent);
            RidesViewModel = new RidesViewModel();
            MyRides = InitialiseUpComingRidesList();
            MaterialCard upcomingRidesCardLabel = new MaterialCard { BackgroundColor = Color.White, Elevation = 5, IsClickable = true, HeightRequest = 100, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            Label upComingLabel = new Label { Text = "Upcoming Rides", BackgroundColor = Color.White, TextColor = Color.Black, FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            upcomingRidesCardLabel.Content = new StackLayout { Children = { upComingLabel }, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
            Main.Children.Add(userInfoParent);
            Main.Children.Add(upcomingRidesCardLabel);
            Main.Children.Add(MyRides);
        }

        private void MyRides_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Ride rental = e.ItemData as Ride;
            RhodeITDB db = new RhodeITDB();
            LoginDetails details = db.GetUserDetails();
            DockBicycleAsync(new Bicycle { ID = rental.ID, renter = details.Ethereum_Address, DockdeAt = rental.StationName }).Wait();

        }

        private async Task DockBicycleAsync(Bicycle bicycle)
        {
            await SmartContract.DockBicycle(bicycle).ConfigureAwait(false);

        }

        /// <summary>
        /// Fetches all user upcoming rides from the RhodeIT Smartcontract and populates the listview
        /// </summary>
        /// <returns></returns>
        private SfListView InitialiseUpComingRidesList()
        {
            SfListView temp = new SfListView
            {
                ItemSize = 350,
                Orientation = Orientation.Horizontal,
                ItemsSource = RidesViewModel.Rides,
                LayoutManager = new LinearLayout(),
                ItemTemplate = InitialiseUpcomingUserRidesDataTemplate()
            };
            temp.ItemHolding += MyRides_ItemTapped;
            temp.ItemTapped += MyRides_ItemTapped;
            return temp;
        }

        private void MyRides_ItemTapped(object sender, ItemHoldingEventArgs e)
        {
            RemoveRideFromList(e).Wait();
        }

        private async Task<bool> RemoveRideFromList(ItemHoldingEventArgs e)
        {
            var dialog = UserDialogs.Instance;
            dialog.ShowLoading("Docking Bicycle...");
            Ride rental = e.ItemData as Ride;
            RhodeITDB db = new RhodeITDB();
            LoginDetails details = db.GetUserDetails();
            await DockBicycleAsync(new Bicycle { ID = rental.ID, renter = details.Ethereum_Address, DockdeAt = rental.StationName }).ConfigureAwait(false);
            db.RemoveRideFromList(rental);
            MyRides = InitialiseUpComingRidesList();
            dialog.HideLoading();
            return true;
        }

        /// <summary>
        /// Initialises the DataTemplate to be used to populate the Listview
        /// which renders all upcoming user rides
        /// </summary>
        /// <returns>DataTemplate</returns>
        private static DataTemplate InitialiseUpcomingUserRidesDataTemplate()
        {
            return new DataTemplate(() =>
            {
                Label stationName = new Label { BackgroundColor = Color.Transparent, FontSize = 15, TextColor = Color.Black };
                Label stationNameLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "Station", TextColor = Color.Black };
                stationName.SetBinding(Label.TextProperty, new Binding("StationName"));
                Label bikeID = new Label { BackgroundColor = Color.Transparent, FontSize = 15, Text = "BikeID", TextColor = Color.Black };
                Label bikeIDLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, TextColor = Color.Black };
                bikeID.SetBinding(Label.TextProperty, new Binding("ID"));
                Label docked = new Label { BackgroundColor = Color.Transparent, FontSize = 15, TextColor = Color.Black };
                Label dockedLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "Docked", TextColor = Color.Black };
                docked.SetBinding(Label.TextProperty, new Binding("Docked"));
                SfButton details = new SfButton
                {
                    Text = "Cancel",
                    BackgroundColor = Color.White,
                    TextColor = Color.Black,
                    CornerRadius = 20,
                    BorderColor = Color.Black,
                    BorderWidth = 1
                };
                StackLayout bikeIDParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            bikeIDLabel,bikeID
                        }
                };
                StackLayout stationNameParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            stationNameLabel,stationName
                        }
                };
                StackLayout dockedParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            dockedLabel,docked,
                        }
                };
                StackLayout transactionRecieptParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                            details
                        }
                };
                StackLayout parentInfo = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                };
                MaterialCard parentInforCard = new MaterialCard
                {
                    Elevation = 5,
                    BackgroundColor = Color.White,
                    IsClickable = true
                };
                parentInfo.Children.Add(bikeIDParent);
                parentInfo.Children.Add(stationNameParent);
                parentInfo.Children.Add(dockedParent);
                parentInfo.Children.Add(transactionRecieptParent);
                parentInforCard.Content = parentInfo;
                Frame cardFrame = new Frame
                {
                    HeightRequest = 80,
                    CornerRadius = 5,
                    BackgroundColor = Color.WhiteSmoke,
                    Padding = 3,
                    Content = parentInforCard
                };
                return cardFrame;
            });
        }

        /// <summary>
        /// Parents user related UI elements which indicate who the user is and how many ride credits they have
        /// </summary>
        /// <param name="userDetailsParent">The parent of the User details which is placed at the top of the Screen</param>
        /// <param name="userDetailsLabelParent"> The parent layout of the specific user details such as student number and ride credit balance</param>
        /// <param name="userInfoParent"></param>
        /// <param name="studentNumberCard"> The material card which renders the students student number</param>
        /// <param name="topUpParent">The material card which is used to render the option of purchasing new ride credits</param>
        private static void CreateUserInformationUIElements(out MaterialCard userInfoParent, MaterialCard studentNumberCard, StackLayout topUpParent)
        {

            StackLayout userDetailsParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            StackLayout userDetailsLabelParent = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            userInfoParent = new MaterialCard
            {
                Elevation = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsClickable = true
            };
            Label rideCreditsLabel = new Label
            {
                Text = "Ride Credits",
                FontSize = 16,
                TextColor = Color.Black
            };
            MaterialCard rideCreditsCard = new MaterialCard
            {
                BackgroundColor = Color.Transparent,
                Elevation = 10,
                IsClickable = true
            };
            StackLayout rideCreditsParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            LoginDetails details = new RhodeITDB().GetUserDetails();
            string balance = GetUserCreditBalanceAsync(details).Result;
            Label rideCredits = new Label
            {
                Text = balance,
                TextColor = Color.Black
            };
            rideCreditsParent.Children.Add(rideCreditsLabel);
            rideCreditsParent.Children.Add(rideCredits);
            rideCreditsCard.Content = rideCreditsParent;
            userDetailsLabelParent.Children.Add(studentNumberCard);
            userDetailsLabelParent.Children.Add(rideCreditsCard);
            userDetailsParent.Children.Add(userDetailsLabelParent);
            userDetailsParent.Children.Add(topUpParent);
            userInfoParent.Content = userDetailsParent;

        }
        /// <summary>
        /// Creates the UI elements that will be used to render the users details such as student number and ride credits
        /// </summary>
        /// <param name="studentNumberCard"> The parent material card that renders a students details</param>
        /// <param name="studentNumberParent">The parent of the studentNumber card</param>
        /// <param name="topUpParent">The Parent of the button which allows the user to top they ride credits</param>
        private void CreateStudentNumberElements(out MaterialCard studentNumberCard, out StackLayout studentNumberParent, out StackLayout topUpParent)
        {
            Label studentNumberLabel = new Label
            {
                Text = "Student No",
                FontSize = 16,
                TextColor = Color.Black
            };
            studentNumberCard = new MaterialCard
            {
                BackgroundColor = Color.Transparent,
                Elevation = 10,
            };
            Label studentNumber = new Label
            {
                Text = details.User_ID,
                TextColor = Color.Black
            };
            SfButton TopUp = new SfButton
            {
                Text = "Purchase Ride Credits",
                CornerRadius = 20,
                WidthRequest = 100,
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                BorderColor = Color.Black,
                BorderWidth = 1

            };
            TopUp.Clicked += TopUp_Clicked;
            studentNumberParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            studentNumberParent.Children.Add(studentNumberLabel);
            studentNumberParent.Children.Add(studentNumber);
            topUpParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children ={
                    TopUp
                    }
            };
            studentNumberCard.Content = studentNumberParent;
        }

        /// <summary>
        /// Invoked when a user clicks on the topup button 
        /// used to purchase Ride credits which can inturn used for 
        /// renting out bicycles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopUp_Clicked(object sender, EventArgs e)
        {
            PurchaseRideCreditsViewModel.PopupLayout.Show();
        }
        /// <summary>
        /// Fetches a user ride credit from the smartcontract storage
        /// </summary>
        /// <param name="details">Users login details</param>
        /// <returns>string</returns>
        private static async Task<string> GetUserCreditBalanceAsync(LoginDetails details)
        {
            string balance = await new RhodeITService().GetUsercreditQueryAsync(details.Ethereum_Address).ConfigureAwait(false);
            return balance;
        }

        /// <summary>
        /// Invloked when any property is assigned a new value.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            PropertyChanged?.Invoke(this, e);
        }


    }
}
