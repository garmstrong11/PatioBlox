namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public class ShellViewModel : Conductor<IScreen>, IShell
  {

    public ShellViewModel()
    {
    }

    protected override void OnActivate()
    {
      DisplayName = "Patio Blox Prep";
    }
  }
}