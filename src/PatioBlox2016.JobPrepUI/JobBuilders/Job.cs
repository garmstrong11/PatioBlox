namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using Abstract;
  using Concrete;

  public class Job : IJob
  {
    private readonly IBookFactory _bookFactory;
    private readonly IJobFolders _jobFolders;
    private readonly List<IBook> _books;
    
    public Job(IBookFactory bookFactory, IJobFolders jobFolders)
    {
      _bookFactory = bookFactory;
      _jobFolders = jobFolders;
      _books = new List<IBook>();
    }

    public IReadOnlyCollection<IBook> Books
    {
      get { return _books.AsReadOnly();}
    }

    public void PopulateJob(IEnumerable<IGrouping<string, IPatchRowExtract>> bookGroups)
    {
      var books = bookGroups
        .Select(bg => _bookFactory.CreateBook(this, bg.Key, bg));

      AddBookRange(books);
    }

    public void AddBook(IBook book)
    {
      _books.Add(book);
    }

    public void AddBookRange(IEnumerable<IBook> books)
    {
      _books.AddRange(books);
    }

    public void RemoveBook(IBook book)
    {
      _books.Remove(book);
    }

    public string ToJsxString(int indentLevel)
    {
      var sb = new StringBuilder();
      var contentLevel = indentLevel + 1;

      sb.AppendLine("var patches = {".Indent(indentLevel));

      var books = Books.Select(b => b.ToJsxString(contentLevel));
      var bookStrings = string.Join(",\n", books);

      sb.AppendLine(bookStrings);
      sb.AppendLine("}".Indent(indentLevel));

      return sb.ToString();
    }
  }
}