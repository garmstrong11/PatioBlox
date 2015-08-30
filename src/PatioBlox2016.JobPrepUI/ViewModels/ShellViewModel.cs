namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public class ShellViewModel : Conductor<IScreen>, IShell
  {
    private readonly IWindowManager _windowManager;
    private readonly IEventAggregator _eventAggregator;


    public ShellViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
    {
      _windowManager = windowManager;
      _eventAggregator = eventAggregator;
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
  }
}