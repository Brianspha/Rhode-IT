using Newtonsoft.Json;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeITManager.Models
{
    /// <summary>
    /// @dev used by an admin to add new docking stations to the platform
    /// </summary>
    public class EditableVenueLocation
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

        public bool MakeDockingStation { get; set; }
    }
}
