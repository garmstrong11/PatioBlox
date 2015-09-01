namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.Linq;
  using Caliburn.Micro;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.Extractor;
  using PatioBlox2016.JobPrepUI.Views;

  public class KeywordManagerViewModel : Screen
  {
    private readonly IRepository<Keyword> _keyWordRepository;
    private readonly IRepository<Expansion> _expansionRepository;
    private readonly IExtractionResult _extractionResult;
    private BindableCollection<WordCandidateViewModel> _candidates;
    private string _selectedCandidate;

    public KeywordManagerViewModel(IRepository<Keyword> keyWordRepository,
      IRepository<Expansion> expansionRepository, IExtractionResult extractionResult)
    {
      _keyWordRepository = keyWordRepository;
      _expansionRepository = expansionRepository;
      _extractionResult = extractionResult;

      Candidates = new BindableCollection<WordCandidateViewModel>();
      Expansions = new BindableCollection<Expansion>();
      Keywords = new BindableCollection<KeywordViewModel>();

      Candidates.AddRange(FilterExistingWords(_extractionResult.UniqueWords)
        .Select(w => new WordCandidateViewModel(w, GetUsages(w))));
    }

    private IEnumerable<string> GetUsages(string word)
    {
      return _extractionResult.UniqueDescriptions
        .Where(d => d.WordList.Contains(word))
        .Select(d => d.Text);
    }

    private IEnumerable<string> FilterExistingWords(IEnumerable<string> candidateWords)
    {
      var keywords = _keyWordRepository.GetAll().Select(k => k.Word);
      var expansions = _expansionRepository.GetAll().Select(e => e.Word);
      var existing = keywords.Union(expansions);

      return candidateWords.Except(existing);
    } 

    public BindableCollection<WordCandidateViewModel> Candidates
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

    public void MoveToKeywords()
    {
      var selectedCandidates = Candidates.Where(w => w.IsSelected).ToList();
      var keywords = selectedCandidates
        .Select(k => new KeywordViewModel(k));

      Keywords.AddRange(keywords);
      Candidates.RemoveRange(selectedCandidates);
    }

    public void MoveToCandidates()
    {
      var selectedKeywords = Keywords.Where(k => k.IsSelected).ToList();
      var candidates = selectedKeywords
        .Select(k => new WordCandidateViewModel(k)).ToList();

      Candidates.AddRange(candidates);
      Keywords.RemoveRange(selectedKeywords);
    }
  }
}