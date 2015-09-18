namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading;
  using FluentValidation.Results;
  using PatioBlox2016.Concrete.Validators;

  public class Barcode
  {
    private static readonly Regex DigitRegex = new Regex(@"^\d+$", RegexOptions.Compiled);
    private static readonly int[] ValidLengths = {12, 13};
    private readonly ValidationResult _validationResult;

    public Barcode(string upc)
    {
      if (string.IsNullOrWhiteSpace(upc)) throw new ArgumentNullException("upc");
      Upc = upc;

      var validator = new BarcodeValidator();
      _validationResult = validator.Validate(this);
    }

    public bool IsValid
    {
      get { return _validationResult.IsValid; }
    }

    public IList<ValidationFailure> Errors
    {
      get { return _validationResult.Errors; }
    } 

    public string Upc { get; private set; }

    public int Length
    {
      get { return Upc.Length; }
    }

    /// <summary>
    /// Identifies a state in which a upc has not been entered in the source data.
    /// </summary>
    public bool IsMissing
    {
      get { return Upc.Contains("_"); }
    }

    public string LastDigit
    {
      get { return Upc.Last().ToString(); }
    }

    public string CalculatedCheckDigit
    {
      get
      {
        if (!DigitRegex.IsMatch(Upc)) return "-1";
        if (!ValidLengths.Contains(Length)) return "-1";

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
        if (Length == 13)
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