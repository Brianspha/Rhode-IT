using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Models
{
    /// <summary>
    /// @dev used for decoding transaction logger whenever we check if a user exists of not
    /// </summary>
    [Event("userExistsLogger")]
    public class UserExistsLogger: IEventDTO
    {
        /// <summary>
        /// @dev the userID +pass hash we checking it registered
        /// </summary>
        [Parameter("bytes32", "tHash", 1,true)]
        public string userID { get; set; }
        /// <summary>
        /// @dev the Results after checking if user exists
        /// </summary>
        [Parameter("bool", "results", 2,true)]
        public bool Results { get; set; }
    }
}
