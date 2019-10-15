using Realms;
using System.Collections.Generic;
using System.ComponentModel;

namespace RhodeIT.Models
{
    public class DockingStaion : RealmObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Stores all information about this particular Docking station i.e. Name, lat,long etc
        /// </summary>
        public VenueLocation DockingStationInformation { get; set; }
        /// <summary>
        /// Stores all available bicycles on docking station
        /// </summary>
        public IList<Bicycle> AvailableBicycles { get; }
    }
}
