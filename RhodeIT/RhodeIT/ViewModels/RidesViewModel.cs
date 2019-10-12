﻿using RhodeIT.Models;
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
            for(int i=0; i < 0; i++)
            {
                Rides.Add(new Ride { ID = i.ToString(), Docked = true, TransactionReciept = "0x6686491f125ab926f51b1ddf3b8ac370c902cb637a3fb6af2a9ef1b59df07a1a", Duration = i * 10000, StationName = "Hamilton" });
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
