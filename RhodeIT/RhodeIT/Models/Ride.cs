using Realms;
using System.ComponentModel;

namespace RhodeIT.Models
{
    public class Ride : RealmObject, INotifyPropertyChanged
    {
        public string ID { get; set; }
        public string StationName { get; set; }
        public string Docked { get; set; }
        public string TransactionReceipt { get; set; }


    }
}
