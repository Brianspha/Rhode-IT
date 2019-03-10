using System;
using System.Collections.Generic;
using Realms;
namespace Rhode_IT
{
	public class VenueData:RealmObject
	{
		public DateTimeOffset DateSubmitted = DateTime.Now;
		public IList<VenueLocation> Venues { get; }
	}
}
