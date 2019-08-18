using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeITManager.Models
{
 public   class LoginDetails:RealmObject
{
      public string userID { get; set; }
        public string password { get; set; }
        public string TransactionHash { get; internal set; }
    }
}
