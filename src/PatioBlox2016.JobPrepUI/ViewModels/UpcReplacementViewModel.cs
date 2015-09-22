namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Abstract;
  using Caliburn.Micro;
  using Concrete;

  public class UpcReplacementViewModel : PropertyChangedBase
  {
    private readonly IProduct _product;
    private readonly IUpcReplacement _upcReplacement;

    public UpcReplacementViewModel(IProduct product)
    {
      _product = product;

      _upcReplacement = new UpcReplacement
                        {
                          InvalidUpc = product.Upc,
                          Replacement = _product.Upc
                        };
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