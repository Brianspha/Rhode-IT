﻿using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Models
{
   public class TransactionReciept:RealmObject
    {
        public string Receipt { get; set; }
        public string Activity { get; set; }
    }
}
