namespace PatioBlox2016.JobPrepUI.Infra
{
  using System;
  using System.Collections;
  using System.ComponentModel;
  using System.Linq;
  using Caliburn.Micro;
  using FluentValidation;
  using FluentValidation.Results;

  public class ValidatingScreen : Screen, INotifyDataErrorInfo
  {
    private readonly IValidator _validator;
    private ValidationResult _validationResult;

    public ValidatingScreen(IValidator validator)
    {
      _validator = validator;

      PropertyChanged += Validate;
    }

    private void Validate(object sender, PropertyChangedEventArgs e)
    {
      _validationResult = _validator.Validate(this);

      foreach (var failure in _validationResult.Errors) {
        NotifyOfErrorChange(failure.PropertyName);
      }
    }

    private void NotifyOfErrorChange(string propertyName)
    {
      var handler = ErrorsChanged;
      if (handler != null) {
        handler(this, new DataErrorsChangedEventArgs(propertyName));
      }
    }

    public IEnumerable GetErrors(string propertyName)
    {
      return _validationResult.Errors
        .Where(e => e.PropertyName == propertyName)
        .Select(e => e.ErrorMessage);
    }

    public bool HasErrors
    {
      get { return _validationResult.Errors.Any(); }
    }

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged = delegate { };
  }
}