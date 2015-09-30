namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class ProductViewModel : PropertyChangedBase
  {
    private readonly Product _product;

    public ProductViewModel(Product product)
    {
      _product = product;
    }

    public string Upc
    {
      get { return _product.Upc; }
      set
      {
        if (value == _product.Upc) return;
        _product.Upc = value;
        NotifyOfPropertyChange(() => Upc);
      }
    }

    public int Sku
    {
      get { return _product.Sku; }
    }

    public List<string> Failures
    {
      get { return _product.BarcodeErrors; }
    }

    public int UsageCount
    {
      get { return _product.Usages.Count; }
    }
  }
}