using ImageCircle.Forms.Plugin.Abstractions;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Buttons;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace RhodeIT.ViewModels
{
    public class UserProfileViewModel
    {


        private SfListView myRides;

        private SfListView MyRides
        {
            get => myRides;
            set
            {
                if (value != myRides)
                {
                    myRides = value;
                    OnPropertyChanged(nameof(myRides));
                }
            }
        }

        private CircleImage userImage;
        private StackLayout main;
        public StackLayout Main
        {
            get => main;
            set
            {
                if (value != main)
                {
                    main = value;
                    OnPropertyChanged(nameof(Main));
                }
            }
        }

        private RidesViewModel RidesViewModel;

        public UserProfileViewModel()
        {
            SetUp();
        }

        private void SetUp()
        {
            Main = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            StackLayout imageTopParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            userImage = new CircleImage
            {
                HeightRequest = 250,
                WidthRequest = 250,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                Source = ImageSource.FromUri(new Uri("http://oi68.tinypic.com/25s8nf5.jpg")),
                Opacity = 100
            };

            StackLayout userDetailsLabelParent = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            MaterialCard userInfoParent = new MaterialCard
            {
                Elevation = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsClickable = true
            };


            //@dev student element
            Label studentNumberLabel = new Label
            {
                Text = "Student No",
                FontSize = 16,
                TextColor = Color.Black
            };
            MaterialCard studentNumberCard = new MaterialCard
            {
                BackgroundColor = Color.Transparent,
                Elevation = 10,
            };
            Label studentNumber = new Label
            {
                Text = "g14m1190",
                TextColor = Color.Black
            };
            StackLayout studentNumberParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            studentNumberParent.Children.Add(studentNumberLabel);
            studentNumberParent.Children.Add(studentNumber);
            studentNumberCard.Content = studentNumberParent;

            //@dev ridecredits elements
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
            };
            StackLayout rideCreditsParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            Label rideCredits = new Label
            {
                Text = "20",
                TextColor = Color.Black
            };
            rideCreditsParent.Children.Add(rideCreditsLabel);
            rideCreditsParent.Children.Add(rideCredits);
            rideCreditsCard.Content = rideCreditsParent;


            userDetailsLabelParent.Children.Add(studentNumberCard);
            userDetailsLabelParent.Children.Add(rideCreditsCard);
            imageTopParent.Children.Add(userDetailsLabelParent);
            userInfoParent.Content = imageTopParent;

            RidesViewModel = new RidesViewModel();
            SfListView upComingrides = new SfListView
            {
                ItemSize = 350,
                Orientation = Orientation.Horizontal,
                ItemsSource = RidesViewModel.Rides,
                LayoutManager = new GridLayout() { SpanCount = 2 },
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
                    SfButton receipt = new SfButton();
                    receipt.ImageSource = "https://cdn0.iconfinder.com/data/icons/social-messaging-ui-color-shapes/128/Info-circle-blue-512.png";
                    receipt.ShowIcon = true;
                    receipt.BackgroundColor = Color.Transparent;
                    receipt.CornerRadius = new Thickness(360);
                    StackLayout bikeIDParent = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Padding=0,
                        HeightRequest=30,
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
                            receipt
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
                            receipt
                        }
                    };
                    StackLayout parentInfo = new StackLayout
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Padding=0
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
                        HeightRequest=80,
                        CornerRadius = 5,
                        BackgroundColor = Color.WhiteSmoke,
                        Padding = 3,
                        Content = parentInforCard
                    };

                    return cardFrame;
                })
            };
            SfListView pastRides = new SfListView
            {
                ItemSize = 350,
                Orientation = Orientation.Horizontal,
                ItemsSource = RidesViewModel.Rides,
                LayoutManager = new GridLayout() { SpanCount = 2 },
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
                    SfButton receipt = new SfButton();
                    receipt.ImageSource = "https://cdn0.iconfinder.com/data/icons/social-messaging-ui-color-shapes/128/Info-circle-blue-512.png";
                    receipt.ShowIcon = true;
                    receipt.BackgroundColor = Color.Transparent;
                    receipt.CornerRadius = new Thickness(360);
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
                            receipt
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
                            receipt
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
            MaterialCard pastCardLabel = new MaterialCard { BackgroundColor = Color.White, Elevation = 10, IsClickable = true, HeightRequest = 100, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

            Label upComingLabel = new Label { Text = "Upcoming Rides", BackgroundColor = Color.White, FontSize = 18,FontAttributes=FontAttributes.Bold, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            Label ridesLabel = new Label { Text = "My Rides", BackgroundColor = Color.White, FontSize = 18, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            upcomingRidesCardLabel.Content = new StackLayout { Children = { upComingLabel },VerticalOptions=LayoutOptions.CenterAndExpand,HorizontalOptions=LayoutOptions.CenterAndExpand };
            pastCardLabel.Content = new StackLayout { Children = { ridesLabel }, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand };
            Main.Children.Add(userInfoParent);
            Main.Children.Add(upcomingRidesCardLabel);
            Main.Children.Add(upComingrides);
            Main.Children.Add(pastCardLabel);
            Main.Children.Add(pastRides);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
