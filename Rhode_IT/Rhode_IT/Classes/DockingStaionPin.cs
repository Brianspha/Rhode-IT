using Rhode_IT.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xamarin.Forms.Maps;

namespace Rhode_IT.Classes
{
     public class DockingStaionPin: Pin
    {
        public List<Bicycle> availableBicycles { get; set; }
        public string Label { get; set; }
        public Position Position { get; set; }
        public Type Type { get; set; }
        
    }
}
