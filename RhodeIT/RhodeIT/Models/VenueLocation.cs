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

        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string Description { get; set; }
        /// <summary>
        /// /
        /// </summary>

        public double Latitude { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public double Longitude { get; set; }


    }
}
