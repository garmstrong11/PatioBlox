namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public class BookFactory : IBookFactory
  {
    private readonly ISectionFactory _sectionFactory;

    public BookFactory(ISectionFactory sectionFactory)
    {
      _sectionFactory = sectionFactory;
    }
    
    public Book CreateBook(Job job, string name, IEnumerable<IPatchRowExtract> extracts)
    {
      var book = new Book(job, name);
      book.AddSectionRange(_sectionFactory.CreateSections(extracts, book));

      return book;
    }
  }
}