namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using AddKeyword;
  using Caliburn.Micro;
  using Concrete;
  using Services.Contracts;

  public class KeywordManagerViewModel : Screen
  {
    private readonly IExtractionResultValidationUow _uow;
    private readonly IWindowManager _windowManager;
    private BindableCollection<KeywordViewModel> _keywords;

    public KeywordManagerViewModel(IExtractionResultValidationUow uow, IWindowManager windowManager)
    {
      if (uow == null) throw new ArgumentNullException("uow");
      if (windowManager == null) throw new ArgumentNullException("windowManager");

      _uow = uow;
      _windowManager = windowManager;

      Keywords = new BindableCollection<KeywordViewModel>();
    }

    protected override void OnActivate()
    {
      _uow.PersistNewKeywords();
      Keywords.Clear();

      var existingRoots = _uow.GetRootKeywords();
      var existingParentKeywords = _uow.GetParentKeywords();
      var newKeywords = _uow.GetNewKeywords();

      var keywordViewModels = newKeywords
        .Select(v => new KeywordViewModel(v, _uow.GetKeywords()))
        .ToList();

      foreach (var viewModel in keywordViewModels)
      {
        var firstLetter = viewModel.Word.Substring(0, 1);
        var parentList = new List<Keyword>(existingRoots);
        var candidateList = new List<Keyword>();
        
        viewModel.Usages.AddRange(_uow.GetUsagesForWord(viewModel.Word));

        candidateList.AddRange(existingParentKeywords.Where(w => w.Word.StartsWith(firstLetter)));
        candidateList.AddRange(newKeywords
          .Where(w => w.Word.StartsWith(firstLetter) && w.Word != viewModel.Word));

        parentList.AddRange(candidateList.OrderBy(k => k.Word));

        viewModel.Parents.AddRange(parentList);
      }

      Keywords.AddRange(keywordViewModels);
      //_uow.AddKeyword(new Keyword("Skeezer") {ParentId = 3});

      base.OnActivate();
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
      _uow.SaveChanges();
    }

    public void AddKeyword()
    {
      var existingWords = _uow.GetKeywords().Select(k => k.Word);
      var validator = new AddKeywordViewModelValidator(existingWords);
      var vm = new AddKeywordViewModel(validator, _uow) {DisplayName = "Add A Keyword"};

      _windowManager.ShowDialog(vm);
    }
  }
}