﻿using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeIT.Models
{
    [JsonObject(MemberSerialization.OptIn)] // Only properties marked [JsonProperty] will be serialized
    public class Bicycle:RealmObject,INotifyPropertyChanged
{
        [JsonProperty]
        [Indexed]
     public int ID { get; set; }
        [JsonProperty]
        public string BikeName { get; set; }
     public string Model { get; set; }
        [JsonProperty]
        public bool Status { get; set; }
        [JsonProperty]
        public IList<Student> UserHistory { get;  } 
}
}
