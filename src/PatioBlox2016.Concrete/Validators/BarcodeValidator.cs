namespace PatioBlox2016.Concrete.Validators
{
  using FluentValidation;

  public class BarcodeValidator : AbstractValidator<Barcode>
  {
    public BarcodeValidator()
    {
      RuleFor(b => b.IsMissing).NotEqual(true)
        .WithMessage("Upc was not found for SKU {0}", b => b.Upc.Split('_')[0]);

      RuleFor(b => b.Length).InclusiveBetween(12, 13)
        .Unless(b => b.IsMissing)
        .WithMessage("Upc must be 12 or 13 characters long. This upc has {PropertyValue} characters.");

      RuleFor(b => b.CalculatedCheckDigit).Equal(b => b.LastDigit)
        .Unless(b => b.CalculatedCheckDigit == "-1")
        .WithMessage("Incorrect check digit \"{0}\" in Upc. Correct check digit is \"{1}\".", 
          b => b.LastDigit, b => b.CalculatedCheckDigit);
    }
  }
}