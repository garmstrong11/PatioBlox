namespace PatioBlox2018.Validators
{
  using FluentValidation;
  using PatioBlox2018.Impl;

  public class ScanbookPatioBlokValidator : AbstractValidator<ScanbookPatioBlok>
  {
    //public ScanbookPatioBlokValidator()
    //{
    //  RuleFor(i => i.ItemNumber)
    //    .NotEmpty()
    //    .WithMessage(blok => $"Unable to extract an item number from row {blok.SourceRowIndex}");

    //  RuleFor(b => b.Barcode).SetValidator(new BarcodeValidator());
    //  RuleFor(i => i.PhotoFilename).SetValidator(new PhotoValidator());
    //}
  }
}