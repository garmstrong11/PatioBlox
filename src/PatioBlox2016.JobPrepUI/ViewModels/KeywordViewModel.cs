namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class KeywordViewModel : PropertyChangedBase
  {
    private string _word;
    private string _wordType;
    private BindableCollection<string> _wordTypes;
    private List<string> _usages;
    private BindableCollection<string> _expansions;
    private string _selectedExpansion;
    private readonly Keyword _keyword;

    public KeywordViewModel()
    {
      WordTypes = new BindableCollection<string>(Enum.GetNames(typeof(WordType)));
      Expansions = new BindableCollection<string>(new[] {""});

      Word = "Untitled";
      SelectedWordType = "Name";
      _selectedExpansion = string.Empty;
      _keyword = new Keyword();

      Usages = new List<string>();
    }

    public KeywordViewModel(string word) : this()
    {
      Word = word;
    }

    public string Word
    {
      get { return _word; }
      set
      {
        if (value == _word) return;
        _word = value;
        _keyword.Word = value;
        NotifyOfPropertyChange(() => Word);
      }
    }

    public BindableCollection<string> Expansions
    {
      get { return _expansions; }
      set
      {
        if (Equals(value, _expansions)) return;
        _expansions = value;
        NotifyOfPropertyChange(() => Expansions);
      }
    }

    public string SelectedExpansion
    {
      get { return _selectedExpansion; }
      set
      {
        if (value == _selectedExpansion) return;
        _selectedExpansion = value;
        NotifyOfPropertyChange(() => SelectedExpansion);
      }
    }

    public BindableCollection<string> WordTypes
    {
      get { return _wordTypes; }
      set
      {
        if (Equals(value, _wordTypes)) return;
        _wordTypes = value;
        NotifyOfPropertyChange(() => WordTypes);
      }
    }

    public List<string> Usages
    {
      get { return _usages; }
      set
      {
        if (Equals(value, _usages)) return;
        _usages = value;
        NotifyOfPropertyChange(() => Usages);
      }
    }

    public string SelectedWordType
    {
      get { return _wordType; }
      set
      {
        if (value == _wordType) return;
        _wordType = value;
        NotifyOfPropertyChange(() => SelectedWordType);
        
        if (value != "Abbreviation") return;

        // Remove this word from the expansion list so
        // an expansion can't refer to itself.
        Expansions.Remove(Word);
        NotifyOfPropertyChange(() => Expansions);
      }
    }

    public Keyword MapToKeyword()
    {
      var keywordDto = new Keyword
      {
        Word = Word,
        WordType = (WordType) Enum.Parse(typeof(WordType), SelectedWordType)
      };

      return keywordDto;
    }
  }
}