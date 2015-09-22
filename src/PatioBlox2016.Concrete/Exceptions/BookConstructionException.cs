namespace PatioBlox2016.Concrete.Exceptions
{
  using System;

  public class BookConstructionException : Exception
  {
    private readonly string _message;

    public BookConstructionException(string bookName, string message)
    {
      _message = message;
      BookName = bookName;
    }

    public string BookName { get; private set; }

    public override string Message
    {
      get { return _message; }
    }
  }
}