using Realms;
using System.Collections.Generic;
using System.ComponentModel;

namespace RhodeIT.Models
{
    public class Student : RealmObject, INotifyPropertyChanged
    {

        public string StudentNo { get; set; }

        public string Password { get; set; }


        public IList<Bicycle> UsedBicycles { get; }
    }
}
