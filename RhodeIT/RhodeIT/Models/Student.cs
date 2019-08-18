using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeIT.Models
{
    [JsonObject(MemberSerialization.OptIn)] // Only properties marked [JsonProperty] will be serialized
    public class Student:RealmObject, INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string StudentNo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [JsonProperty]
        public IList<Bicycle> UsedBicycles { get;  } 
    }
}
