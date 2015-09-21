namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

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
  }
}