namespace PatioBlox2016.Concrete.Validators
{
  using FluentValidation;

  public class BarcodeValidator : AbstractValidator<Barcode>
  {
    public BarcodeValidator()
    {
      RuleFor(b => b.IsNumeric).Equal(true).WithMessage("Barcode contains non-numeric characters.");

      RuleFor(b => b.Length).InclusiveBetween(12, 13)
        .WithMessage("Upc must be 12 or 13 characters long. This upc has {PropertyValue} characters.");

      RuleFor(b => b.CalculatedCheckDigit).Equal(b => b.LastDigit)
        .Unless(b => b.CalculatedCheckDigit == "-1")
        .WithMessage("Incorrect check digit \"{0}\" in Upc. Correct check digit is \"{1}\".", 
          b => b.LastDigit, b => b.CalculatedCheckDigit);
    }
  }
}