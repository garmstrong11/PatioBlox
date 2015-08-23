namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading;

  public class Barcode
  {
    private static readonly Regex DigitRegex = new Regex(@"^\d+$", RegexOptions.Compiled);

    private Barcode() { }

    public Barcode(string upc) : this()
    {
      Id = 0;
      Upc = string.IsNullOrWhiteSpace(upc) ? "11111" : upc;
    }
    
    public int Id { get; private set; }
    public string Upc { get; set; }
    public DateTime InsertDate { get; set; }

    public BarcodeType BarcodeType
    {
      get
      {
        var len = Upc.Length;
        BarcodeType barcodeType;
        var dict = new Dictionary<int, BarcodeType>
                   {
                     {12, BarcodeType.Upc},
                     {13, BarcodeType.Ean13}
                   };

        return dict.TryGetValue(len, out barcodeType) ? barcodeType : BarcodeType.Unknown;
      }
    }

    public bool IsNumeric
    {
      get { return DigitRegex.IsMatch(Upc); }
    }

    public string LastDigit
    {
      get { return Upc.Last().ToString(); }
    }

    public string CalculatedCheckDigit
    {
      get
      {
        if (!IsNumeric) return "-1";
        if (BarcodeType == BarcodeType.Unknown) return "-1";

        var chekString = Upc.Substring(0, Upc.Length - 1);
        var digits = GetInts(chekString);
        var calculatedCheckDigit = ((10 - (digits.Sum() % 10)) % 10)
          .ToString(Thread.CurrentThread.CurrentCulture);

        return calculatedCheckDigit;
      }
    }

    private IEnumerable<int> GetInts(string upc)
    {
      var len = upc.Length;
      for (var i = 0; i < len; i++)
      {
        var parsed = int.Parse(upc[i].ToString(Thread.CurrentThread.CurrentCulture));
        if (BarcodeType == BarcodeType.Ean13)
        {
          yield return i % 2 == 0 ? parsed : parsed * 3;
        }
        else
        {
          yield return i % 2 == 0 ? parsed * 3 : parsed;
        }
      }
    }
  }
}