namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.Linq;
  using Caliburn.Micro;
  using Abstract;
  using Concrete;
  using Extractor;

  public class KeywordManagerViewModel : Screen
  {
    private readonly IRepository<Keyword> _keyWordRepository;
    private readonly IExtractionResult _extractionResult;
    private BindableCollection<KeywordViewModel> _keywords;

    public KeywordManagerViewModel(IRepository<Keyword> keyWordRepository, IExtractionResult extractionResult)
    {
      _keyWordRepository = keyWordRepository;
      _extractionResult = extractionResult;

      Keywords = new BindableCollection<KeywordViewModel>();
    }

    protected override void OnActivate()
    {
      var existingKeywords = _keyWordRepository.GetAll();
      var existingWords = existingKeywords.Select(k => k.Word);

      var existingRoots = existingKeywords
        .Where(kw => !kw.ParentId.HasValue)
        .ToList();

      var existingParentKeywords = existingKeywords
        .Where(kw => existingRoots.Contains(kw.Parent))
        .ToList();

      var newKeywords = _extractionResult.UniqueWords
        .Except(existingWords)
        .Select(w => new Keyword(w))
        .ToList();

      var addedKeywords = _keyWordRepository.AddRange(newKeywords).ToList();
      _keyWordRepository.SaveChanges();

      var keywordViewModels = addedKeywords
        .Select(v => new KeywordViewModel(v))
        .ToList();

      foreach (var viewModel in keywordViewModels)
      {
        var firstLetter = viewModel.Word.Substring(0, 1);
        var parentList = new List<Keyword>(existingRoots);
        var candidateList = new List<Keyword>();
        
        viewModel.Usages.AddRange(GetUsages(viewModel.Word));

        candidateList.AddRange(existingParentKeywords.Where(w => w.Word.StartsWith(firstLetter)));
        candidateList.AddRange(newKeywords
          .Where(w => w.Word.StartsWith(firstLetter) && w.Word != viewModel.Word));

        parentList.AddRange(candidateList.OrderBy(k => k.Word));

        viewModel.Parents.AddRange(parentList);
      }

      Keywords.AddRange(keywordViewModels);

      base.OnActivate();
    }

    private IEnumerable<string> GetUsages(string word)
    {
      return _extractionResult.UniqueDescriptions
        .Where(d => d.WordList.Contains(word))
        .Select(d => d.Text)
        .ToList();
    }

    public BindableCollection<KeywordViewModel> Keywords
    {
      get { return _keywords; }
      set
      {
        if (Equals(value, _keywords)) return;
        _keywords = value;
        NotifyOfPropertyChange(() => Keywords);
      }
    }

    public void Save()
    {
      _keyWordRepository.SaveChanges();
    }
  }
}