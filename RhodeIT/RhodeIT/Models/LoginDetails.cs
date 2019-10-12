using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Models
{
 public class LoginDetails:RealmObject
{
      public string User_ID { get; set; }
        public string Password { get; set; }
        public string TransactionHash { get;  set; }
        public string Ethereum_Address { get; set; }
        public int RideCredits { get; set; }
    }
}
