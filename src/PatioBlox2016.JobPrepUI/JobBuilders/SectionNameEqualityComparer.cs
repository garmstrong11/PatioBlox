namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using Concrete;

  public class SectionNameEqualityComparer : IEqualityComparer<Section>
  {
    public bool Equals(Section x, Section y)
    {
      if (ReferenceEquals(x, y)) return true;
      if (ReferenceEquals(x, null)) return false;
      if (ReferenceEquals(y, null)) return false;
      return x.GetType() == y.GetType() && string.Equals(x.SectionName, y.SectionName);
    }

    public int GetHashCode(Section obj)
    {
      return obj.SectionName.GetHashCode();
    }
  }
}