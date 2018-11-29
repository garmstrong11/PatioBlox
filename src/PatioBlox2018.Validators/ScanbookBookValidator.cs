namespace PatioBlox2018.Validators
{
  using FluentValidation;
  using PatioBlox2018.Impl;
  public class ScanbookBookValidator : AbstractValidator<ScanbookBook>
  {
    public ScanbookBookValidator()
    {
      RuleForEach(r => r.Pages).SetValidator(new ScanbookPageValidator());
    }
  }
}