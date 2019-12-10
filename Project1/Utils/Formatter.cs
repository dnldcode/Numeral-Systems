using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	public static class Formatter
	{
		public static Dictionary<char, int> hexValuesDictionaty = new Dictionary<char, int>()
		{
			{'A', 10 },
			{'B', 11 },
			{'C', 12 },
			{'D', 13 },
			{'E', 14 },
			{'F', 15 },
		};

		static public String Parse(String value, Base b, bool fraction = false)
		{
			switch (b)
			{
				case Base.Octal:
					if (!value.StartsWith("0") && !fraction)
						value = $"0{value}";
					break;
				case Base.Hex:
					foreach (KeyValuePair<char, int> entry in hexValuesDictionaty)
					{
						if (value.Contains(entry.Value.ToString()))
							value = value.Replace(entry.Value.ToString(), entry.Key.ToString());
					}

					if (!value.StartsWith("0x") && !fraction)
						value = $"0x{value}";
					break;
			}
			return value;
		}

		static public int GetHexNumber(char hex)
		{
			if (hexValuesDictionaty.ContainsKey(hex))
				return hexValuesDictionaty[hex];
			else
				throw new InvalidHexValueFoundException("Hex does not contain value: " + hex);
		}
	}
}
