namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class UpcReplacementViewModel : PropertyChangedBase
  {
    private readonly UpcReplacement _upcReplacement;

    public UpcReplacementViewModel(UpcReplacement upcReplacement)
    {
      if (upcReplacement == null) throw new ArgumentNullException("upcReplacement");

      _upcReplacement = upcReplacement;
    }

    public string InvalidUpc
    {
      get { return _upcReplacement.InvalidUpc; }
      set
      {
        if (value == _upcReplacement.InvalidUpc) return;
        _upcReplacement.InvalidUpc = value;
        NotifyOfPropertyChange();
      }
    }

    public string Replacement
    {
      get { return _upcReplacement.Replacement; }
      set
      {
        if (value == _upcReplacement.Replacement) return;
        _upcReplacement.Replacement = value;
        NotifyOfPropertyChange(() => Replacement);
      }
    }
  }
}