namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Collections.Specialized;
  using System.Linq;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class KeywordViewModel : PropertyChangedBase
  {
    private List<string> _usages;
    private readonly Keyword _keyword;
    private BindableCollection<Keyword> _parents;
    private readonly ObservableCollection<Keyword> _dbKeywords; 

    public KeywordViewModel()
    {
      Parents = new BindableCollection<Keyword>();
      Usages = new List<string>();
    }

    public KeywordViewModel(Keyword keyword) : this()
    {
      _keyword = keyword;
    }

    public KeywordViewModel(Keyword keyword, ObservableCollection<Keyword> dbKeywords)
      : this(keyword)
    {
      _keyword = keyword;
      _dbKeywords = dbKeywords;
      _dbKeywords.CollectionChanged += _dbKeywords_CollectionChanged;
    }

    private void _dbKeywords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.NewItems != null) {
        foreach (var newItem in e.NewItems) {
          var newKey = (Keyword) newItem;

          if(_keyword.Word.StartsWith(newKey.Word.Substring(0, 1)))
          Parents.Insert(5, newKey);
        }
      }

      if (e.OldItems != null) {
        Parents.RemoveRange(e.OldItems.Cast<Keyword>());
      }
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

    internal Keyword Keyword
    {
      get { return _keyword; }
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