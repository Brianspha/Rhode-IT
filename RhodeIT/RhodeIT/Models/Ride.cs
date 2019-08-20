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
                    OnPropertyChanged(nameof(ID));
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
                    OnPropertyChanged(nameof(StationName));
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
                    OnPropertyChanged(nameof(Duration));
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
                    OnPropertyChanged(nameof(docked));
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
                    OnPropertyChanged(nameof(reciept));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
