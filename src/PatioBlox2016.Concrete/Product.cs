namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Abstract;

  public class Product : IProduct
  {
    private readonly HashSet<IUsageLocation> _usages;
    private Barcode _barcode;
    private string _upc;

    public Product(int sku, string upc)
    {
      Sku = sku;
      Upc = upc;
      _usages = new HashSet<IUsageLocation>();
      _barcode = new Barcode(upc);
    }
    
    public int Sku { get; private set; }

    public string Upc
    {
      get { return _upc; }
      set
      {
        _upc = value;
        _barcode = new Barcode(_upc);
      }
    }

    public List<IUsageLocation> Usages
    {
      get { return _usages.ToList(); }
    }

    public bool IsBarcodeInvalid
    {
      get { return !_barcode.IsValid; }
    }

    public List<string> BarcodeErrors
    {
      get { return _barcode.Errors.Select(b => b.ErrorMessage).ToList(); }
    }

    public bool AddUsage(IUsageLocation usage)
    {
      if (usage == null) throw new ArgumentNullException("usage");
      return _usages.Add(usage);
    }

    public IEnumerable<List<IUsageLocation>> PatchProductDuplicates
    {
      get
      {
        var groops = from usage in _usages
          group usage by usage.PatchName
          into patchGroups
          where patchGroups.Count() > 1
          select patchGroups;

        return groops.Select(groop => groop.ToList());
      }
    }

    public bool HasPatchProductDuplicates
    {
      get { return PatchProductDuplicates.Any(); }
    }

    protected bool Equals(Product other)
    {
      return Sku == other.Sku && string.Equals(Upc, other.Upc);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Product) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (Sku * 397) ^ (Upc != null ? Upc.GetHashCode() : 0);
      }
    }

    public override string ToString()
    {
      return string.Format("Sku: {0}, Upc: {1}", Sku, Upc);
    }
  }
}