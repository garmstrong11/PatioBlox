namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public class MessageWindowViewModel : Screen
  {
    private string _message;

    public MessageWindowViewModel(string message)
    {
      _message = message;
    }

    public string Message
    {
      get { return _message; }
      set
      {
        if (value == _message) return;
        _message = value;
        NotifyOfPropertyChange(() => Message);
      }
    }

    public void Ok()
    {
      TryClose();
    }
  }
}