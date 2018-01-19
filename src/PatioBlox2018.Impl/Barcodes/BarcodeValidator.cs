namespace PatioBlox2018.Impl.Barcodes
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using LanguageExt;
  using static LanguageExt.Prelude;

  public class Error : NewType<Error, string>
  {
    public Error(string e) : base(e) { }
  }

  public static class BarcodeValidator
  {
    /// <summary>
    /// Ensures that the candidate is not null or empty.
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    public static Validation<Error, string> NotNullOrWhitespace(string candidate) =>
      string.IsNullOrWhiteSpace(candidate)
        ? Fail<Error, string>(Error.New("No value provided for barcode"))
        : Success<Error, string>(candidate);

    /// <summary>
    /// Ensures that the candidate contains numeric characters only.
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    public static Validation<Error, string> DigitsOnly(string candidate) =>
      candidate.ForAll(char.IsDigit)
        ? Success<Error, string>(candidate)
        : Fail<Error, string>(Error.New($"The candidate value '{candidate}' must contain only digits (0-9)"));

    /// <summary>
    /// Creates a delegate that closes over a passed-in lower bound to
    /// ensure that the candidate contains enough characters.
    /// </summary>
    /// <param name="lowerBound"></param>
    /// <returns></returns>
    public static Func<string, Validation<Error, string>> NotTooShort(int lowerBound) =>
      candidate =>
        candidate.Length >= lowerBound
          ? Success<Error, string>(candidate)
          : Fail<Error, string>(Error.New($"Barcode must not be less than {lowerBound} characters"));

    /// <summary>
    /// Creates a delegate that closes over a passed-in upper bound to
    /// ensure that the candidate does not contain too many characters.
    /// </summary>
    /// <param name="upperBound"></param>
    /// <returns></returns>
    public static Func<string, Validation<Error, string>> NotTooLong(int upperBound) =>
      candidate =>
        candidate.Length <= upperBound
          ? Success<Error, string>(candidate)
          : Fail<Error, string>(Error.New(string.Format(ValidationMessages.BarcodeTooLong, upperBound)));

    /// <summary>
    /// Ensures that the candidate's last digit matches an independently calculated check digit.
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    public static Validation<Error, string> CheckDigitMatchesLastDigit(string candidate)
    {
      var comparison =
        from digit in ExtractLastDigit(candidate)
        from check in CalculateCheckDigit(candidate)
        select digit == check;

      return comparison.Match(
        Some: cand => cand 
          ? Success<Error, string>(candidate) 
          : Fail<Error, string>(Error.New("Check digit is incorrect")),
        None: () => Fail<Error, string>(Error.New("Unable to calculate check digit"))
      );
    }

    /// <summary>
    /// Composes rules to form a whole barcode validation
    /// </summary>
    /// <param name="candidate">The string to validate</param>
    /// <param name="minChars">The minimum length for a valid string</param>
    /// <param name="maxChars">The maximum length for a valid string</param>
    /// <returns></returns>
    public static Validation<Error, string> ValidateBarcode(string candidate, int minChars, int maxChars)
    {
      return NotNullOrWhitespace(candidate)
        .Bind(DigitsOnly)
        .Bind(NotTooShort(minChars))
        .Bind(NotTooLong(maxChars))
        .Bind(CheckDigitMatchesLastDigit);
    }

    private static Option<int> ExtractLastDigit(string src) 
      => parseInt(src.Last().ToString());

    private static Option<int> CalculateCheckDigit(string candidate)
    {
      var len = candidate.Length;
      var factorSeedMap = GetFactorMap();

      var multipliers = factorSeedMap.Find(len);

      var digits = candidate.GetBarcodeDigits(multipliers);
      
      return digits.Map(d => (10 - d.Sum() % 10) %10);
    }

    private static Map<int, IEnumerable<int>> GetFactorMap()
    {
      var upcaSeeds = new[] { 3, 1 }.Repeat(6);
      var eanSeeds  = new[] { 1, 3 }.Repeat(6);

      return Map(
        (12, upcaSeeds), 
        (13, eanSeeds)
      );
    }
  }
}