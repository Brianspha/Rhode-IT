using System;
using System.Collections.Generic;
using Realms;
namespace RhodeIT
{
	public class VenueLocation : RealmObject
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTimeOffset LastUpdated = DateTime.Now;
    }
}
