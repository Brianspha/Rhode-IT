using System;
using System.Collections.Generic;
using Realms;
namespace RhodeIT
{
	public class VenueData:RealmObject
	{
		public DateTimeOffset DateSubmitted = DateTime.Now;
		public IList<VenueLocation> Venues { get; }
	}
}
