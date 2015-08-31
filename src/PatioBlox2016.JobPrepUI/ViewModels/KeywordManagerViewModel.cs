namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.DataAccess;
  using PatioBlox2016.Extractor;
  using PatioBlox2016.JobPrepUI.Views;

  public class KeywordManagerViewModel : Screen
  {
    private readonly PatioBloxContext _context;
    private readonly IExtractionResult _extractionResult;
    private BindableCollection<string> _candidates;
    private string _selectedCandidate;

    public KeywordManagerViewModel(PatioBloxContext context, IExtractionResult extractionResult)
    {
      _context = context;
      _extractionResult = extractionResult;
      Candidates = new BindableCollection<string>();
      Expansions = new BindableCollection<Expansion>();
      Keywords = new BindableCollection<KeywordViewModel>();
      Candidates.AddRange(_extractionResult.UniqueWords);
    }

    public BindableCollection<string> Candidates
    {
      get { return _candidates; }
      set
      {
        if (Equals(value, _candidates)) return;
        _candidates = value;
        NotifyOfPropertyChange(() => Candidates);
      }
    }

    public string SelectedCandidate
    {
      get { return _selectedCandidate; }
      set
      {
        if (value == _selectedCandidate) return;
        _selectedCandidate = value;
        NotifyOfPropertyChange();
      }
    }

    public BindableCollection<Expansion> Expansions { get; set; }

    public BindableCollection<KeywordViewModel> Keywords { get; set; }

    public void MoveToKeywords(string item)
    {
      var keyword = new KeywordViewModel(item);

      Keywords.Add(keyword);
      Candidates.Remove(item);
    }
  }
}