using System;
using System.IO;
using Android.Content;
using Android.Content.Res;
using Android.Media;
using System.Collections.Generic;

namespace RhodeIT.Android
{

    public class ResourceHelper
	{
		Context context;
		string FileName,FileName1;
		List<VenueLocation> loc;
		public ResourceHelper(Context ctx,string FileName,string FileName1)
		{
			loc = new List<VenueLocation>();
			context = ctx;
			this.FileName = FileName;
            this.FileName1 = FileName1;
		}
		/// <summary>
		/// Reads the local file.
		/// </summary>
		/// <returns>GeoJSONData in raw format.</returns>
		public GeoJSONData ReadLocalFile()
		{
			GeoJSONData data = new GeoJSONData();
			string content="";
			using (var input = context.Assets.Open(FileName))
			using (StreamReader sr = new System.IO.StreamReader(input))
			{
				string temp = sr.ReadLine();
				while (true)
				{
					if (string.IsNullOrEmpty(temp)) break;
					content += temp + "\n";
					temp = sr.ReadLine();
				}
			}
			var geoData = new GeoJSONData ();
			geoData.Data = content;
            var tempList = new List<VenueLocation>();
            using (var input = context.Assets.Open(FileName1))
            using (StreamReader sr = new StreamReader(input))
            {
                string temp = sr.ReadLine();
                while (true)
                {
                    if (string.IsNullOrEmpty(temp)) break;
                    var subject = "";
                    var index = 0;
                    for (int i = 0; i < temp.Length; i++, index++)
                    {
						if (temp[i] != '[') subject += temp[i];
						else
						{
							index++; //skip over the '['
							break;
						}
                    }
                    var venue = "";
					subject = new SpaceRemover(subject).RemovedSpaces;
					for (int a = index; a < temp.Length; a++) {
						var ch = temp[a];
						if (temp[a] == ']') break;
						else venue += temp[index++];
					}
                    var tempVenueLoc = new VenueLocation { Name = venue };
					tempList.Add(tempVenueLoc);
                    temp = sr.ReadLine();
                }
            }
			var result = string.Join(",", tempList);
			loc = tempList;
            return geoData;
		}
		public List<VenueLocation> GetParsedVenuesWithSubjects()
		{
			return loc;
		}

	}
}
