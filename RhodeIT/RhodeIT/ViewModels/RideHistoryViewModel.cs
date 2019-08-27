using RhodeIT.Models;
using Syncfusion.ListView.XForms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace RhodeIT.ViewModels
{
    public class RideHistoryViewModel
    {
        private MaterialCard myPastRides;
        public MaterialCard MyPastRides
        {
            get => myPastRides;
            private set
            {
                if (myPastRides != value)
                {
                    myPastRides = value;
                    OnPropertyChanged(nameof(MyPastRides));
                }
            }
        }

        private ObservableCollection<Ride> rideHistory;
        public ObservableCollection<Ride> RideHistory
        {
            get => rideHistory;
            private set
            {
                if (value != rideHistory)
                {
                    rideHistory = value;
                    OnPropertyChanged(nameof(RideHistory));
                }
            }
        }
        private event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Constructor
        /// </summary>
        public RideHistoryViewModel()
        {
            SetUp();
        }
        /// <summary>
        /// Sets up the UI elements to be rendered by the RideHistory View
        /// </summary>
        private void SetUp()
        {
            StackLayout rides = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
            MyPastRides = new MaterialCard
            {
                BackgroundColor = Color.White,
                Elevation = 10,
                IsClickable = true,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            SfListView pastRides = new SfListView
            {
                ItemSize = 350,
                ItemsSource = new RidesViewModel().Rides,
                LayoutManager = new LinearLayout(),
                ItemTemplate = InitialiseListTemplates()
            };
            MaterialCard historyRidesCardLabel = new MaterialCard { BackgroundColor = Color.White, Elevation = 10, IsClickable = true, HeightRequest = 100, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            Label historyRidesLabel = new Label { Text = "Past Rides", BackgroundColor = Color.White, FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            historyRidesCardLabel.Content = new StackLayout { Children = { historyRidesLabel }, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
            rides.Children.Add(historyRidesCardLabel);
            rides.Children.Add(pastRides);
            MyPastRides.Content = rides;
        }

        /// <summary>
        /// Initialises the Listview item templates and sets the appropiate 
        /// Properties to be binded to each UI  Element
        /// </summary>
        /// <returns>DataTemplate</returns>
        private static DataTemplate InitialiseListTemplates()
        {
            return new DataTemplate(() =>
            {
                Label stationName = new Label { BackgroundColor = Color.Transparent, FontSize = 15 };
                Label stationNameLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "Station" };
                stationName.SetBinding(Label.TextProperty, new Binding("StationName"));
                Label bikeID = new Label { BackgroundColor = Color.Transparent, FontSize = 15, Text = "BikeID" };
                Label bikeIDLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15 };
                bikeID.SetBinding(Label.TextProperty, new Binding("BikeID"));
                Label duration = new Label { BackgroundColor = Color.Transparent, FontSize = 15 };
                Label durationLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "Duration" };
                duration.SetBinding(Label.TextProperty, new Binding("Duration"));
                Label docked = new Label { BackgroundColor = Color.Transparent, FontSize = 15 };
                Label dockedLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "Docked" };
                docked.SetBinding(Label.TextProperty, new Binding("Docked"));
                Label transactionReciept = new Label { BackgroundColor = Color.Transparent, FontSize = 15 };
                Label transactionRecieptLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "TransactionReceipt" };
                transactionReciept.SetBinding(Label.TextProperty, new Binding("TransactionReciept"));

                StackLayout bikeIDParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 30,
                    Children = {
                            bikeIDLabel,bikeID
                        }
                };
                StackLayout stationNameParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 30,
                    Children = {
                            stationNameLabel,stationName
                        }
                };
                StackLayout durationParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 30,
                    Children = {
                            durationLabel,duration
                        }
                };
                StackLayout dockedParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 30,
                    Children = {
                            dockedLabel,docked,
                        }
                };
                StackLayout transactionHashParent = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand, Children = { transactionReciept } };
                StackLayout transactionRecieptParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Children = {
                            transactionRecieptLabel
                        }
                };
                StackLayout parentInfo = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children ={
                        bikeIDParent,
                        stationNameParent,
                        durationParent,
                        dockedParent,
                        transactionRecieptParent,
                        transactionHashParent
                    }
                };
                MaterialCard parentInforCard = new MaterialCard
                {
                    Elevation = 5,
                    BackgroundColor = Color.White
                };
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
        /// Invoked when a Property is assingned a new value
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
