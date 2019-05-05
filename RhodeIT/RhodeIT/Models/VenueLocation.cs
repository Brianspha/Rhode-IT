using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Realms;
namespace RhodeIT
{
    [JsonObject(MemberSerialization.OptIn)] // Only properties marked [JsonProperty] will be serialized

    public class VenueLocation : RealmObject
	{
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]

        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]

        public string Description { get; set; }
        /// <summary>
        /// /
        /// </summary>
        [JsonProperty]

        public double Lat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty]

        public double Long { get; set; }


    }
}
