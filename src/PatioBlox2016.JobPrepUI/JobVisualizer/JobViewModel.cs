namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using System.Collections.ObjectModel;
  using System.Linq;
  using Abstract;

  public class JobViewModel
  {
    private readonly ReadOnlyCollection<BookViewModel> _books;

    public JobViewModel(IJob job)
    {
      _books = new ReadOnlyCollection<BookViewModel>(
        job.Books.Select(b => new BookViewModel(b)).ToList());
    }

    public ReadOnlyCollection<BookViewModel> Books
    {
      get { return _books; }
    } 
  }
}