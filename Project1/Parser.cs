using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	enum Base { Decimal = 10, Binary = 2, Octal = 8, Hex = 16 };

	static class Parser
	{
		static public Base Parse(Object obj)
		{
			if (isDecimal(obj))
				return Base.Decimal;
			else
				throw new Exception("Type not provided");
		}

		static private bool isDecimal(Object obj)
		{
			double res;
			return Double.TryParse(obj.ToString(), out res);
		}
	}
}
