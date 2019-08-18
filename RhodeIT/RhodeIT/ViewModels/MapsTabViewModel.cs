using RhodeIT.Classes;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using RhodeIT.Views;
using SlideOverKit;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace RhodeIT.ViewModels
{
    public class MapsTabViewModel : INotifyPropertyChanged
    {
        private RhodesMap customMap;
        private readonly RhodesDataBase RhodesDataBase;
        private RhodeITService RhodeITServices;
        private readonly RhodeITDB platformdb;
        private bool visibleYet;
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Position startPos = new Position(-33.311836d, 26.520642d);
        private StackLayout main;
        private SlideMenuView SlideUp;
        private readonly Command ShowMenu;
        private readonly string[] files = new string[] { "Icon1.png" };

        private ObservableCollection<DockingStaion> dockingStaions;
        public ObservableCollection<DockingStaion> DockingStaions
        {
            get => dockingStaions;
            set
            {
                if (value != dockingStaions)
                {
                    dockingStaions = value;
                    OnPropertyChanged(nameof(DockingStaions));
                }
            }
        }
        private ObservableCollection<Pin> pins;
        public ObservableCollection<Pin> Pins
        {
            get => pins;
            set
            {
                if (value != pins)
                {
                    pins = value;
                    OnPropertyChanged(nameof(Pins));
                }
            }
        }
        public bool MapRegionVisible
        {
            get => visibleYet;
            set
            {
                if (null != customMap.Region)
                {
                    visibleYet = value;
                    OnPropertyChanged("MapRegionVisible");
                    SetUpMap();
                }
            }
        }

        public RhodesMap CustomMap
        {
            get => customMap;
            set
            {
                if (customMap != value)
                {
                    customMap = value;
                    OnPropertyChanged(nameof(CustomMap));
                }
            }

        }
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



        public MapsTabViewModel(SlideMenuView slideMenu, Command show)
        {
            //CustomMap =(RhodesMap)mp;
            RhodesDataBase = new RhodesDataBase();
            RhodeITServices = new RhodeITService();
            SlideUp = slideMenu;
            SetUpMap();
            ShowMenu = show;
        }
        public void IsVisible()
        {
            //  if (customMap.VisibleRegion != null) { SetUpMap(); }
        }

        private void SetUpMap()
        {
            Pins = new ObservableCollection<Pin>();
            CreatePins();
            CustomMap = new RhodesMap
            {
                MapType = MapType.Street,
                IsEnabled = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            if (pins.Count == 0)
            {
                pins.Add(new Pin { Position = new Position(-33.311836, 26.520642), Label = "Rhodes University" });
            }
            foreach (Pin p in pins)
            {
                customMap.Pins.Add(p);
            }
            Console.WriteLine("Pins added: ", customMap.Pins.Count);
            customMap.PinClicked += CustomMap_PinClicked;
            Main = new StackLayout
            {
                Children =
                    {
                        customMap
                    },
                HeightRequest = customMap.Height,
                WidthRequest = customMap.Width,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(pins[0].Position, Distance.FromMiles(.8)));
            customMap.Cluster();
        }
        public void CreatePins()
        {
            DockingStaions = RhodeITServices.GetDockingStations().Result;
            foreach (DockingStaion station in dockingStaions)
            {
                Pin tempPin = new Pin() { Position = new Position(station.DockingStationInformation.Latitude, station.DockingStationInformation.Longitude), Label = station.DockingStationInformation.Name, Address = "Available Bicycles: " + station.AvailableBicycles.Count };
                Assembly assembly = typeof(MapsTab).GetTypeInfo().Assembly;
                string file = files[0];
                string[] names = assembly.GetManifestResourceNames();
                tempPin.Icon = BitmapDescriptorFactory.FromBundle(file);
                Pins.Add(tempPin);
            }
        }
        private void CustomMap_PinClicked(object sender, PinClickedEventArgs e)
        {
            //@dev get pin details
            if (Main.Children.Count > 1)//@dev simulate a close button on the popup listview
            {
                for (int i = Main.Children.Count - 1; i >= 1; i--)
                {
                    Main.Children.RemoveAt(i);
                }
            }
            SlideUp = new AvailableBicycles(e.Pin.Label, ShowMenu, Main);
            Main.Children.Add(SlideUp);
        }


        /// <summary>
        /// On the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
