using System;
using System.Collections.Generic;
using Android;
using AndroidHUD;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using Rhode_IT.Classes;
using Rhode_IT.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Android.Views;
using Rhode_IT.Databases;

[assembly: ExportRenderer(typeof(RhodesMap), typeof(CustomMapRenderer))]
namespace Rhode_IT.Droid.CustomRenderers
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        List<DockingStaionPin> customPins;
        MainDataBase db;
        public CustomMapRenderer(Context context) : base(context)
        {
            db = new MainDataBase();
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (RhodesMap)e.NewElement;
                customPins = formsMap.DockingStaions;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.InfoWindowClick += OnInfoWindowClick;
            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            int count = db.getAvailableBicyclesCount(pin.Label);
            marker.SetSnippet("Available Bicycles: " +count.ToString());
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.ic_launcher));
            return marker;
        }

        void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
       
        }

        public global::Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                global::Android.Views.View view;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }
                view = inflater.Inflate(Resource.Layout.PinView, null);

                var name = view.FindViewById<TextView>(Resource.Id.name);
                var availableBiycles = view.FindViewById<TextView>(Resource.Id.availableBicycles);

                if (name != null)
                {
                    name.Text = marker.Title;
                }
                if (availableBiycles != null)
                {
                    availableBiycles.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        public global::Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        DockingStaionPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

 
    }
}

