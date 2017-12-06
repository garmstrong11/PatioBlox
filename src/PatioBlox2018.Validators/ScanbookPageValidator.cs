namespace PatioBlox2018.Validators
{
  using FluentValidation;
  using PatioBlox2018.Impl;

  public class ScanbookPageValidator : AbstractValidator<ScanbookPage>
  {
    public ScanbookPageValidator()
    {
      RuleFor(p => p.BlockCount).InclusiveBetween(1, 4);

    }
  }
}