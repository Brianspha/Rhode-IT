using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeIT.Models
{
    /// <summary>
    /// @dev keeps track of all registered docking stations on the app
    /// </summary>
    public class DockingStations:RealmObject, INotifyPropertyChanged
    {
        /// <summary>
        /// @dev list of venues registered as docking stations
        /// </summary>
       public IList<DockingStaion> RegisteredDockingStaions { get;  }
        /// <summary>
        /// @dev keeps track of when the docking stations were updated
        /// </summary>
        public DateTimeOffset LastUpdated { get; set; } 
}
}
