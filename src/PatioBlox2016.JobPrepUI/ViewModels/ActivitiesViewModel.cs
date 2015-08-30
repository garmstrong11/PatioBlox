namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public class ActivitiesViewModel : Conductor<IScreen>.Collection.OneActive, IActivitiesViewModel
  {
    private BindableCollection<IScreen> _screens;
    private IScreen _selectedScreen;

    public BindableCollection<IScreen> Screens
    {
      get { return _screens; }
      set
      {
        if (Equals(value, _screens)) return;
        _screens = value;
        NotifyOfPropertyChange(() => Screens);
      }
    }

    public IScreen SelectedScreen
    {
      get { return _selectedScreen; }
      set
      {
        if (Equals(value, _selectedScreen)) return;
        _selectedScreen = value;
        NotifyOfPropertyChange(() => SelectedScreen);
        ActivateItem(_selectedScreen);
      }
    }
  }
}