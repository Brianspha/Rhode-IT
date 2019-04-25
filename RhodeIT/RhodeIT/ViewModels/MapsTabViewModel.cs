using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using RhodeIT.Databases;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;
using RhodeIT.Helpers;
using RhodeIT.Classes;
using Xamarin.Forms.GoogleMaps.Clustering;
using RhodeIT.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SlideOverKit;
using RhodeIT.Views;

namespace RhodeIT.ViewModels
{
    public class MapsTabViewModel : INotifyPropertyChanged
        {
            private RhodesMap customMap;
            LocalDataBase db;
            bool visibleYet;
            public event PropertyChangedEventHandler PropertyChanged;
            Position startPos = new Position(-33.311836d, 26.520642d);
            StackLayout main;
            SlideMenuView SlideUp;
            Action CloseMenu;
            Action ShowMenue;
             public bool MapRegionVisible
            {
                get { return visibleYet; }
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
            get
            {
                return customMap;
            }
            set
            {
                if(customMap != value)
                {
                    customMap = value;
                    OnPropertyChanged(nameof(CustomMap));
                }
            }

            }
        public StackLayout Main
        {
            get
            {
                return main;
            }
            set
            {
                if(value != main)
                {
                    main = value;
                    OnPropertyChanged(nameof(Main));
                }
            }
        }



        public MapsTabViewModel(SlideMenuView slideMenu, Action close, Action show)
            {
                //CustomMap =(RhodesMap)mp;
                db = new LocalDataBase();
                SlideUp = slideMenu;
                SetUpMap();
                CloseMenu = close;
                ShowMenue = show;
        }
            public void IsVisible()
            {
              //  if (customMap.VisibleRegion != null) { SetUpMap(); }
            }
            void SetUpMap()
            {
                var dockingStation =new string[] { "Hamilton Building", "Journalism Department", "Ellen Khuzwayo Female Residence,", "Education Department" , "Union Steve Biko Building", "Joe Slovo Male Residence" };
                db.addDockingStation(dockingStation);//@dev in future i will check the value returned
                var pins = db.getAllDockingStations();
                CustomMap = new RhodesMap
                {
                    MapType = MapType.Street,
                    IsEnabled = true,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                foreach (Pin p in pins)
                {
                    customMap.Pins.Add(p);
                }
                Console.WriteLine("Pins added: ",customMap.Pins.Count);
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
                customMap.Cluster();
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(pins[0].Position, Distance.FromMiles(.8)));
        }

        private void CustomMap_PinClicked(object sender, PinClickedEventArgs e)
        {
            //@dev get pin details
            SlideUp = new AvailableBicycles(e.Pin.Label,CloseMenu);
        }


        /// <summary>
        /// On the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
