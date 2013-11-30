namespace PatioBlox.Tests.DomainTests
{
	using System;
	using Domain;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class BarcodeTests
	{
		[Test]
		public void Can_make_a_Barcode()
		{
			var bc = new Barcode("036000291452");
			bc.Should().NotBeNull();
		}
		
		[Test]
		public void BarcodeType_is_Upc_if_input_is_12_char()
		{
			var bc = new Barcode("036000291452");

			bc.BarcodeType.Should().Be(BarcodeType.Upc);
		}

		[Test]
		public void ChecDigit_calculated_correctly_for_Upc()
		{
			var bc = new Barcode("036000291452");
			bc.CalculatedCheckDigit.Should().Be("2");
		}

		[Test]
		public void ChecDigit_calculated_correctly_for_Ean()
		{
			var bc = new Barcode("8901526206056");
			bc.CalculatedCheckDigit.Should().Be("6");
		}

		[Test]
		public void BarcodeType_is_Unknown_if_input_contains_alpha()
		{
			const string str = "890152a206056";
			var bc = new Barcode(str);
			//var expected = String.Format("{0} contains invalid characters", str);

			bc.BarcodeType.Should().Be(BarcodeType.Unknown);
			//Assert.AreEqual(expected, bc.Message);
		}

		[Test]
		public void Message_is_correct()
		{
			const string str = "89015a206056";
			var bc = new Barcode(str);

			var expected = String.Format("{0} contains invalid characters", str);
			Assert.AreEqual(expected, bc.Message);
		}

		[Test]
		public void BarcodeType_is_Ean13_if_input_is_13_char()
		{
			var bc = new Barcode("8901526206056");

			bc.BarcodeType.Should().Be(BarcodeType.Ean13);
		}

		[Test]
		public void BarcodeType_is_Unknown_if_input_is_less_than_12_char()
		{
			var str = "89015262060";
			var bc = new Barcode(str);

			bc.BarcodeType.Should().Be(BarcodeType.Unknown);
			//bc.Message.Should().Be(String.Format("{0} has incorrect length of {1}", str, str.Length));
		}

		[Test]
		public void BarcodeType_is_Unknown_if_input_is_greater_than_13_char()
		{
			var str = "89015262060794";
			var bc = new Barcode(str);

			bc.BarcodeType.Should().Be(BarcodeType.Unknown);
			//bc.Message.Should().Be(String.Format("{0} has incorrect length of {1}", str, str.Length));
		}

		[Test]
		public void OriginalCheckDigit_returns_correct_value()
		{
			var bc = new Barcode("036000391452");

			bc.OriginalCheckDigit.Should().Be("2");
		}
	}
}