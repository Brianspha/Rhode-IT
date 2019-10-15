using RhodeIT.Models;
using Syncfusion.ListView.XForms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using XamEffects;
using XF.Material.Forms.UI;

namespace RhodeIT.ViewModels
{
    public class RideHistoryViewModel : INotifyPropertyChanged
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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(MyPastRides)));
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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(RideHistory)));
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

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                PropertyChanged = value;
            }

            remove
            {
                PropertyChanged = null;
            }
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
                ItemsSource = new TransactionHistoryViewModel().Receipts,
                LayoutManager = new LinearLayout(),
                ItemTemplate = InitialiseListTemplates()
            };
            MaterialCard historyRidesCardLabel = new MaterialCard { BackgroundColor = Color.White, Elevation = 4, IsClickable = true, HeightRequest = 100, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            Label historyRidesLabel = new Label { Text = "Past Rides", BackgroundColor = Color.White, FontSize = 18, TextColor=Color.Black,FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
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
                Label receipt = new Label { BackgroundColor = Color.Transparent, FontSize = 15, TextColor = Color.Black };
                Label receiptLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "Receipt", TextColor = Color.Black };
                receiptLabel.SetBinding(Label.TextProperty, new Binding("Receipt"));
                Label activity = new Label { BackgroundColor = Color.Transparent, FontSize = 15, Text = "Activity", TextColor = Color.Black };
                Label activityLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15,TextColor=Color.Black };
                activity.SetBinding(Label.TextProperty, new Binding("Activity"));
                  StackLayout activityParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 30,
                    Children = {
                            activityLabel,activity
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
                            receiptLabel,receipt
                        }
                };
                StackLayout parentInfo = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children ={
                        activityParent,
                        stationNameParent,
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
        /// Invoked when a property is assigned a new value
        /// </summary>
        /// <param name="e"> property thats changed</param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
