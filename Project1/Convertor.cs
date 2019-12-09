using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	static class Convertor
	{
		static public void Convert(Object obj)
		{
			String decimalValue = "";
			String binaryValue = "";
			String octalValue = "";
			String hexValue = "";


			switch (Parser.Parse(obj))
			{
				case Base.Decimal:
					int dec = Int32.Parse(obj as string);

					decimalValue = dec.ToString();
					binaryValue = decimalToAny(dec, Base.Binary);
					octalValue = decimalToAny(dec, Base.Octal);
					hexValue = decimalToAny(dec, Base.Hex);

					break;
				default:
					break;
			}

			Console.WriteLine("In decimal: " + decimalValue);
			Console.WriteLine("In binary: " + binaryValue);
			Console.WriteLine("In octal: " + octalValue);
			Console.WriteLine("In hex: " + hexValue);
		}


		static private String decimalToAny(int dec, Base b)
		{
			int divider = (int) b;

			int result = dec;
			List<int> remainders = new List<int>();

			while (result != 0)
			{
				remainders.Add((int)result % divider);
				result /= divider;
			}

			remainders.Reverse();

			return Convertor.Format(String.Join("", remainders.ToArray()), b);
		}

		static private String AnyToDecimal(String any, Base b)
		{
			double baseIndex = (double)b;
			int sum = 0;

			for (int i = 0; i < any.Length; i++)
			{
				sum += Int32.Parse(any[i].ToString()) * (int)Math.Pow(baseIndex, any.Length - 1 - i);
			}

			return sum.ToString();

		}

		static private String Format(String value, Base b)
		{
			switch (b)
			{
				case Base.Octal:
					value = $"0{value}";
					break;
				case Base.Hex:
					value = $"0x{value}";
					break;
			}

			return value;
		}
	}
}
