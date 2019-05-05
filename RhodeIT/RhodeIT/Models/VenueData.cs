using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Realms;
namespace RhodeIT
{
    [JsonObject(MemberSerialization.OptIn)] // Only properties marked [JsonProperty] will be serialized
    public class VenueData:RealmObject
	{
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]

        public DateTimeOffset DateSubmitted = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]

        public IList<VenueLocation> Venues { get; }
	}
}
