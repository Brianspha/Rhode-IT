﻿using System;
namespace RhodeIT
{
	public static class CommaRemover
	{

		/// <summary>
		/// Removes the last comma from the latitude and longitude.
		/// Eg. 24.65656565, we remove the , to make processing easier
		/// </summary>
		/// <returns>The comma.</returns>
		/// <param name="g">The green component.</param>
		public static string RemoveComma(string latorlong)
		{
			string temp = "";
			var temp2 = latorlong.Split(',');
			temp += temp2[0].Trim();
			return temp.Trim();
		}
	}
}
