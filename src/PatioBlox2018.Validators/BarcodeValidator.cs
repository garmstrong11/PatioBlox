namespace PatioBlox2018.Validators
{
  using FluentValidation;
  using PatioBlox2018.Core;
  using PatioBlox2018.Impl.Barcodes;

  public class BarcodeValidator : AbstractValidator<IBarcode>
  {
    public BarcodeValidator()
    {
      RuleFor(b => b).Must(k => k.GetType() == typeof(ValidBarcode));
    }
  }
}