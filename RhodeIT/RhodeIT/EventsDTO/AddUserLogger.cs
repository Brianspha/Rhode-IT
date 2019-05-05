using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Models
{
    /// <summary>
    /// @dev used to fetch the log emitted when we add a new user
    /// </summary>
    [Event("addUserLogger")]
   public class AddUserLogger:IEventDTO
   {
        /// <summary>
        /// @dev the user ID + password hashed 
        /// </summary>
        [Parameter("bytes32", "tHash", 1,true)]
       public string userID { get; set; }
        /// <summary>
        /// @dev the results after adding the user
        /// </summary>
        [Parameter("bool", "results", 2,true)]
        public bool Results { get; set; }
   }
}
