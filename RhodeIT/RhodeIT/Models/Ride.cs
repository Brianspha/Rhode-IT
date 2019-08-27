using System.ComponentModel;

namespace RhodeIT.Models
{
    public class Ride : INotifyPropertyChanged
    {
        private string id;
        private string stationName;
        private string reciept;
        private int duration;
        private bool docked;
        public string ID
        {
            get => id; set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ID)));
                }
            }
        }
        public string StationName
        {
            get => stationName; set
            {
                if (value != stationName)
                {
                    stationName = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(StationName)));
                }
            }
        }
        public int Duration
        {
            get => duration;
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Duration)));
                }
            }
        }
        public bool Docked
        {
            get => docked;
            set
            {
                if (value != docked)
                {
                    docked = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Docked)));
                }
            }
        }
        public string TransactionReciept
        {
            get => reciept; set
            {
                if (value != reciept)
                {
                    reciept = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(TransactionReciept)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Invoked when a property is assigned a new value
        /// </summary>
        /// <param name="e"> property thats changed</param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this,e);
        }

    }
}
