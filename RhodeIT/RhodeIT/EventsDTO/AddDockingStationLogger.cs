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
        /// (Name+Lat+Long) hashed together
        /// </summary>
        [Parameter("bytes32", "tHash", 1,true)]
        public string Name { get; set; }
       /// <summary>
       /// @dev results of the transaction
       /// </summary>
        [Parameter("bool", "results", 2, true)]
        public bool Results { get; set; }
       
    }
}
