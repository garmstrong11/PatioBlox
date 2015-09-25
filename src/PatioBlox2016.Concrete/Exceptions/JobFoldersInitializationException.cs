namespace PatioBlox2016.Concrete.Exceptions
{
  using System;
  using PatioBlox2016.Abstract;

  public class JobFoldersInitializationException : Exception
  {
    private readonly string _message;

    public JobFoldersInitializationException(IFileInfoAdapter fileInfoAdapter, string message)
    {
      _message = message;
      ExcelFilePath = fileInfoAdapter.FullName;
    }

    public string ExcelFilePath { get; private set; }

    public override string Message
    {
      get { return _message; }
    }
  }
}