namespace PatioBlox2016.JobPrepUI.Infra
{
  using Abstract;
  using Properties;

  public class ColumnIndexService : IColumnIndexService
  {
    private readonly Settings _props;

    public ColumnIndexService()
    {
      _props = Settings.Default;
    }

    public int SectionIndex
    {
      get { return _props.SectionIndex; }
      set { _props.SectionIndex = value; }
    }

    public int ItemIndex
    {
      get { return _props.ItemIndex; }
      set { _props.ItemIndex = value; }
    }

    public int DescriptionIndex
    {
      get { return _props.DescriptionIndex; }
      set { _props.DescriptionIndex = value; }
    }

    public int PalletQtyIndex
    {
      get { return _props.PalletQtyIndex; }
      set { _props.PalletQtyIndex = value; }
    }

    public int UpcIndex
    {
      get { return _props.UpcIndex; }
      set { _props.UpcIndex = value; }
    }
  }
}