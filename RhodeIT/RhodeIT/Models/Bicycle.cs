﻿using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RhodeIT.Models
{
    public class Bicycle:RealmObject,INotifyPropertyChanged
{
   
     public int ID { get; set; }
        public string BikeName { get; set; }
     public string Model { get; set; }
        public bool Status { get; set; }
        public IList<Student> UserHistory { get;  } 
}
}
