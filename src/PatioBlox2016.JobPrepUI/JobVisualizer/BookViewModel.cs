namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using Abstract;

  public class BookViewModel : TreeViewItemViewModel
  {
    private readonly IBook _book;
    
    public BookViewModel(IBook book) : base(null, true)
    {
      _book = book;
    }

    public string BookHeader
    {
      get
      {
        var suffixor = _book.PageCount > 1 ? "pages" : "page";
        return string.Format("{0} ({1} {2})", _book.BookName, _book.PageCount, suffixor);
      }
    }

    protected override void LoadChildren()
    {
      foreach (var section in _book.Sections) {
        Children.Add(new SectionViewModel(this, section));
      }
    }
  }
}