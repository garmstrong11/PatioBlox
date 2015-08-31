namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public class ErrorWindowViewModel : Screen
  {
    private string _errors;

    public string Errors
    {
      get { return _errors; }
      set
      {
        if (value == _errors) return;
        _errors = value;
        NotifyOfPropertyChange();
      }
    }

    public void Ok()
    {
      TryClose();
    }
  }
}