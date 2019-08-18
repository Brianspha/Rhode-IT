using RhodeIT.Databases;
using System.Collections.Generic;
using System.Globalization;
namespace RhodeIT.Helpers
{
    public class GEOJSONParser
    {
        private GeoJSONData data;
        private RhodeITDB db;
        private readonly List<VenueLocation> LocationsWithAssociatedHostedSubjects;
        public GEOJSONParser(GeoJSONData dat, List<VenueLocation> ven)
        {
            data = dat;
            LocationsWithAssociatedHostedSubjects = ven;
            Process();

        }
        /// <summary>
        /// @dev Process this GeoJSON data into JSON.
        /// @notice Disclaimer Realm does not support storing of Point data structures
        /// 
        /// </summary>
        private void Process()
        {
            string[] list = data.Data.Split('\n');
            List<VenueLocation> VenueList = new List<VenueLocation>();
            bool readCord = false;
            VenueLocation tempLoc = new VenueLocation();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Contains("properties"))
                {
                    i++;
                    tempLoc.Name = CommaRemover.RemoveComma(list[i++]);
                    tempLoc.Name = QuoteRemover.removeQuote(tempLoc.Name);
                    tempLoc.Description = NameRemover.Process(list[i].Trim());
                }
                if (list[i].Contains("coordinates"))
                {
                    string removecommaLat = CommaRemover.RemoveComma(list[++i]);
                    string removecommaLong = CommaRemover.RemoveComma(list[++i]);
                    tempLoc.Latitude = double.Parse(removecommaLat, CultureInfo.InvariantCulture);
                    tempLoc.Longitude = double.Parse(removecommaLong, CultureInfo.InvariantCulture);
                    readCord = true;
                    tempLoc.Latitude = tempLoc.Latitude;
                    tempLoc.Longitude = tempLoc.Longitude;
                    continue;
                }
                else if (readCord)
                {
                    VenueList.Add(tempLoc);
                    readCord = false;
                    tempLoc = new VenueLocation();
                }
            }

            db.storeVenueLocations(VenueList);
        }


    }
}
