using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeIT.Models
{
    [JsonObject(MemberSerialization.OptIn)] // Only properties marked [JsonProperty] will be serialized
    public class DockingStaion:RealmObject, INotifyPropertyChanged
    {
        [JsonProperty]
        /// <summary>
        /// Stores all information about this particular Docking station i.e. Name, lat,long etc
        /// </summary>
        public VenueLocation DockingStationInformation { get; set; }
        [JsonProperty]
        /// <summary>
        /// Stores all available bicycles on docking station
        /// </summary>
        public IList<Bicycle> AvailableBicycles { get;  }
}
}
