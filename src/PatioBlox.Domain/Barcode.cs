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
		private readonly int _len;
		private readonly bool _isNumeric;
		private readonly bool _isValidLength;

		public Barcode(string str)
		{
			if (!String.IsNullOrEmpty(str)) {
				_orig = str.Trim();
				_len = _orig.Length;
			}

			_isNumeric = Regex.IsMatch(_orig, @"^\d+$");
			_isValidLength = (_len == 12 || _len == 13);
			BarcodeType = SetBarcodeType();
		}

		public BarcodeType BarcodeType { get; private set;}

		public bool IsValid
		{
			get
			{
				if (!_isValidLength) {
					Message = String.Format("{0} has incorrect length of {1}", _orig, _len);
					return false;
				}

				if (!_isNumeric) {
					Message = String.Format("{0} contains invalid characters", _orig);
					return false;
				}

				if (CalculatedCheckDigit != OriginalCheckDigit) {
					Message = String.Format("{0} has incorrect Check Digit of {1}. Correct check digit is {2}",
						_orig, OriginalCheckDigit, CalculatedCheckDigit);
					return false;
				}

				return true;
			}
			
		}

		public string OriginalCheckDigit {
			get
			{
				return _orig.Substring(_orig.Length - 1);
			}
		}

		public string CalculatedCheckDigit
		{
			get
			{
				var chekValue = _orig.Substring(0, _orig.Length - 1);
				return ((10 - (GetInts(chekValue).Sum() % 10)) %10).ToString(Thread.CurrentThread.CurrentCulture);
			}
		}

		public override string ToString()
		{
			return String.IsNullOrEmpty(Message) ? _orig : Message;
		}

		private BarcodeType SetBarcodeType()
		{
			if (!_isValidLength) return BarcodeType.Unknown;
			if (!_isNumeric) return BarcodeType.Unknown;

			switch (_len) {
				case 12:
					return BarcodeType.Upc;
				case 13:
					return BarcodeType.Ean13;
				default:
					return BarcodeType.Unknown;
			}
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

		public string Message { get; private set; }

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