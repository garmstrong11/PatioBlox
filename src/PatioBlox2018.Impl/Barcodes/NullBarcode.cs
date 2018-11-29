namespace PatioBlox2018.Impl.Barcodes
{
  using System;
  using System.Collections.Generic;
  using PatioBlox2018.Core;

  public class NullBarcode : IBarcode
  {
    public string Value => string.Empty;
    public ISet<string> Usages { get; }

    public NullBarcode()
    {
      Usages = new HashSet<string>();
    }

    public void AddUsage(string bookName)
    {
      Usages.Add(bookName);
    }

    public string Candidate => string.Empty;
  }
}