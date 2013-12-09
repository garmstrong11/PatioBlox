namespace PatioBlox.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Threading;

	public class Barcode : IBarcode
	{	
		public Barcode(string str)
		{
      IsValid = true;
      Message = String.Empty;

      if (String.IsNullOrWhiteSpace(str)) {
				BarcodeType = BarcodeType.Unknown;
				IsValid = false;
				Message = "Barcode Missing";
			  Value = String.Empty;
				return;
			}

			Value = str.Trim();

			var len = Value.Length;
			var originalCheckDigit = Value.Substring(Value.Length - 1);

			var isNumeric = Regex.IsMatch(Value, @"^\d+$");
			var isValidLength = (len == 12 || len == 13);

			if (isValidLength && isNumeric) {
				BarcodeType = len == 12 ? BarcodeType.Upc : BarcodeType.Ean13;

				var chekString = Value.Substring(0, Value.Length - 1);
				var calculatedCheckDigit = ((10 - (GetInts(chekString).Sum() % 10)) % 10).ToString(Thread.CurrentThread.CurrentCulture);

				if (calculatedCheckDigit == originalCheckDigit) return;

				IsValid = false;
				Message = String.Format("{0} has incorrect Check Digit of {1}. Correct check digit is {2}",
					Value, originalCheckDigit, calculatedCheckDigit);
			}

			else {
				BarcodeType = BarcodeType.Unknown;

				if (!isValidLength)
				{
					Message = String.Format("{0} has incorrect length of {1}", Value, len);
				}

				if (!isNumeric)
				{
					Message = String.Format("{0} contains invalid characters", Value);
				}

				IsValid = false;
			}
		}

		public BarcodeType BarcodeType { get; private set;}
		public bool IsValid { get; private set; }
		public string Message { get; private set; }

		public string Value { get; private set; }

		public override string ToString()
		{
			return String.IsNullOrEmpty(Message) ? Value : Message;
		}

		private IEnumerable<int> GetInts(string upc)
		{
			var len = upc.Length;
			for (var i = 0; i < len; i++)
			{
				var parsed = int.Parse(upc[i].ToString(Thread.CurrentThread.CurrentCulture));
				if (BarcodeType == BarcodeType.Ean13) {
					yield return i % 2 == 0 ? parsed : parsed * 3;
				}
				else {
					yield return i % 2 == 0 ? parsed * 3 : parsed;
				}
			}
		}

		public override bool Equals(object obj)
		{
			var item = obj as Barcode;
			if (item == null) return false;

			return ToString() == item.ToString();
		}

		public override int GetHashCode()
		{
			var hash = 17;
			hash += ToString().GetHashCode();

			return hash;
		}
	}
}