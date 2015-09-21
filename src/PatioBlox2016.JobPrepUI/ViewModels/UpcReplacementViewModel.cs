namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class UpcReplacementViewModel : PropertyChangedBase
  {
    private readonly Product _product;
    private readonly UpcReplacement _upcReplacement;

    public UpcReplacementViewModel(Product product)
    {
      _product = product;

      _upcReplacement = new UpcReplacement {InvalidUpc = product.Upc};
      _upcReplacement.Replacement = _product.Upc;
    }

    public string InvalidUpc
    {
      get { return _upcReplacement.InvalidUpc; }
    }

    public int Sku
    {
      get { return _product.Sku; }
    }

    public int UsageCount
    {
      get { return _product.Usages.Count; }
    }

    public string UsageList
    {
      get { return string.Join(", ", _product.Usages); }
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

    public string Errors
    {
      get { return string.Join("; ", _product.BarcodeErrors); }
    }
  }
}