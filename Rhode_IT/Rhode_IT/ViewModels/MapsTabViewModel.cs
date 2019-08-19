using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using Rhode_IT.Databases;
using Prism.Navigation;
using Xamarin.Forms.Maps;
using Rhode_IT.Helpers;
using Rhode_IT.Classes;

namespace Rhode_IT.ViewModels
{
    public class MapsTabViewModel : INotifyPropertyChanged
        {
            private RhodesMap customMap;
            MainDataBase db;
            bool visibleYet { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
            Position startPos = new Xamarin.Forms.Maps.Position(-33.311836d, 26.520642d);
            StackLayout main;
            public bool MapRegionVisible
            {
                get { return visibleYet; }
                set
                {
                    if (null != customMap.VisibleRegion)
                    {
                        visibleYet = true;
                        this.OnPropertyChanged("MapRegionVisible");
                        SetUpMap();
                        moveToRegion();
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


        public MapsTabViewModel()
            {
                db = new MainDataBase();
                SetUpMap();
                moveToRegion();
        }
            public void IsVisible()
            {
              //  if (customMap.VisibleRegion != null) { SetUpMap(); }
            }
            void SetUpMap()
            {
                var dockingStation =new string[] { "Hamilton Building", "Journalism Department", "Ellen Khuzwayo Female Residence,", "Education Department" };
                db.addDockingStation(dockingStation);//@dev in future i will check the value returned
                var pins = db.getAllDockingStations();
                customMap = new RhodesMap {
                MapType=MapType.Street,
                WidthRequest= Variables.Width,
                HeightRequest= Variables.Height,
                MinimumHeightRequest=Variables.Height,
                MinimumWidthRequest=Variables.Width
            };
            customMap.VerticalOptions = LayoutOptions.FillAndExpand;
            customMap.DockingStaions = pins;
            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(pins[0].Position, Distance.FromMiles(1.0)));
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
            
        }
            public bool IsNullOrValue(double valueToCheck)
            {
                return Double.IsNaN(valueToCheck);
            }
            private int getMapZoom()
            {
            //   // var LatLng = (customMap.VisibleRegion.LatitudeDegrees + customMap.VisibleRegion.LongitudeDegrees) / 2.0f;
             //   int zoom = (int)Math.Floor(Math.Log(360 / LatLng, 2));
               return 0;
            }
            private double setMapZoom(int nZoom)
            {
                if (nZoom < 1 || nZoom > 18)
                {
                    return 0;
                }

                var latlongdeg = 360 / (Math.Pow(2, nZoom));
                return latlongdeg;
            }
            /// <summary>
            /// Ons the property changed.
            /// </summary>
            /// <param name="propertyName">Property name.</param>
            void OnPropertyChanged(string propertyName)
            {
                //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        public Map getCustomMap()
        {
            return customMap;
        }
        public void moveToRegion()
        {
            customMap.MoveToRegion(new MapSpan(startPos, 0.01, 0.01));

        }
    }
}
