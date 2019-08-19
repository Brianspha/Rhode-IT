using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rhode_IT.Models
{
  public class Bicycle:RealmObject
{
        [Indexed]
     public int ID { get; set; }
}
}
