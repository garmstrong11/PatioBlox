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

    public List<Book> Books
    {
      get { return new List<Book>(_books);}
    }

    public void AddBook(Book book)
    {
      _books.Add(book);
    }
  }
}