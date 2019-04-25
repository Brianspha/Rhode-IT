using RhodeIT.Databases;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RhodeIT.Helpers
{
	public class GEOJSONParser
    {
		GeoJSONData data;
		LocalDataBase db;
        List<VenueLocation> LocationsWithAssociatedHostedSubjects;
		public GEOJSONParser(GeoJSONData dat,List<VenueLocation> ven)
		{
			data = dat;
			db = new LocalDataBase();
			LocationsWithAssociatedHostedSubjects = ven;
		}
		/// <summary>
		/// Process this GeoJSON data into JSON.
		/// Disclaimer Realm does not support storing of Point data structures
		/// 
		/// </summary>
		public void Process()
		{
			var list = data.Data.Split('\n');
			var VList = new List<VenueLocation>();
			var readCord = false;
			var tempLoc = new VenueLocation();
			for (int i = 0; i < list.Length; i++)
			{
				if (list[i].Contains("properties"))
				{
					i++;
					tempLoc.Name = new CommaRemover(list[i++]).RemoveComma();
                    tempLoc.Name = QuoteRemover.removeQuote(tempLoc.Name);
                    tempLoc.Description = list[i].Trim();
					continue;
				}
				if (list[i].Contains("coordinates"))
				{
					var removecommaLat =  new CommaRemover(list[++i]).RemoveComma();				
					var removecommaLong  = new CommaRemover(list[++i]).RemoveComma();
					tempLoc.Lat = double.Parse(removecommaLat, CultureInfo.InvariantCulture);
					tempLoc.Long = double.Parse(removecommaLong, CultureInfo.InvariantCulture);
					readCord = true;
                    var temp = tempLoc.Lat;
                    tempLoc.Lat = tempLoc.Long;
                    tempLoc.Long = temp;
					continue;
				}
				if (readCord)
				{
					VList.Add(tempLoc);
					readCord = false;
					tempLoc = new VenueLocation();
				}
			}

			db.storeVenueLocations(VList);
		}


	}
}
