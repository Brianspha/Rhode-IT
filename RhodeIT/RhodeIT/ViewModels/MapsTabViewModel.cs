using RhodeIT.Classes;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using RhodeIT.Views;
using SlideOverKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace RhodeIT.ViewModels
{
    public class MapsTabViewModel : INotifyPropertyChanged
    {
        private RhodesMap customMap;
        private RhodesDataBase RhodesDataBase;
        private RhodeITService RhodeITServices;
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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(DockingStaions)));
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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Pins)));
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
                    OnPropertyChanged(new PropertyChangedEventArgs("MapRegionVisible"));
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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(CustomMap)));
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
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Main)));
                }
            }
        }

        public MapsTabViewModel(SlideMenuView slideMenu, Command show)
        {
            //CustomMap =(RhodesMap)mp;
            RhodeITServices = new RhodeITService();
            SlideUp = slideMenu;
            SetUpMap();
            ShowMenu = show;
            //RefereshMap();
        }
        public void IsVisible()
        {
            //  if (customMap.VisibleRegion != null) { SetUpMap(); }
        }

        private void SetUpMap()
        {
            PopulateMap();
            Main = new StackLayout
            {
                Children =
                    {
                        CustomMap
                    },
                HeightRequest = CustomMap.Height,
                WidthRequest = CustomMap.Width,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
        }


        public void CreatePins()
        {
            DockingStaions = RhodeITServices.GetDockingStations().Result;
            foreach (DockingStaion station in dockingStaions)
            {
                Pin tempPin = new Pin() { Position = new Position(station.DockingStationInformation.Latitude, station.DockingStationInformation.Longitude), Label = station.DockingStationInformation.Name, Address = "Available Bicycles: " + GetDockingStationCount(station.DockingStationInformation.Name, station.AvailableBicycles) };
                Assembly assembly = typeof(MapsTab).GetTypeInfo().Assembly;
                string file = files[0];
                string[] names = assembly.GetManifestResourceNames();
                tempPin.Icon = BitmapDescriptorFactory.FromBundle(file);
                Pins.Add(tempPin);
            }
        }
        public int GetDockingStationCount(string stationName, IList<Bicycle> bicycles)
        {
            int count = 0;
            var tempBicycles = new List<Bicycle>();
            foreach(var bicycle in bicycles)
            {
                if(bicycle.Status== "Available")
                {
                    tempBicycles.Add(bicycle);
                }
            }
            var sortedBicycles = from bicycle in tempBicycles
                                 group bicycle by bicycle.DockdeAt into sorted
                                 let tempcount = sorted.Count()
                                 orderby tempcount descending
                                 select new { Name = sorted.Key, Count = tempcount };
            foreach (var station in sortedBicycles)
            {
                if (station.Name == stationName)
                {
                    count = station.Count;
                    break;
                }
            }
            return count;

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

        private void PopulateMap()
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
                CustomMap.Pins.Add(p);
            }
            Console.WriteLine("Pins added: ", customMap.Pins.Count);
            CustomMap.PinClicked += CustomMap_PinClicked;
            CustomMap.MoveToRegion(MapSpan.FromCenterAndRadius(pins[0].Position, Distance.FromMiles(.8)));
            CustomMap.Cluster();
        }
        /// <summary>
        /// Refreshes map 
        /// </summary>
        public void RefereshMap()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Device.BeginInvokeOnMainThread(() => PopulateMap());
                return true;
            });

        }

        /// <summary>
        /// On property changed Notifies UI on data changes.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
