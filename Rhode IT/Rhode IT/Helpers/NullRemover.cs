using System;
using System.Collections.Generic;
using System.Linq;

namespace Rhode_IT
{
	public class NullRemover
	{
		 Dictionary<int, string> RemoveNullValues(Dictionary<int, string> m)
		{
			Dictionary<int, string> temp = new Dictionary<int, string>();
			for (int i = 0; i < m.Count; i++)
			{
				if (m.Values.ToList()[i] != null && m.Values.ToList()[i] != "")
				{
					temp.Add(m.Keys.ToList()[i], m.Values.ToList()[i]);
				}
			}
			return temp;
		}


	}
}
