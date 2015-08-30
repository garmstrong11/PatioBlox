namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public interface IActivitiesViewModel : IScreen, IConductor
  {
    BindableCollection<IScreen> Screens { get; set; }
    IScreen SelectedScreen { get; set; }
  }
}