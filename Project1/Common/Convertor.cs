using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	public static class Convertor
	{
		static public void Convert(Object obj)
		{
			String decimalValue = "Unknown type provided";
			String binaryValue = "Unknown type provided";
			String octalValue = "Unknown type provided";
			String hexValue = "Unknown type provided";

			obj = (obj as String).Replace(",", ".");

			switch (Parser.Parse(obj))
			{
				case Base.Decimal:
					String dec = obj as string;

					decimalValue = dec;
					binaryValue = DecimalToAny(dec, Base.Binary);
					octalValue = DecimalToAny(dec, Base.Octal);
					hexValue = DecimalToAny(dec, Base.Hex);

					break;
				case Base.Octal:
					String octal = obj as string;

					decimalValue = OctalToAny(octal, Base.Decimal);
					binaryValue = OctalToAny(octal, Base.Binary);
					octalValue = octal;
					hexValue = OctalToAny(octal, Base.Hex);

					break;
				case Base.Hex:
					String hex = obj as string;

					decimalValue = HexToAny(hex, Base.Decimal);
					binaryValue = HexToAny(hex, Base.Binary);
					octalValue = HexToAny(hex, Base.Octal);
					hexValue = hex;

					break;
				default:
					break;
			}

			Console.WriteLine("In decimal: " + decimalValue);
			Console.WriteLine("In binary: " + binaryValue);
			Console.WriteLine("In octal: " + octalValue);
			Console.WriteLine("In hex: " + hexValue);
		}


		static public String DecimalToAny(String dec, Base b)
		{
			if (Parser.Parse(dec) == Base.Unknown)
				throw new InvalidArgumentException("Invalid value was provided.");

			if (b == Base.Decimal) return dec;

			String fractionValue = "";
			if (dec.Contains("."))
			{
				String[] fraction = dec.Split('.');
				dec = fraction[0];

				fractionValue = DecimalFractionToAny(fraction[1], b);
			}

			int result = int.Parse(dec);
			int divider = (int)b;
			List<int> remainders = new List<int>();

			while (result != 0)
			{
				remainders.Add((int)result % divider);
				result /= divider;
			}

			remainders.Reverse();

			String returnValue = Formatter.Parse(String.Join("", remainders.ToArray()), b);
			if (fractionValue != "") returnValue = $"{returnValue}.{fractionValue}";

			return returnValue;
		}

		static public String AnyToDecimal(String any, Base b)
		{
			if (Parser.Parse(any) == Base.Unknown)
				throw new InvalidArgumentException("Invalid value was provided.");

			if (any.Contains("."))
				return AnyToDecimalFraction(any, b);

			if (b == Base.Decimal)
				return any;

			if (b == Base.Hex && any.StartsWith("0x"))
				any = any.Substring(2, any.Length - 2);
			else if (b == Base.Octal && any.StartsWith("0"))
				any = any.Substring(1, any.Length - 1);

			int baseIndex = (int)b;
			int sum = 0;

			for (int i = 0; i < any.Length; i++)
			{
				int value;
				if (!Int32.TryParse(any[i].ToString(), out value))
					value = Formatter.GetHexNumber(any[i]);

				int i2 = (int)Math.Pow(baseIndex, any.Length - 1 - i);

				sum += value * i2;
			}

			return sum.ToString();
		}

		static public String OctalToAny(String octal, Base b)
		{
			if (Parser.Parse(octal) == Base.Unknown)
				throw new InvalidArgumentException("Invalid value was provided.");

			if (b == Base.Octal) return octal;

			return DecimalToAny(AnyToDecimal(octal, Base.Octal), b);
		}

		static public String HexToAny(String hex, Base b)
		{
			if (Parser.Parse(hex) == Base.Unknown)
				throw new InvalidArgumentException("Invalid value was provided.");

			if (b == Base.Hex) return hex;

			return DecimalToAny(AnyToDecimal(hex, Base.Hex), b);
		}

		static public String AnyToDecimalFraction(String any, Base b)
		{
			if (Parser.Parse(any) == Base.Unknown)
				throw new InvalidArgumentException("Invalid value was provided.");

			if (b == Base.Hex && any.StartsWith("0x"))
				any = any.Substring(2, any.Length - 2);
			else if (b == Base.Octal && any.StartsWith("0"))
				any = any.Substring(1, any.Length - 1);

			int indexOfPoint = any.IndexOf('.');
			int baseIndex = (int)b;
			double sum = 0;

			any = any.Replace(".", "");
			for (int i = 0; i < any.Length; i++)
			{
				int value;
				if (!Int32.TryParse(any[i].ToString(), out value))
					value = Formatter.GetHexNumber(any[i]);

				sum += value * Math.Pow(baseIndex, indexOfPoint - 1 - i);
			}

			return sum.ToString().Replace(",", ".");
		}

		static public String DecimalFractionToAny(String any, Base b)
		{
			if (Parser.Parse(any) == Base.Unknown)
				throw new InvalidArgumentException("Invalid value was provided.");

			if (b == Base.Decimal)
				return any;

			double result = Double.Parse($"0,{any}");
			int divider = (int)b;
			List<String> integerParts = new List<String>();

			for (int i = 0; i < 10; i++)
			{
				result *= divider;
				String resultString = result.ToString();

				int indexOfPoint = resultString.IndexOf(',');
				String fractionPart = resultString.Substring(indexOfPoint + 1, resultString.Length - indexOfPoint - 1);
				String integerPart = (indexOfPoint > -1) ? resultString.Substring(0, indexOfPoint) : "0";

				result = Double.Parse($"0,{fractionPart}");

				integerParts.Add(integerPart);
			}

			return Formatter.Parse(String.Join("", integerParts.ToArray()), b, true);
		}
	}
}