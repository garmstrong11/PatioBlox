namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class Product
  {
    private readonly HashSet<UsageLocation> _usages;

    public Product()
    {
      _usages = new HashSet<UsageLocation>();
    }

    public Product(int sku, string upc) : this()
    {
      Sku = sku;
      Upc = upc;
    }
    
    public int Sku { get; private set; }
    public string Upc { get; private set; }

    public List<UsageLocation> Usages
    {
      get { return _usages.ToList(); }
    } 

    public bool AddUsage(UsageLocation usage)
    {
      if (usage == null) throw new ArgumentNullException("usage");
      return _usages.Add(usage);
    }

    public IEnumerable<List<UsageLocation>> GetPatchProductDuplicates
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

    public static bool operator ==(Product left, Product right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Product left, Product right)
    {
      return !Equals(left, right);
    }

    public override string ToString()
    {
      return string.Format("Sku: {0}, Upc: {1}", Sku, Upc);
    }
  }
}