using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Helpers
{
    public static class LatLongSeparator
    {
        public static Tuple<int, int, int, int> Decouple(double Lat, double Long)
        {
            var temp = new Tuple<int, int, int, int>(0, 0, 0, 0);
            var LatWhole = Lat.ToString().Split('.');
            var Latitude = Math.Abs(Convert.ToInt32(LatWhole[0]));
            var LatitudeP2 = Convert.ToInt32(LatWhole[1]);
            var LongWhole = Long.ToString().Split('.');
            var Longitude = Convert.ToInt32(LongWhole[0]);
            var LongitudeP2 = Convert.ToInt32(LongWhole[1]);

            return new Tuple<int, int, int, int>(Latitude,LatitudeP2,Longitude,LongitudeP2);
        }
    }
}
