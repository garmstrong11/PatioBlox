namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class KeywordViewModel : PropertyChangedBase, ISelectableViewModel
  {
    private string _word;
    private string _wordType;
    private BindableCollection<string> _wordTypes;
    private bool _isSelected;
    private List<string> _usages;

    public KeywordViewModel()
    {
      SelectedWordType = "Name";
      WordTypes = new BindableCollection<string>(Enum.GetNames(typeof(WordType)));
      IsSelected = false;
      Word = "Untitled";
      Usages = new List<string>();
    }

    public KeywordViewModel(string word) : this()
    {
      Word = word;
    }

    public KeywordViewModel(WordCandidateViewModel wcvm)
      : this()
    {
      Word = wcvm.Word;
      Usages.AddRange(wcvm.Usages);
    }

    public bool IsSelected
    {
      get { return _isSelected; }
      set
      {
        if (value == _isSelected) return;
        _isSelected = value;
        NotifyOfPropertyChange(() => IsSelected);
      }
    }

    public string Word
    {
      get { return _word; }
      set
      {
        if (value == _word) return;
        _word = value;
        NotifyOfPropertyChange(() => Word);
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