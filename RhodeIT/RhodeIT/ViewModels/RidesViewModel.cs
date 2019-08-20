using RhodeIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace RhodeIT.ViewModels
{
    public class RidesViewModel
    {

        ObservableCollection<Ride> rides;
        public ObservableCollection<Ride> Rides
        {
            get
            {
                return rides;
            }
            set
            {
                if (value != rides)
                {
                    rides = value;
                    OnPropertyChanged(nameof(Rides));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        public RidesViewModel()
        {
            SetUp();
        }

        private void SetUp()
        {
            Rides = new ObservableCollection<Ride>();
            for(int i=0; i < 51; i++)
            {
                Rides.Add(new Ride { ID = i.ToString(), Docked = true, TransactionReciept = "0x6686491f125ab926f51b1ddf3b8ac370c902cb637a3fb6af2a9ef1b59df07a1a", Duration = i * 10000, StationName = "Hamilton" });
            }
        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}
