using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Project1.Test
{
	[TestClass]
	public class ConvertorTest
	{
		[TestMethod]
		public void BinaryToDecimal()
		{
			Assert.AreEqual(Convertor.AnyToDecimal("101101", Base.Binary), "45");
		}

		[TestMethod]
		public void OctalToDecimal()
		{
			Assert.AreEqual(Convertor.AnyToDecimal("430", Base.Octal), "280");
			Assert.AreEqual(Convertor.AnyToDecimal("0430", Base.Octal), "280");
		}

		[TestMethod]
		public void HexToDecimal()
		{
			Assert.AreEqual(Convertor.AnyToDecimal("0x1A2C", Base.Hex), "6700");

			Assert.AreEqual(Convertor.AnyToDecimal("0x123", Base.Hex), "291");
		}

		[TestMethod]
		public void DecimalToOctal()
		{
			Assert.AreEqual(Convertor.DecimalToAny("68", Base.Octal), "0104");
			Assert.AreEqual(Convertor.DecimalToAny("123", Base.Octal), "0173");
			Assert.AreEqual(Convertor.DecimalToAny("8271", Base.Octal), "020117");
		}

		[TestMethod]
		public void DecimalToHex()
		{
			Assert.AreEqual(Convertor.DecimalToAny("68", Base.Hex), "0x44");
		}

		[TestMethod]
		public void DecimalToBinary()
		{
			Assert.AreEqual(Convertor.DecimalToAny("68", Base.Binary), "1000100");
			Assert.AreEqual(Convertor.DecimalToAny("123", Base.Binary), "1111011");
			Assert.AreEqual(Convertor.DecimalToAny("8271", Base.Binary), "10000001001111");
		}

		[TestMethod]
		public void Parse()
		{
			Assert.AreEqual(Formatter.Parse("7ABC", Base.Hex), "0x7ABC");
			Assert.AreEqual(Formatter.Parse("0x7ABC", Base.Hex), "0x7ABC");

			Assert.AreEqual(Formatter.Parse("05", Base.Octal), "05");
			Assert.AreEqual(Formatter.Parse("5", Base.Octal), "05");
		}

		[TestMethod]
		public void DefineTypeTest()
		{
			Assert.AreEqual(Parser.Parse("0123"), Base.Octal);
			Assert.AreEqual(Parser.Parse("123"), Base.Decimal);
			Assert.AreEqual(Parser.Parse("0x123"), Base.Hex);
		}

		[TestMethod]
		public void AnyToDecimalFraction()
		{
			Assert.AreEqual(Convertor.AnyToDecimalFraction("101.011", Base.Binary), "5.375");
			Assert.AreEqual(Convertor.AnyToDecimalFraction("0.763", Base.Octal), "0.974609375");
			Assert.AreEqual(Convertor.AnyToDecimalFraction("0x1A.F1", Base.Hex), "26.94140625");

			Assert.AreEqual(Convertor.AnyToDecimalFraction("0123.456", Base.Octal), "83.58984375");
		}

		[TestMethod]
		public void DecimalFractionToAny()
		{
			Assert.AreEqual(Convertor.DecimalFractionToAny("456", Base.Binary), "0111010010");
			Assert.AreEqual(Convertor.DecimalFractionToAny("456", Base.Octal), "3513615237");
			Assert.AreEqual(Convertor.DecimalFractionToAny("456", Base.Hex), "74BC6A7EF9");
		}
	}
}