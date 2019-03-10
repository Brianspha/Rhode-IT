using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using TK.CustomMap;
using Rhode_IT.Databases;

namespace Rhode_IT.ViewModels
{
    public class MapsViewModel : INotifyPropertyChanged
        {
            private TKCustomMap customMap;
            MainDataBase db;
            bool visibleYet { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            public MapsViewModel()
            {
                db = new MainDataBase();
                SetUpMap();
            }
            public void IsVisible()
            {
              //  if (customMap.VisibleRegion != null) { SetUpMap(); }
            }
            void SetUpMap()
            {
            MapSpan mapSpan = new MapSpan(new Position(-33.311836, 26.520642), 0.002, 0.001);
            customMap = new TKCustomMap(mapSpan);
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

        public TKCustomMap getCustomMap()
        {
            return customMap;
        }
        }
}
