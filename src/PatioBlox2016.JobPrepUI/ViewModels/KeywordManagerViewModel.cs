namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.Linq;
  using Caliburn.Micro;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.Extractor;

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

      var existingWords = _keyWordRepository.GetAll().Select(k => k.Word);

      var newWords = _extractionResult.UniqueWords.Except(existingWords).ToList();

      var keywordViewModels = newWords
        .Select(v => new KeywordViewModel(v)
        {
          SelectedWordType = "Name",
          SelectedExpansion = string.Empty
        })
        .ToList();

      foreach (var viewModel in keywordViewModels) {
        var firstLetter = viewModel.Word.Substring(0, 1);

        viewModel.Usages.AddRange(GetUsages(viewModel.Word));
        viewModel.Expansions.AddRange(newWords.Where(w => w.StartsWith(firstLetter)));
      }

      Keywords.AddRange(keywordViewModels);
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
  }
}