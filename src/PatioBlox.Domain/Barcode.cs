namespace PatioBlox.Domain
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Threading;

	public class Barcode : IBarcode
	{
		private readonly string _orig;
		private readonly BarcodeType _barcodeType;

		public Barcode(string str)
		{
			_orig = str.Trim();
			_barcodeType = SetBarcodeType();
		}

		public BarcodeType BarcodeType {
			get
			{
				return _barcodeType;
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
				return (10 - GetInts(chekValue).Sum() % 10).ToString(Thread.CurrentThread.CurrentCulture);
			}
		}

		public override string ToString()
		{
			return String.IsNullOrEmpty(Message) ? _orig : Message;
		}

		private BarcodeType SetBarcodeType()
		{
			var len = _orig.Length;

			if (len < 12 || len > 13) {
				Message = String.Format("{0} has incorrect length of {1}", _orig, len);
				return BarcodeType.Invalid;
			}

			if (!Regex.IsMatch(_orig, @"^\d+$")) {
				Message = String.Format("{0} contains invald characters", _orig);
				return BarcodeType.Invalid;
			}

			var ints = GetInts(_orig);
			var sum = ints.Sum();



			if (sum % 10 != 0) {
				Message = String.Format("{0} has incorrect Check Digit of {1}. Correct check digit is {2}",
					_orig, OriginalCheckDigit, CalculatedCheckDigit);
				return BarcodeType.Invalid;
			}

			return len == 12 ? BarcodeType.Upc : BarcodeType.Ean13;
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

		public string Message { get; set; }

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