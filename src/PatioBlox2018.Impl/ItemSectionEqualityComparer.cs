namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  public class ItemSectionEqualityComparer : IEqualityComparer<ScanbookPatioBlok>
  {
    public bool Equals(ScanbookPatioBlok x, ScanbookPatioBlok y)
    {
      if (x is null) return false;
      if (y is null) return false;
      if (ReferenceEquals(x, y)) return true;

      return x.ItemNumber == y.ItemNumber
             && x.Page.Section.Name.Equals(y.Page.Section.Name, StringComparison.CurrentCultureIgnoreCase);
    }

    public int GetHashCode(ScanbookPatioBlok obj)
    {
      unchecked
      {
        return (obj.ItemNumber * 397) ^ obj.Page.Section.Name.GetHashCode();
      }
    }
  }
}