namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Linq;
  using Caliburn.Micro;
  using PatioBlox2016.JobPrepUI.Infra;

  public class ShellViewModel : Conductor<IScreen>, IShell, IHandle<AcquisitionCompleteEvent>
  {
    private readonly IWindowManager _windowManager;

    public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
    {
      _windowManager = windowManager;
      eventAggregator.Subscribe(this);
    }

    protected override void OnActivate()
    {
      DisplayName = "Patio Blox Prep";
      ShowDropScreen();
    }

    private void ShowDropScreen()
    {
      ActivateItem(IoC.Get<IPatchFileDropViewModel>());
    }

    public void ShowActivities()
    {
      ActivateItem(IoC.Get<IActivitiesViewModel>());
    }

    public void Handle(AcquisitionCompleteEvent message)
    {
      if (message.ValidationResult.IsValid) {
        ShowActivities();
        return;
      }

      var errorWindow = new ErrorWindowViewModel();
      var errorString = string.Join("\n", message.ValidationResult.Errors.Select(e => e.ErrorMessage));
      errorWindow.DisplayName = "Acquisition Failed";
      errorWindow.Errors = errorString;

      _windowManager.ShowDialog(errorWindow);
    }
  }
}