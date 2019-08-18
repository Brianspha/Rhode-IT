using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Realms;
namespace RhodeITManager
{

    public class VenueData:RealmObject
	{
        /// <summary>
        /// 
        /// </summary>


        public DateTimeOffset DateSubmitted = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>


        public IList<VenueLocation> Venues { get; }
	}
}
