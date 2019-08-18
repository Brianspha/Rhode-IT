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
    public class UserExistsLogger : IEventDTO
    {
        [Parameter("bytes32", "tHash", 1, true)]
        public virtual byte[] THash { get; set; }
        [Parameter("bool", "results", 2, true)]
        public virtual bool Results { get; set; }
    }
}
