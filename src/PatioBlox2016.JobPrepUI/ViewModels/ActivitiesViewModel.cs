﻿namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Linq;
  using Caliburn.Micro;

  public class ActivitiesViewModel : Conductor<IScreen>.Collection.OneActive, IActivitiesViewModel
  {
    private BindableCollection<IScreen> _screens;
    private IScreen _selectedScreen;

    public ActivitiesViewModel()
    {
      Screens = new BindableCollection<IScreen>();
    }

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

    protected override void OnActivate()
    {
      SelectedScreen = Screens.First(s => s.GetType() == typeof (ExtractionResultValidationViewModel));
      base.OnActivate();
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