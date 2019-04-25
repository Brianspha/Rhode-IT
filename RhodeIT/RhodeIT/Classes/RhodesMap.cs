using RhodeIT.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Clustering;

namespace RhodeIT.Classes
{
   public class RhodesMap:ClusteredMap
    {
        public List<Pin> DockingStaions { get; set; }

    }
}
