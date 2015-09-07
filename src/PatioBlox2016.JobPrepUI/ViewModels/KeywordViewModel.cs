namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using Caliburn.Micro;
  using Concrete;

  public class KeywordViewModel : PropertyChangedBase
  {
    private List<string> _usages;
    private readonly Keyword _keyword;
    private BindableCollection<Keyword> _parents;

    public KeywordViewModel()
    {
      Parents = new BindableCollection<Keyword>();
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

    public BindableCollection<Keyword> Parents
    {
      get { return _parents; }
      set
      {
        if (Equals(value, _parents)) return;
        _parents = value;
        NotifyOfPropertyChange(() => Parents);
      }
    }

    public Keyword SelectedParent
    {
      get { return _keyword.Parent; }
      set
      {
        if (Equals(value, _keyword.Parent)) return;

        _keyword.Parent = value;
        NotifyOfPropertyChange(() => SelectedParent);
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
  }
}