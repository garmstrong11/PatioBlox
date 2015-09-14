namespace PatioBlox2016.Concrete
{
  using Abstract;

  public class Product : IProduct
  {
    public Product(int sku, string upc)
    {
      Sku = sku;
      Upc = upc;
    }
    
    public int Sku { get; private set; }
    public string Upc { get; private set; }

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