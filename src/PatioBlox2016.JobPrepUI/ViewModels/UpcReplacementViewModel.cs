namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class UpcReplacementViewModel : PropertyChangedBase
  {
    private readonly UpcReplacement _upcReplacement;
    private readonly Barcode _barcode;

    public UpcReplacementViewModel(Barcode barcode)
    {
      if (barcode == null) throw new ArgumentNullException("barcode");

      _barcode = barcode;
      _upcReplacement = new UpcReplacement {InvalidUpc = barcode.Upc};
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

    public List<string> Errors
    {
      get { return _barcode.Errors.Select(e => e.ErrorMessage).ToList(); }
    }
  }
}