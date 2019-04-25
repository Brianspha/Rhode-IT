using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Models
{
    /// <summary>
    /// @dev used for decoding transaction logger whenever we attempt to sync newly registered docking stations locally with smart contract data
    /// </summary>
    [Event("addDockingStationLogger")]
    public class AddDockingStationLogger:IEventDTO
    {
        /// <summary>
        /// @dev name of docking station
        /// </summary>
        [Parameter("string", "name", 1,true)]
        public string Name { get; set; }
       /// <summary>
       /// @dev latitide position on map
       /// </summary>
        [Parameter("string", "Latitude", 2, true)]
        public string Latitude { get; set; }
        /// <summary>
        /// @dev longitude position on map
        /// </summary>
        [Parameter("string", "Longitude", 3, true)]
        public string Longitude { get; set; }
        /// <summary>
        /// @dev results of adding user
        /// </summary>
        [Parameter("bool", "results", 4)]
        public bool Results { get; set; }
    }
}
