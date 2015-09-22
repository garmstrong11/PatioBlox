namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

  public class Job
  {
    private readonly List<Book> _books;
    
    public Job()
    {
      _books = new List<Book>();
    }

    public IReadOnlyCollection<Book> Books
    {
      get { return _books.AsReadOnly();}
    }

    public void AddBook(Book book)
    {
      _books.Add(book);
    }

    public void AddBookRange(IEnumerable<Book> books)
    {
      _books.AddRange(books);
    }

    public void RemoveBook(Book book)
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