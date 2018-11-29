namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using PatioBlox2018.Core;

  public class BarcodeCandidateEqualityComparer : IEqualityComparer<IBarcode>
  {
    public bool Equals(IBarcode x, IBarcode y)
    {
      if (x is null) return false;
      if (y is null) return false;
      return ReferenceEquals(x, y) 
             || x.Candidate.Equals(y.Candidate, StringComparison.CurrentCultureIgnoreCase);
    }

    public int GetHashCode(IBarcode obj) => obj.Candidate.GetHashCode();
  }
}