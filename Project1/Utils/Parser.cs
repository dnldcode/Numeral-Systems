using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	public enum Base { Decimal = 10, Binary = 2, Octal = 8, Hex = 16, Unknown };

	public static class Parser
	{
		static public Base Parse(Object obj)
		{
			String value = obj as String;

			while (value.Contains(".")) value = value.Replace(".", ",");

			if (isOctal(value))
				return Base.Octal;
			if (isDecimal(value))
				return Base.Decimal;
			if (isHex(value))
				return Base.Hex;
			else
				return Base.Unknown;
		}

		static private bool isDecimal(String value)
		{
			double res;
			return (!value.StartsWith("0") || value.StartsWith("0,")) && Double.TryParse(value, out res);
		}

		static private bool isOctal(String value)
		{
			double res;
			return value.StartsWith("0") && !value.StartsWith("0,") && Double.TryParse(value, out res) && !value.Contains("8") && !value.Contains("9");
		}

		static private bool isHex(String value)
		{
			double res;
			if (value.StartsWith("0x"))
			{
				value = value.Substring(2, value.Length - 2);

				foreach(char c in value)
				{
					if (c == ',') continue;

					if (!Formatter.hexValuesDictionaty.ContainsKey(c) && !Double.TryParse(c.ToString(), out res))
						return false;
				}

				return true;
			}
			else return false;
		}
	}
}
