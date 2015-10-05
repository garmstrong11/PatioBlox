namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using Abstract;

  public class CellViewModel : TreeViewItemViewModel
  {
    private readonly ICell _cell;

    //public CellViewModel(ICell cell)
    //{
    //  _cell = cell;
    //}

    public CellViewModel(TreeViewItemViewModel parent, ICell cell)
      : base(parent, true)
    {
      _cell = cell;
    }

    public int Sku
    {
      get { return _cell.Sku; }
    }

    public string Upc
    {
      get { return _cell.Upc; }
    }

    public string Description
    {
      get { return _cell.Description; }
    }

    public string PalletQty
    {
      get { return _cell.PalletQty; }
    }
  }
}