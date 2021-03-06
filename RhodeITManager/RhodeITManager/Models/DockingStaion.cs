﻿using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeITManager.Models
{

    public class DockingStaion:RealmObject, INotifyPropertyChanged
    {

        /// <summary>
        /// Stores all information about  particular Docking station i.e. Name, lat,long etc
        /// </summary>
        public VenueLocation DockingStationInformation { get; set; }
        /// <summary>
        /// Stores all available bicycles on docking station
        /// </summary>
        public IList<Bicycle> AvailableBicycles { get;  }
}
}
