using System;

namespace RhodeIT
{
	public static class NameRemover
	{


		public static string Process(string venueLocation)
		{
			int index = 0;
			var temp = "";
			while (venueLocation[index++] != ':') ;
			index++;
			while (venueLocation.Length > index) { if (venueLocation[index] == '"') { index++; continue; } temp += venueLocation[index++]; }
			return temp.Trim();
		}
	}
}