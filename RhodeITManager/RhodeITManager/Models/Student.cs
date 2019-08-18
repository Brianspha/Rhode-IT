using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeITManager.Models
{

    public class Student:RealmObject, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>

        public string StudentNo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>

        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [JsonProperty]
        public IList<Bicycle> UsedBicycles { get;  } 
    }
}
