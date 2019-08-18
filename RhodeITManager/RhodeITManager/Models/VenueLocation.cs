using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Realms;
namespace RhodeITManager
{


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


        public double Lat { get; set; }
        /// <summary>
        /// 
        /// </summary>


        public double Long { get; set; }


    }
}
