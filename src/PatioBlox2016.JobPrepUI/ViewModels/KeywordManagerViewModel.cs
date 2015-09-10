namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Caliburn.Micro;
  using Concrete;
  using Extractor;
  using Services.Contracts;

  public class KeywordManagerViewModel : Screen
  {
    private readonly IKeywordRepository _keywordRepository;
    private readonly IExtractionResult _extractionResult;
    private BindableCollection<KeywordViewModel> _keywords;

    public KeywordManagerViewModel(IKeywordRepository keywordRepository, IExtractionResult extractionResult)
    {
      if (keywordRepository == null) throw new ArgumentNullException("keywordRepository");
      if (extractionResult == null) throw new ArgumentNullException("extractionResult");

      _keywordRepository = keywordRepository;
      _extractionResult = extractionResult;

      Keywords = new BindableCollection<KeywordViewModel>();
    }

    protected override void OnActivate()
    {
      var existingKeywords = _keywordRepository.GetAll();
      var nameParent = existingKeywords.SingleOrDefault(k => k.Word == "NAME");

      var existingRoots = existingKeywords
        .Where(kw => !kw.ParentId.HasValue)
        .ToList();

      var existingParentKeywords = existingKeywords
        .Where(kw => existingRoots.Contains(kw.Parent))
        .ToList();

      var newKeywords = _keywordRepository.FilterExisting(_extractionResult.UniqueWords)
        .Select(w => new Keyword(w) {Parent = nameParent})
        .ToList();

      var keywordViewModels = newKeywords
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
        .Where(d => Description.ExtractWordList(d).Contains(word))
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

    public bool CanSave()
    {
      return Keywords.Count > 0;
    }

    public void Save()
    {
      var kws = Keywords.Select(k => k.Keyword);

      //foreach (var keyword in kws) {
      //  _keywordRepository.Add(keyword);
      //}

      _keywordRepository.AddRange(kws);
      var count = _keywordRepository.SaveChanges();

      if (count > 0) {
        Console.WriteLine("Success");
      }
    }
  }
}