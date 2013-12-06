namespace PatioBlox.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Threading;

	public class Barcode : IBarcode
	{
		private readonly string _orig = "";
		
		public Barcode(string str)
		{
			if (String.IsNullOrWhiteSpace(str)) {
				BarcodeType = BarcodeType.Unknown;
				IsValid = false;
				Message = "Barcode Missing";
				return;
			}

			_orig = str.Trim();
			var len = _orig.Length;
			var originalCheckDigit = _orig.Substring(_orig.Length - 1);

			IsValid = true;
			Message = String.Empty;
			Value = String.Empty;

			var isNumeric = Regex.IsMatch(_orig, @"^\d+$");
			var isValidLength = (len == 12 || len == 13);

			if (isValidLength && isNumeric) {
				BarcodeType = len == 12 ? BarcodeType.Upc : BarcodeType.Ean13;
				Value = _orig;

				var chekString = _orig.Substring(0, _orig.Length - 1);
				var calculatedCheckDigit = ((10 - (GetInts(chekString).Sum() % 10)) % 10).ToString(Thread.CurrentThread.CurrentCulture);

				if (calculatedCheckDigit == originalCheckDigit) return;

				IsValid = false;
				Message = String.Format("{0} has incorrect Check Digit of {1}. Correct check digit is {2}",
					_orig, originalCheckDigit, calculatedCheckDigit);
			}

			else {
				BarcodeType = BarcodeType.Unknown;

				if (!isValidLength)
				{
					Message = String.Format("{0} has incorrect length of {1}", _orig, len);
				}

				if (!isNumeric)
				{
					Message = String.Format("{0} contains invalid characters", _orig);
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
			return String.IsNullOrEmpty(Message) ? _orig : Message;
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