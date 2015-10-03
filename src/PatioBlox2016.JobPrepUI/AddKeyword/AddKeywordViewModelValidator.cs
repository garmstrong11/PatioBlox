namespace PatioBlox2016.JobPrepUI.AddKeyword
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentValidation;

  public class AddKeywordViewModelValidator : AbstractValidator<AddKeywordViewModel>
  {
    private readonly IEnumerable<string> _existingWords;

    public AddKeywordViewModelValidator(IEnumerable<string> existingKeywords)
    {
      _existingWords = existingKeywords;

      RuleFor(p => p.Word).NotEmpty();
      RuleFor(p => p.Word).Must(NotBeAnExistingKeyword);
      RuleFor(p => p.SelectedParent).NotNull();
    }

    public bool NotBeAnExistingKeyword(AddKeywordViewModel viewModel, string word)
    {
      return !_existingWords.Contains(word);
    }
  }
}