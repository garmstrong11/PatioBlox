namespace PatioBlox2018.Impl.Barcodes
{
  using System;
  using System.Linq;
  using System.Text.RegularExpressions;
  using LanguageExt;
  using static LanguageExt.Prelude;
  using PatioBlox2018.Core;

  public class DefaultBarcodeFactory //: IBarcodeFactory
  {
    private static Regex DigitRegex { get; }

    static DefaultBarcodeFactory()
    {
      DigitRegex = new Regex(@"^\d+$", RegexOptions.Compiled);
    }

  //  public IBarcode Create(int itemNumber, string candidate)
  //  {
  //    if (string.IsNullOrWhiteSpace(candidate))
  //      return new MissingBarcode(itemNumber, candidate);

  //    if (!DigitRegex.IsMatch(candidate))
  //      return new NonNumericBarcode(itemNumber, candidate);

  //    if (candidate.Length < 12)
  //      return new TooShortBarcode(itemNumber, candidate);

  //    if (candidate.Length > 13)
  //      return new TooLongBarcode(itemNumber, candidate);

  //    var lastDigit = candidate.GetLastDigit();
  //    var checkDigit = CalculateCheckDigit(candidate);

  //    if (lastDigit != checkDigit)
  //      return new BadCheckDigitBarcode(itemNumber, candidate, lastDigit, checkDigit);

  //    return new ValidBarcode(itemNumber, candidate);
  //  }

  //  private static int CalculateCheckDigit(string candidate)
  //  {
  //    if (string.IsNullOrWhiteSpace(candidate))
  //      throw new ArgumentException("Value cannot be null or whitespace.", nameof(candidate));

  //    var discriminator = candidate.Length == 13 ? 1 : 0;

  //    var digits = candidate.GetUpcDigits(discriminator);
  //    var calculatedCheckDigit = 10 - (digits.Sum() % 10) % 10;

  //    return calculatedCheckDigit;
  //  }
  //}

  //public interface IBusinessRule<T>
  //{
  //  IBusinessRule<T> NextRule { get; }
  //  Option<string> Evaluate(T candidate);
  //  Option<string> Forward(int itemNumber, string candidate);
  //}

  //public abstract class BarcodeRule : IBusinessRule
  //{
  //  protected abstract Func<string, int, Option<string>> Predicate { get; }
  //  protected abstract string Message { get; }

  //  protected BarcodeRule(IBusinessRule nextRule)
  //  {
  //    NextRule = nextRule;
  //  }

  //  public IBusinessRule NextRule { get; }
  //  public Option<string> Evaluate(int itemNumber, string candidate)
  //  {
  //    if
  //    //return Predicate(candidate, itemNumber) ? (i,c) => NextRule.Evaluate(itemNumber, candidate) : Some(Message);
  //  }
  }
}