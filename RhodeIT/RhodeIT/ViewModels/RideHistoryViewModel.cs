using RhodeIT.Models;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Buttons;
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

        public RideHistoryViewModel()
        {
            SetUp();
        }

        private void SetUp()
        {
            MaterialCard rides = new MaterialCard { Elevation = 10, IsClickable = true, BackgroundColor = Color.White };
            SfListView pastRides = new SfListView
            {
                ItemSize = 350,
                ItemsSource = new RidesViewModel().Rides,
                LayoutManager = new LinearLayout(),
                ItemTemplate = new DataTemplate(() =>
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
                    StackLayout transactionRecieptParent = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Padding = 0,
                        HeightRequest = 30,
                        Children = {
                            transactionRecieptLabel,transactionReciept
                        }
                    };
                    StackLayout parentInfo = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding = 0
                    };
                    MaterialCard parentInforCard = new MaterialCard
                    {
                        Elevation = 5,
                        BackgroundColor = Color.White
                    };
                    parentInfo.Children.Add(bikeIDParent);
                    parentInfo.Children.Add(stationNameParent);
                    parentInfo.Children.Add(durationParent);
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
                })
            };
            MaterialCard upcomingRidesCardLabel = new MaterialCard { BackgroundColor = Color.White, Elevation = 10, IsClickable = true, HeightRequest = 100, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

            Label upComingLabel = new Label { Text = "Past Rides", BackgroundColor = Color.White, FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            upcomingRidesCardLabel.Content = new StackLayout { Children = { upComingLabel }, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
            MyPastRides = new MaterialCard();
            rides.Content = pastRides;
            MyPastRides.Content = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, Children = { upcomingRidesCardLabel, rides } };
        }
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
