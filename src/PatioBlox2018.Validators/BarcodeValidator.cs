namespace PatioBlox2018.Validators
{
  using System;
  using System.Linq;
  using System.Text.RegularExpressions;
  using FluentValidation;
  using PatioBlox2018.Core;
  using PatioBlox2018.Impl;

  public class BarcodeValidator : AbstractValidator<IBarcode>
  {
    private static Regex DigitRegex { get; }

    static BarcodeValidator()
    {
      DigitRegex = new Regex(@"^\d+$", RegexOptions.Compiled);
    }

    //public BarcodeValidator()
    //{
    //  CascadeMode = CascadeMode.StopOnFirstFailure;

    //  RuleFor(b => b.ItemNumber).NotEmpty();

    //  RuleFor(b => b.Value).NotEmpty()
    //    .WithMessage(bc => $"The upc input value for item '{bc.ItemNumber}' is null or empty.");

    //  RuleFor(b => b.Value).Matches(DigitRegex)
    //    .WithMessage(bc => $"The upc value '{bc.Value}' for item '{bc.ItemNumber}' contains non-numeric characters");

    //  RuleFor(b => b.Value.Length).InclusiveBetween(12, 13)
    //    .WithMessage(bc =>
    //      $"The upc value '{bc.Value}' for item '{bc.ItemNumber}' has invalid length ({bc.Value.Length} characters).");

    //  RuleFor(b => b.Value).Must((b,d) => CalculateCheckDigit(b.Value) == b.Value.GetLastDigit())
    //    .WithMessage(bc => $" has incorrect check digit {LastDigit}. Correct check digit is {CheckDigit}")
    //}

    //private static int CalculateCheckDigit(string candidate)
    //{
    //  if (string.IsNullOrWhiteSpace(candidate))
    //    throw new ArgumentException("Value cannot be null or whitespace.", nameof(candidate));

    //  var discriminator = candidate.Length == 13 ? 1 : 0;

    //  var digits = candidate.GetUpcDigits(discriminator);
    //  var calculatedCheckDigit = 10 - (digits.Sum() % 10) % 10;

    //  return calculatedCheckDigit;
    //}
  }
}