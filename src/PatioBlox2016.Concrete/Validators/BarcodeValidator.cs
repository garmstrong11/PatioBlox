namespace PatioBlox2016.Concrete.Validators
{
  using FluentValidation;

  public class BarcodeValidator : AbstractValidator<Barcode>
  {
    public BarcodeValidator()
    {
      RuleFor(b => b.IsNumeric).Equal(true);
      RuleFor(b => b.BarcodeType).NotEqual(BarcodeType.Unknown)
        .WithMessage("BarcodeType is unknown");

      RuleFor(b => b.CalculatedCheckDigit).Equal(b => b.LastDigit)
        .WithMessage("Incorrect check digit \"{0}\" in Upc. Correct check digit is \"{1}\"", 
          b => b.LastDigit, b => b.CalculatedCheckDigit);
    }
  }
}