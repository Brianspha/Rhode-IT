using ImageCircle.Forms.Plugin.Abstractions;
using Syncfusion.ListView.XForms;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace RhodeIT.ViewModels
{
    public class UserProfileViewModel
    {
        SfListView myRides;
        SfListView MyRides
        {
            get
            {
                return myRides;
            }
            set
            {
                if(value != myRides)
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
            Label studentNumberLabel = new Label
            {
                Text = "Student No",
                FontSize = 16,
                TextColor = Color.Black
            };

            Label rideCreditsLabel = new Label
            {
                Text = "Ride Credits",
                FontSize = 16,
                TextColor = Color.Black
            };
            MaterialCard userInfoParent = new MaterialCard
            {
                Elevation = 1,
                HeightRequest = 350,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsClickable = true
            };
            MaterialCard rideCreditsCard = new MaterialCard
            {
                BackgroundColor = Color.Transparent,
                Elevation = 4,
            };
            MaterialCard studentNumberCard = new MaterialCard
            {
                BackgroundColor = Color.Transparent,
                Elevation = 4,
            };
            StackLayout rideCreditsParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            StackLayout studentNumberParent = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            Label rideCredits = new Label
            {
                Text = "20",
                TextColor = Color.Black
            };
            Label studentNumber = new Label
            {
                Text = "g14m1190",
                TextColor = Color.Black
            };

            Label myRidesLabel = new Label
            {
                Text="My Rides"
            };

            MaterialCard myRidesCard = new MaterialCard
            {
                BackgroundColor = Color.Transparent,
                Elevation = 4,
            };
            rideCreditsParent.Children.Add(rideCreditsLabel);
            rideCreditsParent.Children.Add(rideCredits);
            rideCreditsCard.Content = rideCreditsParent;

            studentNumberParent.Children.Add(studentNumberLabel);
            studentNumberParent.Children.Add(studentNumber);
            studentNumberCard.Content = studentNumberParent;

            userDetailsLabelParent.Children.Add(studentNumberCard);
            userDetailsLabelParent.Children.Add(rideCreditsCard);
            imageTopParent.Children.Add(userImage);
            imageTopParent.Children.Add(userDetailsLabelParent);
            userInfoParent.Content = imageTopParent;




            Main.Children.Add(userInfoParent);

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
