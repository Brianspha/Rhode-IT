using RhodeIT.Databases;
using RhodeIT.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RhodeIT.ViewModels
{
    public class RidesViewModel
    {
        private ObservableCollection<Ride> rides;
        public ObservableCollection<Ride> Rides
        {
            get => rides;
            set
            {
                if (value != rides)
                {
                    rides = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Rides)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Constructor
        /// </summary>
        public RidesViewModel()
        {
            SetUp();
        }
        /// <summary>
        /// Fetches data from the smart contract and creates Ride objects to be used by the neccessary View models
        /// </summary>
        private void SetUp()
        {
            Rides = new ObservableCollection<Ride>();
            RhodeITDB db = new RhodeITDB();
            List<Ride> userRides = db.GetUserUpComingRides();
            foreach (Ride ride in userRides)
            {
                Rides.Add(new Ride { ID = ride.ID, Docked = ride.Docked, TransactionReceipt = ride.TransactionReceipt, StationName = ride.StationName });
            }
        }

        /// <summary>
        /// Invoked when a property is assigned a new value
        /// </summary>
        /// <param name="e"> property thats changed</param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }



    }
}
