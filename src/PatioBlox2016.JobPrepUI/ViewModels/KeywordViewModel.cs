namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Caliburn.Micro;
  using Concrete;

  public class KeywordViewModel : PropertyChangedBase
  {
    private List<string> _usages;
    private BindableCollection<Keyword> _expansions;
    private readonly Keyword _keyword;

    public KeywordViewModel()
    {
      Expansions = new BindableCollection<Keyword>();
      Usages = new List<string>();
    }

    public KeywordViewModel(Keyword keyword) : this()
    {
      _keyword = keyword;
    }

    public string Word
    {
      get { return _keyword.Word; }
      set
      {
        if (value == _keyword.Word) return;
        _keyword.Word = value;
        NotifyOfPropertyChange(() => Word);
      }
    }

    public BindableCollection<Keyword> Expansions
    {
      get { return _expansions; }
      set
      {
        if (Equals(value, _expansions)) return;
        _expansions = value;
        NotifyOfPropertyChange(() => Expansions);
      }
    }

    public Keyword SelectedExpansion
    {
      get { return _keyword.Expansion; }
      set
      {
        var parent = value;

        if (Equals(parent, _keyword.Expansion)) return;

        if (string.IsNullOrWhiteSpace(parent.Word)) {
          _keyword.Expansion = null;
          _keyword.ExpansionId = null;
          NotifyOfPropertyChange(() => SelectedExpansion);
          return;
        }

        _keyword.Expansion = parent;
        SelectedWordType = parent.WordType;

        NotifyOfPropertyChange(() => SelectedExpansion);
        NotifyOfPropertyChange(() => SelectedWordType);
      }
    }

    public IEnumerable<WordType> WordTypes
    {
      get { return Enum.GetValues(typeof (WordType)).Cast<WordType>(); }
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

    public WordType SelectedWordType
    {
      get { return _keyword.WordType; }
      set
      {
        if (value == _keyword.WordType) return;
        _keyword.WordType = value;

        foreach (var abbreviation in _keyword.Abbreviations) {
          abbreviation.WordType = value;
        }

        NotifyOfPropertyChange(() => SelectedWordType);
      }
    }
  }
}