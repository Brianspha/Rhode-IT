using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
namespace RhodeIT.Helpers
{
   public static class Variables
    {
        public static string mapsAPIKEY = "AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o";
        public static int Height= (int)DeviceDisplay.MainDisplayInfo.Height;
        public static int Width = (int)DeviceDisplay.MainDisplayInfo.Width;
        public static string connectionStringRhodeITDB = "Server=146.231.123.137; Port=5432; User Id=admin; Password=Password12#; Database=RhodeITDB; Timeout=30;";
        public static string connectionStringRhodesDB = "Server=146.231.123.137; Port=5432; User Id=admin; Password=1234; Database=RhodesDB; Timeout=30;";


    }
}
