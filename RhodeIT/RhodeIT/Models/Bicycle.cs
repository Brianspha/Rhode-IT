using Realms;
using System.Collections.Generic;
using System.ComponentModel;

namespace RhodeIT.Models
{
    public class Bicycle : RealmObject, INotifyPropertyChanged
    {
        public string ID { get; set; }
        public string DockdeAt { get; set; }
        public bool Status { get; set; }
        public string renter { get; set; }
        public IList<Student> UserHistory { get; }
    }
}
