using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeIT.Models
{
    [JsonObject(MemberSerialization.OptIn)] // Only properties marked [JsonProperty] will be serialized
    /// <summary>
    /// @dev keeps track of all registered docking stations on the app
    /// </summary>
    public class DockingStations:RealmObject, INotifyPropertyChanged
    {
        [JsonProperty]
        /// <summary>
        /// @dev list of venues registered as docking stations
        /// </summary>
        public IList<DockingStaion> RegisteredDockingStaions { get;  }
        [JsonProperty]
        /// <summary>
        /// @dev keeps track of when the docking stations were updated
        /// </summary>
        public DateTimeOffset LastUpdated { get; set; } 
}
}
