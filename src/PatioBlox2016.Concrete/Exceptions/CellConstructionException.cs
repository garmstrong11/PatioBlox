namespace PatioBlox2016.Concrete.Exceptions
{
  using System;

  public class CellConstructionException : Exception
  {
    private readonly string _message;

    public CellConstructionException(int rowIndex, int sku, string desc, string message)
    {
      _message = message;
      RowIndex = rowIndex;
      Sku = sku;
      Description = desc;
    }
    
    public int RowIndex { get; private set; }
    public int Sku { get; private set; }
    public string Description { get; private set; }

    public override string Message
    {
      get { return _message; }
    }
  }
}