using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Classes
{
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}
