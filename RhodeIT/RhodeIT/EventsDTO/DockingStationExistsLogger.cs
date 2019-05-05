using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Models
{
    /// <summary>
    /// @dev used for decoding transaction logger whenever we attempt to sync newly registered docking stations locally with smart contract checking if it exists or not
    /// </summary>
    [Event("dockingStationExistsLogger")]
    public class DockingStationExistsLogger:IEventDTO
    {
        /// <summary>
        /// @dev the tHash of (name+lat+long) hashed of the of dockingstation to be checked if it exists
        /// </summary>
        [Parameter("bytes32", "name", 1,true)]
        public string Name { get; set; }
        /// <summary>
        /// @dev the results after checking if docking station exists
        /// </summary>
        [Parameter("bool", "results", 2,true)]
        public bool Results { get; set; }
    }
}
