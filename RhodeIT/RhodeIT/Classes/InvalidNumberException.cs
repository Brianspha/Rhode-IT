using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Classes
{
   public class InvalidNumberException:Exception
    {
        public InvalidNumberException(string message) : base(message)
        {

        }
    }
}
