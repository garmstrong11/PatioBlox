namespace PatioBlox2018.Impl
{
  using System;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;

  [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
  public class ScanbookProduct
  {
    //[JsonProperty(PropertyName = "sku")]
    public int Sku { get; }

    //[JsonProperty(PropertyName = "upc")]
    public string Upc { get; }

    public ScanbookProduct(int sku, string upc)
    {
      if (string.IsNullOrWhiteSpace(upc))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(upc));
      Sku = sku;
      Upc = upc;
    }

    protected bool Equals(ScanbookProduct other)
    {
      return Sku == other.Sku && string.Equals(Upc, other.Upc, StringComparison.CurrentCultureIgnoreCase);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((ScanbookProduct) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (Sku * 397) ^ (Upc != null ? StringComparer.CurrentCultureIgnoreCase.GetHashCode(Upc) : 0);
      }
    }
  }
}