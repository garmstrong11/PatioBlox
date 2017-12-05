namespace PatioBlox2016.Reporter
{
  using System;
  using PatioBlox2018.Impl;
  using PatioBlox2018.Impl.AbstractReporter;

  public class PatchReportDto : IPatchReportDto
  {
    private ScanbookBook Book { get; }

    public PatchReportDto(ScanbookBook book)
    {
      Book = book ?? throw new ArgumentNullException(nameof(book));
    }

    public string Name => Book.Name;
    public int PageCount => Book.PageCount;
    public int StoreCount => Book.StoreCount;
    public int CopiesPerStore => ScanbookBook.CopiesPerStore;
  }
}