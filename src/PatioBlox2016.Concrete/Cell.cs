namespace PatioBlox2016.Concrete
{
  public class Cell
  {
    public Cell(Page page, int sourceRowIndex, int sku, string palletQty, string description)
    {
      SourceRowIndex = sourceRowIndex;
      Sku = sku;
      PalletQty = palletQty;
      Description = description;
      Page = page;
    }
    
    public int SourceRowIndex { get; private set; }
	  public int Sku { get; private set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public string Name { get; set; }
	  public string PalletQty { get; private set; }
		public string Upc { get; set; }
    public string Description { get; private set; }
    public Page Page { get; set; }

	  public string Image
	  {
			get { return string.Format("{0}.psd", Sku); }
	  }

    protected bool Equals(Cell other)
    {
      return Sku == other.Sku
             && string.Equals(PalletQty, other.PalletQty)
             && string.Equals(Upc, other.Upc)
             && string.Equals(Description, other.Description);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Cell) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        var hashCode = Sku;
        hashCode = (hashCode*397) ^ (PalletQty != null ? PalletQty.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (Upc != null ? Upc.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
        return hashCode;
      }
    }

    public override string ToString()
    {
      return string.Format("{0}_{1}", SourceRowIndex, Description);
    }
  }
}