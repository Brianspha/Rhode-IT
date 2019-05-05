using System;
using System.Collections.Generic;
using System.Text;

namespace RhodeIT.Helpers
{
    /// <summary>
    /// @de responsible for remove any " from a give n string
    /// </summary>
    public static class QuoteRemover
    {
        public static string removeQuote(string toclean)
        {
            string temp = toclean.Replace('"', ' ');
            Console.WriteLine("Cleaned: ", temp);
            string [] split = temp.Split(':');
            temp =split[1];

            return temp;
        }
    }
}
